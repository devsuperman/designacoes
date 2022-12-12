using System;
using App.Data;
using App.Models;
using System.Linq;
using App.Constantes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    [Authorize]
    public class DesignacoesController : Controller
    {
        private readonly Contexto _context;

        public DesignacoesController(Contexto context)
        {
            _context = context;
        }

        [AllowAnonymous, HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var hoje = DateTime.Now;
            var semanaPassada = hoje.AddDays(-7);

            var lista = await _context.designacoes
                .Where(w => 
                    !w.SubstituicaoId.HasValue &&
                    w.Data >= semanaPassada)
                .Select(a => new DesignacaoDTO
                {
                    Id = a.Id,
                    Data = a.Data,
                    Designado = a.Designado.Nome,
                    Ajudante = a.Ajudante.Nome,
                    Tipo = a.Tipo,
                    Observacao = a.Observacao,
                    Situacao = a.Situacao,
                    SemanaAtual = a.SemanaAtual(hoje),
                    FoiSubstituida = a.FoiSubstituida,
                    EhSubstituicao = a.EhSubstituicao
                })
                .ToListAsync();

            var listaAgrupada = lista
                .GroupBy(a => a.Data)
                .OrderByDescending(a => a.Key)
                .ToList();

            return View(listaAgrupada);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designacao = await _context.designacoes
                .Include(d => d.Ajudante)
                .Include(d => d.Designado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designacao == null)
            {
                return NotFound();
            }

            return View(designacao);
        }

        // GET: Designacoes/Create
        public async Task<IActionResult> Create()
        {
            var publicadoresDisponiveis = await _context.publicadores
            .Where(w => !w.ImpedidoDeFazerPartes)
            .OrderBy(a => a.Designacoes.OrderByDescending(s => s.Data).FirstOrDefault().Data)
            .Select(s => new {
                Id = s.Id,
                Nome = s.Nome + " " + s.Designacoes.OrderByDescending(a => a.Data).FirstOrDefault().Data.ToShortDateString()
            })
            .ToListAsync();

            ViewData["AjudanteId"] = new SelectList(publicadoresDisponiveis, "Id", "Nome");
            ViewData["DesignadoId"] = new SelectList(publicadoresDisponiveis, "Id", "Nome");
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;

            var ultimaDataDeDesignacao = _context.designacoes.Max(a => a.Data);

            var model = new Designacao()
            {
                Data = ultimaDataDeDesignacao
            };

            return View(model);
        }

        // POST: Designacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,DesignadoId,AjudanteId,Observacao,Data,DataDeRegistro")] Designacao designacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { data = designacao.Data });
            }
            ViewData["AjudanteId"] = new SelectList(_context.publicadores, "Id", "Nome", designacao.AjudanteId);
            ViewData["DesignadoId"] = new SelectList(_context.publicadores, "Id", "Nome", designacao.DesignadoId);
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;

            return View(designacao);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designacao = await _context.designacoes.FindAsync(id);
            if (designacao == null)
            {
                return NotFound();
            }
            ViewData["AjudanteId"] = new SelectList(_context.publicadores, "Id", "Nome", designacao.AjudanteId);
            ViewData["DesignadoId"] = new SelectList(_context.publicadores, "Id", "Nome", designacao.DesignadoId);
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;

            return View(designacao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Designacao model)
        {

            if (ModelState.IsValid)
            {
                var designacao = await _context.designacoes.FindAsync(model.Id);
                designacao.Atualizar(model.Data, model.DesignadoId, model.AjudanteId, model.Tipo, model.Observacao);
                _context.Update(designacao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["AjudanteId"] = new SelectList(_context.publicadores, "Id", "Nome", model.AjudanteId);
            ViewData["DesignadoId"] = new SelectList(_context.publicadores, "Id", "Nome", model.DesignadoId);
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;

            return View(model);
        }

        public async Task<IActionResult> Substituir(int id)
        {
            var designacao = await _context.designacoes
                .FindAsync(id);

            var substituicao = new Designacao(designacao);

            if (designacao == null)
                return NotFound();

            ViewData["AjudanteId"] = new SelectList(_context.publicadores, "Id", "Nome", designacao.AjudanteId);
            ViewData["DesignadoId"] = new SelectList(_context.publicadores, "Id", "Nome", designacao.DesignadoId);
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;
            ViewData["DesignacaoPaiId"] = id;

            return View(substituicao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Substituir(int DesignacaoPaiId, string motivo, Designacao substituicaoModel)
        {

            if (ModelState.IsValid)
            {
                var designacao = await _context.designacoes.FindAsync(DesignacaoPaiId);

                designacao.Substituir(substituicaoModel, motivo);

                _context.Update(designacao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = designacao.Substituicao.Id });
            }

            ViewData["AjudanteId"] = new SelectList(_context.publicadores, "Id", "Nome", substituicaoModel.AjudanteId);
            ViewData["DesignadoId"] = new SelectList(_context.publicadores, "Id", "Nome", substituicaoModel.DesignadoId);
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;
            ViewData["DesignacaoId"] = DesignacaoPaiId;

            return View(substituicaoModel);
        }

        public async Task<IActionResult> Avancar(int id)
        {
            var designacao = await _context.designacoes.FindAsync(id);
            designacao.Avancar();
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = id });
        }

        // GET: Designacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designacao = await _context.designacoes
                .Include(d => d.Ajudante)
                .Include(d => d.Designado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designacao == null)
            {
                return NotFound();
            }

            return View(designacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designacao = await _context.designacoes.FindAsync(id);
            _context.designacoes.Remove(designacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignacaoExists(int id)
        {
            return _context.designacoes.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> Substituicoes()
        {

            var substituicoes = await _context.designacoes
                .Include(a => a.Substituicao)
                .Where(w => w.Substituicao != null)
                .Select(a => new DesignacaoDTO
                {
                    Id = a.Id,
                    Data = a.Data,
                    Designado = a.Designado.Nome,
                    Ajudante = a.Ajudante.Nome,
                    Tipo = a.Tipo,
                    Observacao = a.Observacao,
                    Situacao = a.Situacao,
                    FoiSubstituida = a.FoiSubstituida,
                    EhSubstituicao = a.EhSubstituicao,
                    MotivoDaSubstituicao = a.MotivoDaSubstituicao
                })
                .OrderByDescending(a => a.Data)
                .ToListAsync();

            return View(substituicoes);
        }
    }
}

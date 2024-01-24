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

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var hoje = DateTime.Now;
            var semanaPassada = hoje.AddDays(-7);

            var lista = await _context.Designacoes
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
                    SemanaAtual = a.SemanaAtual(hoje),
                    FoiSubstituida = a.FoiSubstituida,
                    EhSubstituicao = a.EhSubstituicao
                })
                .ToListAsync();

            var listaAgrupada = lista
                .OrderBy(o => o.Tipo.Contains("Discurso"))
                    .ThenBy(o => o.Tipo.Contains("Estudo"))
                    .ThenBy(o => o.Tipo.Contains("Revisita"))
                    .ThenBy(o => o.Tipo.Contains("Primeira"))
                    .ThenBy(o => o.Tipo.Contains("Leitura"))
                .GroupBy(a => a.Data)
                .OrderByDescending(a => a.Key)
                .ToList();

            return View(listaAgrupada);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designacao = await _context.Designacoes
                .Include(d => d.Ajudante)
                .Include(d => d.Designado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designacao == null)
            {
                return NotFound();
            }

            return View(designacao);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarViewDatasDoFormulario();

            var ultimaDataDeDesignacao = DateTime.Today;

            if (await _context.Designacoes.AnyAsync())
                ultimaDataDeDesignacao = await _context.Designacoes.MaxAsync(a => a.Data);

            var model = new Designacao()
            {
                Data = ultimaDataDeDesignacao
            };

            return View(model);
        }

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

            await CarregarViewDatasDoFormulario();
            return View(designacao);
        }



        private async Task CarregarViewDatasDoFormulario()
        {
            var publicadoresDisponiveis = await _context.Publicadores
                .Where(w => !w.ImpedidoDeFazerPartes)
                .Select(s => new ListarPublicador
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    UltimaDesignacao = s.Designacoes.OrderByDescending(a => a.Data).FirstOrDefault()
                })
                .ToListAsync();

            publicadoresDisponiveis = publicadoresDisponiveis
                .OrderBy(o => o.UltimaDesignacao?.Data)
                .ThenBy(t => t.Nome)
                .ToList();

            ViewData["AjudanteId"] = new SelectList(publicadoresDisponiveis, "Id", "NomeComData");
            ViewData["DesignadoId"] = new SelectList(publicadoresDisponiveis, "Id", "NomeComData");
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;
        }
        public async Task<IActionResult> ListarAjudantes(int designadoId)
        {       
            var publicadores = await _context.Publicadores
                .AsNoTracking()
                .Where(w =>
                    !w.ImpedidoDeFazerPartes &&
                    w.Id != designadoId)
                .Select(s => new ListarPublicador
                {
                    Id = s.Id,
                    Nome = s.Nome
                })
                .ToListAsync();

            var designacoes = await _context.Designacoes
                .AsNoTracking()
                .OrderByDescending(o => o.Id)
                .Where(w =>
                    w.DesignadoId == designadoId ||
                    w.AjudanteId == designadoId)
                .Select(s => new
                {
                    DesignadoId = s.DesignadoId,
                    AjudanteId = s.AjudanteId,
                    Data = s.Data
                })
                .ToListAsync();

            var lista = publicadores
                .Select(s => new
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Data = designacoes.FirstOrDefault(w =>
                            (w.DesignadoId == designadoId && w.AjudanteId == s.Id) ||
                            (w.DesignadoId == s.Id && w.AjudanteId == designadoId)
                        )?.Data
                })
                .OrderBy(o => o.Data)
                .ThenBy(t => t.Nome)
                .ToList();

            return Ok(lista);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designacao = await _context.Designacoes.FindAsync(id);
            if (designacao == null)
            {
                return NotFound();
            }

            await CarregarViewDatasDoFormulario();
            return View(designacao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Designacao model)
        {

            if (ModelState.IsValid)
            {
                var designacao = await _context.Designacoes.FindAsync(model.Id);
                designacao.Atualizar(model.Data, model.DesignadoId, model.AjudanteId, model.Tipo, model.Observacao);
                _context.Update(designacao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await CarregarViewDatasDoFormulario();

            return View(model);
        }

        public async Task<IActionResult> Substituir(int id)
        {
            var designacao = await _context.Designacoes
                .FindAsync(id);

            var substituicao = new Designacao(designacao);

            if (designacao == null)
                return NotFound();

            await CarregarViewDatasDoFormulario();
            ViewData["DesignacaoPaiId"] = id;

            return View(substituicao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Substituir(int DesignacaoPaiId, string motivo, Designacao substituicaoModel)
        {

            if (ModelState.IsValid)
            {
                var designacao = await _context.Designacoes.FindAsync(DesignacaoPaiId);

                designacao.Substituir(substituicaoModel, motivo);

                _context.Update(designacao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = designacao.Substituicao.Id });
            }

            await CarregarViewDatasDoFormulario();
            ViewData["DesignacaoId"] = DesignacaoPaiId;

            return View(substituicaoModel);
        }

        public async Task<IActionResult> Avancar(int id)
        {
            var designacao = await _context.Designacoes.FindAsync(id);
            designacao.Avancar();
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designacao = await _context.Designacoes
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
            var designacao = await _context.Designacoes.FindAsync(id);
            _context.Designacoes.Remove(designacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignacaoExists(int id)
        {
            return _context.Designacoes.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> Substituicoes()
        {

            var substituicoes = await _context.Designacoes
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

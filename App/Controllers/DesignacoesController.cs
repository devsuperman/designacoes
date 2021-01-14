
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;
using App.Constantes;

namespace App.Controllers
{
    public class DesignacoesController : Controller
    {
        private readonly Contexto _context;

        public DesignacoesController(Contexto context)
        {
            _context = context;
        }

        // GET: Designacoes
        public async Task<IActionResult> Index()
        {
            var hoje = DateTime.Now;

            var lista = await _context.Designacoes
                .Select(a => new DesignacaoDTO
                {
                    Id = a.Id,
                    Data = a.Data,
                    Designado = a.Designado.Nome,
                    Ajudante = a.Ajudante.Nome,
                    Tipo = a.Tipo,
                    Observacao = a.Observacao,
                    SemanaAtual = a.SemanaAtual(hoje)
                })
                .ToListAsync();

            var listaAgrupada = lista
                .GroupBy(a => a.Data)
                .OrderByDescending(a => a.Key)
                .ToList();                

            return View(listaAgrupada);
        }

        // GET: Designacoes/Details/5
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

        // GET: Designacoes/Create
        public IActionResult Create()
        {
            ViewData["AjudanteId"] = new SelectList(_context.Publicadores, "Id", "Nome");
            ViewData["DesignadoId"] = new SelectList(_context.Publicadores, "Id", "Nome");
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;

            return View();
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
                return RedirectToAction(nameof(Create));
            }
            ViewData["AjudanteId"] = new SelectList(_context.Publicadores, "Id", "Nome", designacao.AjudanteId);
            ViewData["DesignadoId"] = new SelectList(_context.Publicadores, "Id", "Nome", designacao.DesignadoId);
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;

            return View(designacao);
        }

        // GET: Designacoes/Edit/5
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
            ViewData["AjudanteId"] = new SelectList(_context.Publicadores, "Id", "Nome", designacao.AjudanteId);
            ViewData["DesignadoId"] = new SelectList(_context.Publicadores, "Id", "Nome", designacao.DesignadoId);
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;

            return View(designacao);
        }

        // POST: Designacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,DesignadoId,AjudanteId,Observacao,Data")] Designacao designacao)
        {
            if (id != designacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignacaoExists(designacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AjudanteId"] = new SelectList(_context.Publicadores, "Id", "Nome", designacao.AjudanteId);
            ViewData["DesignadoId"] = new SelectList(_context.Publicadores, "Id", "Nome", designacao.DesignadoId);
            ViewData["Tipos"] = Tipos.TiposDeDesignacao;

            return View(designacao);
        }

        // GET: Designacoes/Delete/5
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

        // POST: Designacoes/Delete/5
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
    }
}

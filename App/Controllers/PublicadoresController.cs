using App.Data;
using App.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    [Authorize]
    public class PublicadoresController : Controller
    {
        private readonly Contexto _context;

        public PublicadoresController(Contexto context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Publicadores                
                .Select(a => new PublicadorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Sexo = a.Sexo,
                    DataDaUltimaDesignacao = a.Designacoes.OrderByDescending(o => o.Data).FirstOrDefault().Data,
                    DataDaUltimaDesignacaoComoAjudante = a.DesignacoesComoAjudante.OrderByDescending(o => o.Data).FirstOrDefault().Data                    
                })
                .OrderBy(a => a.DataDaUltimaDesignacao)
                .ToListAsync();  
                
            return View(lista);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicador = await _context.Publicadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicador == null)
            {
                return NotFound();
            }

            return View(publicador);
        }

        // GET: Publicadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publicadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sexo")] Publicador publicador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicador);
        }

        // GET: Publicadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicador = await _context.Publicadores.FindAsync(id);
            if (publicador == null)
            {
                return NotFound();
            }
            return View(publicador);
        }

        // POST: Publicadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sexo")] Publicador publicador)
        {
            if (id != publicador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicadorExists(publicador.Id))
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
            return View(publicador);
        }

        // GET: Publicadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicador = await _context.Publicadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicador == null)
            {
                return NotFound();
            }

            return View(publicador);
        }

        // POST: Publicadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicador = await _context.Publicadores.FindAsync(id);
            _context.Publicadores.Remove(publicador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicadorExists(int id)
        {
            return _context.Publicadores.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoAllVoyaje.Data;
using ProjetoAllVoyaje.Models;

namespace ProjetoAllVoyaje.Controllers
{
    [Authorize(Roles = "Admin, ADMIN")]
    public class TipoPacotesController : Controller
    {
        private readonly AllVoyajeDbContext _context;

        public TipoPacotesController(AllVoyajeDbContext context)
        {
            _context = context;
        }

        // GET: TipoPacotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposPacotes.ToListAsync());
        }

        // GET: TipoPacotes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPacote = await _context.TiposPacotes
                .FirstOrDefaultAsync(m => m.TipoPacoteId == id);
            if (tipoPacote == null)
            {
                return NotFound();
            }

            return View(tipoPacote);
        }

        // GET: TipoPacotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoPacotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoPacoteId,Tipo")] TipoPacote tipoPacote)
        {
            if (ModelState.IsValid)
            {
                tipoPacote.TipoPacoteId = Guid.NewGuid();
                _context.Add(tipoPacote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPacote);
        }

        // GET: TipoPacotes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPacote = await _context.TiposPacotes.FindAsync(id);
            if (tipoPacote == null)
            {
                return NotFound();
            }
            return View(tipoPacote);
        }

        // POST: TipoPacotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TipoPacoteId,Tipo")] TipoPacote tipoPacote)
        {
            if (id != tipoPacote.TipoPacoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPacote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPacoteExists(tipoPacote.TipoPacoteId))
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
            return View(tipoPacote);
        }

        // GET: TipoPacotes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPacote = await _context.TiposPacotes
                .FirstOrDefaultAsync(m => m.TipoPacoteId == id);
            if (tipoPacote == null)
            {
                return NotFound();
            }

            return View(tipoPacote);
        }

        // POST: TipoPacotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipoPacote = await _context.TiposPacotes.FindAsync(id);
            if (tipoPacote != null)
            {
                _context.TiposPacotes.Remove(tipoPacote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPacoteExists(Guid id)
        {
            return _context.TiposPacotes.Any(e => e.TipoPacoteId == id);
        }
    }
}

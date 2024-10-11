using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoAllVoyaje.Data;
using ProjetoAllVoyaje.Models;

namespace ProjetoAllVoyaje.Controllers
{
    public class PacoteViagemsController : Controller
    {
        private readonly AllVoyajeDbContext _context;

        public PacoteViagemsController(AllVoyajeDbContext context)
        {
            _context = context;
        }

        // GET: PacoteViagems
        public async Task<IActionResult> Index()
        {
            var allVoyajeDbContext = _context.PacoteViagens.Include(p => p.tipopacote);
            return View(await allVoyajeDbContext.ToListAsync());
        }

        // GET: PacoteViagems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteViagem = await _context.PacoteViagens
                .Include(p => p.tipopacote)
                .FirstOrDefaultAsync(m => m.PacoteViagemId == id);
            if (pacoteViagem == null)
            {
                return NotFound();
            }

            return View(pacoteViagem);
        }

        // GET: PacoteViagems/Create
        public IActionResult Create()
        {
            ViewData["TipoPacoteId"] = new SelectList(_context.TiposPacotes, "TipoPacoteId", "TipoPacoteId");
            return View();
        }

        // POST: PacoteViagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacoteViagemId,Descricao,TipoPacoteId,Preco")] PacoteViagem pacoteViagem)
        {
            if (ModelState.IsValid)
            {
                pacoteViagem.PacoteViagemId = Guid.NewGuid();
                _context.Add(pacoteViagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoPacoteId"] = new SelectList(_context.TiposPacotes, "TipoPacoteId", "TipoPacoteId", pacoteViagem.TipoPacoteId);
            return View(pacoteViagem);
        }

        // GET: PacoteViagems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteViagem = await _context.PacoteViagens.FindAsync(id);
            if (pacoteViagem == null)
            {
                return NotFound();
            }
            ViewData["TipoPacoteId"] = new SelectList(_context.TiposPacotes, "TipoPacoteId", "TipoPacoteId", pacoteViagem.TipoPacoteId);
            return View(pacoteViagem);
        }

        // POST: PacoteViagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PacoteViagemId,Descricao,TipoPacoteId,Preco")] PacoteViagem pacoteViagem)
        {
            if (id != pacoteViagem.PacoteViagemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacoteViagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacoteViagemExists(pacoteViagem.PacoteViagemId))
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
            ViewData["TipoPacoteId"] = new SelectList(_context.TiposPacotes, "TipoPacoteId", "TipoPacoteId", pacoteViagem.TipoPacoteId);
            return View(pacoteViagem);
        }

        // GET: PacoteViagems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteViagem = await _context.PacoteViagens
                .Include(p => p.tipopacote)
                .FirstOrDefaultAsync(m => m.PacoteViagemId == id);
            if (pacoteViagem == null)
            {
                return NotFound();
            }

            return View(pacoteViagem);
        }

        // POST: PacoteViagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pacoteViagem = await _context.PacoteViagens.FindAsync(id);
            if (pacoteViagem != null)
            {
                _context.PacoteViagens.Remove(pacoteViagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacoteViagemExists(Guid id)
        {
            return _context.PacoteViagens.Any(e => e.PacoteViagemId == id);
        }
    }
}

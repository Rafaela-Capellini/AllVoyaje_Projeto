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
    public class OpcoesDatasController : Controller
    {
        private readonly AllVoyajeDbContext _context;

        public OpcoesDatasController(AllVoyajeDbContext context)
        {
            _context = context;
        }

        // GET: OpcoesDatas
        public async Task<IActionResult> Index()
        {
            var allVoyajeDbContext = _context.OpcoesDatas.Include(o => o.PacoteViagem);
            return View(await allVoyajeDbContext.ToListAsync());
        }

        // GET: OpcoesDatas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcoesDatas = await _context.OpcoesDatas
                .Include(o => o.PacoteViagem)
                .FirstOrDefaultAsync(m => m.OpcoesDatasId == id);
            if (opcoesDatas == null)
            {
                return NotFound();
            }

            return View(opcoesDatas);
        }

        // GET: OpcoesDatas/Create
        public IActionResult Create()
        {
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId");
            return View();
        }

        // POST: OpcoesDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpcoesDatasId,DataSaida,DataRetorno,PacoteViagemId,Preco")] OpcoesDatas opcoesDatas)
        {
            if (ModelState.IsValid)
            {
                opcoesDatas.OpcoesDatasId = Guid.NewGuid();
                _context.Add(opcoesDatas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", opcoesDatas.PacoteViagemId);
            return View(opcoesDatas);
        }

        // GET: OpcoesDatas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcoesDatas = await _context.OpcoesDatas.FindAsync(id);
            if (opcoesDatas == null)
            {
                return NotFound();
            }
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", opcoesDatas.PacoteViagemId);
            return View(opcoesDatas);
        }

        // POST: OpcoesDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OpcoesDatasId,DataSaida,DataRetorno,PacoteViagemId,Preco")] OpcoesDatas opcoesDatas)
        {
            if (id != opcoesDatas.OpcoesDatasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opcoesDatas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpcoesDatasExists(opcoesDatas.OpcoesDatasId))
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
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", opcoesDatas.PacoteViagemId);
            return View(opcoesDatas);
        }

        // GET: OpcoesDatas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcoesDatas = await _context.OpcoesDatas
                .Include(o => o.PacoteViagem)
                .FirstOrDefaultAsync(m => m.OpcoesDatasId == id);
            if (opcoesDatas == null)
            {
                return NotFound();
            }

            return View(opcoesDatas);
        }

        // POST: OpcoesDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var opcoesDatas = await _context.OpcoesDatas.FindAsync(id);
            if (opcoesDatas != null)
            {
                _context.OpcoesDatas.Remove(opcoesDatas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpcoesDatasExists(Guid id)
        {
            return _context.OpcoesDatas.Any(e => e.OpcoesDatasId == id);
        }
    }
}

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
    public class PacoteClientesController : Controller
    {
        private readonly AllVoyajeDbContext _context;

        public PacoteClientesController(AllVoyajeDbContext context)
        {
            _context = context;
        }

        // GET: PacoteClientes
        public async Task<IActionResult> Index()
        {
            var allVoyajeDbContext = _context.PacoteClientes.Include(p => p.Cliente).Include(p => p.TipoPacote);
            return View(await allVoyajeDbContext.ToListAsync());
        }

        // GET: PacoteClientes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteCliente = await _context.PacoteClientes
                .Include(p => p.Cliente)
                .Include(p => p.TipoPacote)
                .FirstOrDefaultAsync(m => m.PacoteClienteId == id);
            if (pacoteCliente == null)
            {
                return NotFound();
            }

            return View(pacoteCliente);
        }

        // GET: PacoteClientes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome");
            ViewData["TipoPacoteId"] = new SelectList(_context.TiposPacotes, "TipoPacoteId", "Tipo");
            return View();
        }

        // POST: PacoteClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacoteClienteId,ClienteId,TipoPacoteId,QtdPessoas")] PacoteCliente pacoteCliente)
        {
            if (ModelState.IsValid)
            {
                pacoteCliente.PacoteClienteId = Guid.NewGuid();
                _context.Add(pacoteCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", pacoteCliente.ClienteId);
            ViewData["TipoPacoteId"] = new SelectList(_context.TiposPacotes, "TipoPacoteId", "Tipo", pacoteCliente.TipoPacoteId);
            return View(pacoteCliente);
        }

        // GET: PacoteClientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteCliente = await _context.PacoteClientes.FindAsync(id);
            if (pacoteCliente == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", pacoteCliente.ClienteId);
            ViewData["TipoPacoteId"] = new SelectList(_context.TiposPacotes, "TipoPacoteId", "Tipo", pacoteCliente.TipoPacoteId);
            return View(pacoteCliente);
        }

        // POST: PacoteClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PacoteClienteId,ClienteId,TipoPacoteId,QtdPessoas")] PacoteCliente pacoteCliente)
        {
            if (id != pacoteCliente.PacoteClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacoteCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacoteClienteExists(pacoteCliente.PacoteClienteId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", pacoteCliente.ClienteId);
            ViewData["TipoPacoteId"] = new SelectList(_context.TiposPacotes, "TipoPacoteId", "TipoPacoteId", pacoteCliente.TipoPacoteId);
            return View(pacoteCliente);
        }

        // GET: PacoteClientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteCliente = await _context.PacoteClientes
                .Include(p => p.Cliente)
                .Include(p => p.TipoPacote)
                .FirstOrDefaultAsync(m => m.PacoteClienteId == id);
            if (pacoteCliente == null)
            {
                return NotFound();
            }

            return View(pacoteCliente);
        }

        // POST: PacoteClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pacoteCliente = await _context.PacoteClientes.FindAsync(id);
            if (pacoteCliente != null)
            {
                _context.PacoteClientes.Remove(pacoteCliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacoteClienteExists(Guid id)
        {
            return _context.PacoteClientes.Any(e => e.PacoteClienteId == id);
        }
    }
}

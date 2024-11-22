using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public PacoteClientesController(AllVoyajeDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PacoteClientes
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cliente = await _context.Clientes.Where(c => c.AspNetUserId == user.Id).FirstOrDefaultAsync();
            var clienteId = cliente.ClienteId;
            var eAdmin = await _userManager.IsInRoleAsync(user, "ADMIN");

            ViewData["admin"] = eAdmin;

            var allVoyajeDbContext = _context.PacoteClientes.Include(p => p.Cliente).Include(p => p.Pacote);
            
            if (eAdmin)
                return View(await allVoyajeDbContext.ToListAsync());

            return View(await allVoyajeDbContext.Where(c => c.ClienteId == clienteId).ToListAsync());
        }

        // GET: PacoteClientes/Details/5
        [Authorize(Roles = "Admin, ADMIN")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteCliente = await _context.PacoteClientes
                .Include(p => p.Cliente)
                .Include(p => p.Pacote)
                .FirstOrDefaultAsync(m => m.PacoteClienteId == id);
            if (pacoteCliente == null)
            {
                return NotFound();
            }

            return View(pacoteCliente);
        }

        // GET: PacoteClientes/Create
        [Authorize(Roles = "Admin, ADMIN")]
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId");
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId");
            return View();
        }

        // POST: PacoteClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacoteClienteId,ClienteId,PacoteViagemId,QtdPessoas")] PacoteCliente pacoteCliente)
        {
            if (ModelState.IsValid)
            {
                pacoteCliente.PacoteClienteId = Guid.NewGuid();
                _context.Add(pacoteCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", pacoteCliente.ClienteId);
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", pacoteCliente.PacoteViagemId);
            return View(pacoteCliente);
        }

        // GET: PacoteClientes/Edit/5
        [Authorize(Roles = "Admin, ADMIN")]
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", pacoteCliente.ClienteId);
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", pacoteCliente.PacoteViagemId);
            return View(pacoteCliente);
        }

        // POST: PacoteClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ADMIN")]
        public async Task<IActionResult> Edit(Guid id, [Bind("PacoteClienteId,ClienteId,PacoteViagemId,QtdPessoas")] PacoteCliente pacoteCliente)
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
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", pacoteCliente.PacoteViagemId);
            return View(pacoteCliente);
        }

        // GET: PacoteClientes/Delete/5
        [Authorize(Roles = "Admin, ADMIN")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteCliente = await _context.PacoteClientes
                .Include(p => p.Cliente)
                .Include(p => p.Pacote)
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
        [Authorize(Roles = "Admin, ADMIN")]
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

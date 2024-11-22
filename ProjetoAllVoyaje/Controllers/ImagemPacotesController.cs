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
    public class ImagemPacotesController : Controller
    {
        private readonly AllVoyajeDbContext _context;

        public ImagemPacotesController(AllVoyajeDbContext context)
        {
            _context = context;
        }

        // GET: ImagemPacotes
        public async Task<IActionResult> Index()
        {
            var allVoyajeDbContext = _context.ImagemPacote.Include(i => i.PacoteViagem);
            return View(await allVoyajeDbContext.ToListAsync());
        }

        // GET: ImagemPacotes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemPacote = await _context.ImagemPacote
                .Include(i => i.PacoteViagem)
                .FirstOrDefaultAsync(m => m.ImagemPacoteId == id);
            if (imagemPacote == null)
            {
                return NotFound();
            }

            return View(imagemPacote);
        }

        // GET: ImagemPacotes/Create
        public IActionResult Create()
        {
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId");
            return View();
        }

        // POST: ImagemPacotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacoteViagemId,URL,Imagem")] ImagemPacote imagemPacote)
        {
            if (ModelState.IsValid)
            {
                if (imagemPacote.Imagem != null && imagemPacote.Imagem.Length > 0)
                {
                    // Crie um nome único para a imagem
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagemPacote.Imagem.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                    // Certifique-se de que a pasta existe
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var filePath = Path.Combine(uploadPath, fileName);

                    // Salve o arquivo na pasta especificada
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagemPacote.Imagem.CopyToAsync(stream);
                    }

                    // Armazene a URL ou o nome do arquivo no modelo
                    imagemPacote.URL = $"/img/{fileName}";
                }

                imagemPacote.ImagemPacoteId = Guid.NewGuid();
                _context.Add(imagemPacote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", imagemPacote.PacoteViagemId);
            return View(imagemPacote);
        }


        // GET: ImagemPacotes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemPacote = await _context.ImagemPacote.FindAsync(id);
            if (imagemPacote == null)
            {
                return NotFound();
            }
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", imagemPacote.PacoteViagemId);
            return View(imagemPacote);
        }

        // POST: ImagemPacotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ImagemPacoteId,PacoteViagemId,URL")] ImagemPacote imagemPacote)
        {
            if (id != imagemPacote.ImagemPacoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagemPacote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagemPacoteExists(imagemPacote.ImagemPacoteId))
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
            ViewData["PacoteViagemId"] = new SelectList(_context.PacoteViagens, "PacoteViagemId", "PacoteViagemId", imagemPacote.PacoteViagemId);
            return View(imagemPacote);
        }

        // GET: ImagemPacotes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemPacote = await _context.ImagemPacote
                .Include(i => i.PacoteViagem)
                .FirstOrDefaultAsync(m => m.ImagemPacoteId == id);
            if (imagemPacote == null)
            {
                return NotFound();
            }

            return View(imagemPacote);
        }

        // POST: ImagemPacotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var imagemPacote = await _context.ImagemPacote.FindAsync(id);
            if (imagemPacote != null)
            {
                _context.ImagemPacote.Remove(imagemPacote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagemPacoteExists(Guid id)
        {
            return _context.ImagemPacote.Any(e => e.ImagemPacoteId == id);
        }
    }
}

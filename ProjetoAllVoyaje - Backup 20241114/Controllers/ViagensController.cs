using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAllVoyaje.Data;
using ProjetoAllVoyaje.Models;

namespace ProjetoAllVoyaje.Controllers
{
    public class ViagensController : Controller
    {
        private readonly AllVoyajeDbContext _context;

        public ViagensController(AllVoyajeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dados = await _context.ImagemPacote
                .Include(i => i.PacoteViagem)
                .ThenInclude(t => t.tipopacote)
                .ToListAsync();
            List<ImagemPacote> imagens = await _context.ImagemPacote.ToListAsync();
            ViewData["imagens"] = imagens;

            return View(dados);
        }

        public async Task<IActionResult> filtrar(string origem, string destino, string tipoViagem, DateOnly dataInicio, DateOnly dataFim)
        {

            var dados = await _context.ImagemPacote
                .Include(i => i.PacoteViagem)
                .ThenInclude(t => t.tipopacote)
                .ToListAsync();
            List<ImagemPacote> imagens = await _context.ImagemPacote.ToListAsync();
            ViewData["imagens"] = imagens;

            return View("Index", dados);
           
        }
    }
}

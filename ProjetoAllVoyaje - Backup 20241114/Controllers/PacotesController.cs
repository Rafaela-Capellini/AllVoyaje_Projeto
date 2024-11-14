using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAllVoyaje.Data;
using ProjetoAllVoyaje.Models;

namespace ProjetoAllVoyaje.Controllers
{
    public class PacotesController : Controller
    {
        private readonly AllVoyajeDbContext _context;
        public PacotesController(AllVoyajeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allVoyajeDbContext = _context.PacoteViagens.Include(p => p.tipopacote);
            List<ImagemPacote> imagens = await _context.ImagemPacote.ToListAsync();
            ViewData["imagens"] = imagens;
            return View(await allVoyajeDbContext.ToListAsync());
        }

        public async Task<IActionResult> Adicionar()
        {
            return View();
        }

        public async Task<IActionResult> Details(Guid Id)
        {
            var dados = _context.PacoteViagens
                .Include(p => p.tipopacote)                
                .Where(pv => pv.PacoteViagemId == Id)
                .FirstOrDefault();

            ViewData["preco"] = dados.Preco;

            //var dados = _context.PacoteViagens.FirstOrDefault(p => p.PacoteViagemId == Id);
            List<ImagemPacote> imagens = await _context.ImagemPacote.ToListAsync();
            ViewData["imagens"] = imagens;
            return View(dados);
        }

    }
}

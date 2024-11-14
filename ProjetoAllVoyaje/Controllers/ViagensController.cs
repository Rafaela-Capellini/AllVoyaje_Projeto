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

                .Where(
                        //p => p.PacoteViagem.DataSaida >= dataInicio
                        // && p.PacoteViagem.DataRetorno <= dataFim
                        p => p.PacoteViagem.TipoPacoteId.ToString().ToUpper().Equals("BAF3B655-4C52-48C4-9BE1-A0F8F8DA74DB")
                        && p.PacoteViagem.NomePacote.Contains(destino))
                .AsQueryable()
                .ToListAsync();

            List<ImagemPacote> imagens = await _context.ImagemPacote.ToListAsync();
            ViewData["imagens"] = imagens;

            return View("Index", dados);
           
        }
    }
}

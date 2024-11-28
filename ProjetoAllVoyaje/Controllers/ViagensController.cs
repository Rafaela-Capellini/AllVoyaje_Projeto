using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProjetoAllVoyaje.Data;
using ProjetoAllVoyaje.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            var origens = getOrigens();
            var destinos = getDestinos();

            // Passa a lista de opções para a view
            ViewBag.Origens = origens;
            ViewBag.Destinos = destinos;

            return View(dados);
        }

        private List<SelectListItem> getOrigens()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Todas as origens", Value = "Todas" },
                new SelectListItem { Text = "Sao Paulo", Value = "São Paulo" },
                new SelectListItem { Text = "Campinas", Value = "Campinas" },
                new SelectListItem { Text = "Bauru", Value = "Bauru" },
                new SelectListItem { Text = "Igaraçu", Value = "Igaraçu" },
            };
        }

        private List<SelectListItem> getDestinos()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Todos os destinos", Value = "Todos" },
               new SelectListItem { Text = "São Paulo", Value = "São Paulo" },
                new SelectListItem { Text = "Bahia", Value = "Bahia" },
                new SelectListItem { Text = "Santa Catarina", Value = "Santa Catarina" },
                new SelectListItem { Text = "Amazonas", Value = "Amazonas" },
                new SelectListItem { Text = "Paris", Value = "Paris" },
                new SelectListItem { Text = "Londres", Value = "Londres" },
                new SelectListItem { Text = "Miami", Value = "Miami" },
                new SelectListItem { Text = "Lisboa", Value = "Lisboa" },
                new SelectListItem { Text = "Tokyo", Value = "Tokyo" },
                new SelectListItem { Text = "Barcelona", Value = "Barcelona" },
                new SelectListItem { Text = "Milão", Value = "Milão" },
                new SelectListItem { Text = "Dubai", Value = "Dubai" },
                new SelectListItem { Text = "Munique", Value = "Munique" },
                new SelectListItem { Text = "Roma", Value = "Roma" },
                new SelectListItem { Text = "Rio de Janeiro", Value = "Rio de Janeiro" },
                new SelectListItem { Text = "Nova York", Value = "Nova York" }
            };
        }

        public async Task<IActionResult> filtrar(string origem, string destino, string tipoViagem, DateOnly? dataInicio, DateOnly? dataFim)
        {

            var dados = await _context.ImagemPacote
                .Include(i => i.PacoteViagem)
                .ThenInclude(t => t.tipopacote)
                .Where(
                        p => (dataInicio.HasValue ? p.PacoteViagem.DataSaida >= dataInicio : true)
                        && (dataFim.HasValue ? p.PacoteViagem.DataRetorno <= dataFim : true)
                        // p => p.PacoteViagem.TipoPacoteId.ToString().ToUpper().Equals("7DFF4C8A-700C-4824-A7F2-A1321A22B817")
                        && (destino.ToUpper().Equals("TODOS") ? true : p.PacoteViagem.NomePacote.Contains(destino)))
                .AsQueryable()

                .ToListAsync();

            List<ImagemPacote> imagens = await _context.ImagemPacote.ToListAsync();
            ViewData["imagens"] = imagens;

            // Retornar os campos usados na pesquisa
            ViewData["origem"] = origem;
            ViewData["destino"] = destino;
            ViewData["dataInicio"] = dataInicio.HasValue ? dataInicio.Value.ToString("yyyy-MM-dd") : "";
            ViewData["dataFim"] = dataFim.HasValue ? dataFim.Value.ToString("yyyy-MM-dd") : "";

            var origens = getOrigens();
            var destinos = getDestinos();

            // Passa a lista de opções para a view
            ViewBag.Origens = origens;
            ViewBag.Destinos = destinos;

            if (dados.Count == 0)
            {
                ViewData["mensagem"] = "Não há pacotes de viagens para os filtros selecionados";
            }

            return View("Index", dados);

        }
    }
}

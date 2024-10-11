using Microsoft.AspNetCore.Mvc;
using ProjetoAllVoyaje.Models;

namespace ProjetoAllVoyaje.Controllers
{
    public class PacotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Adicionar()
        {
            /*List<Pacote> pacotes = new List<Pacote>();*/

            return View();
        }
    }
}

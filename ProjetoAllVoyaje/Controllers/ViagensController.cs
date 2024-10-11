using Microsoft.AspNetCore.Mvc;

namespace ProjetoAllVoyaje.Controllers
{
    public class ViagensController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

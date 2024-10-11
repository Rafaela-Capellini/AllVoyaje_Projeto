using Microsoft.AspNetCore.Mvc;

namespace ProjetoAllVoyaje.Controllers
{
    public class PassagensController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

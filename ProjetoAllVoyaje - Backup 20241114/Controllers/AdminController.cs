using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAllVoyaje.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin, ADMIN")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

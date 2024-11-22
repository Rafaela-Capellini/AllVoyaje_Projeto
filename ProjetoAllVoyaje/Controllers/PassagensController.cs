using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAllVoyaje.Data;
using ProjetoAllVoyaje.Models;
using Microsoft.AspNetCore.Identity;

namespace ProjetoAllVoyaje.Controllers
{
    public class PassagensController : Controller
    {
        private readonly AllVoyajeDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PassagensController(AllVoyajeDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        public async Task<IActionResult> Index(Guid Id, int quantPessoas)
        {
            var dados = _context.PacoteViagens
                .Include(p => p.tipopacote)
                .Where(pv => pv.PacoteViagemId == Id)
                .FirstOrDefault();

            ViewData["quantPessoas"] = quantPessoas;

            //var dados = _context.PacoteViagens.FirstOrDefault(p => p.PacoteViagemId == Id);
            List<ImagemPacote> imagens = await _context.ImagemPacote.ToListAsync();
            ViewData["imagens"] = imagens;

            return View(dados);
        }

        //[Bind("ClienteId,Nome,CPF,Telefone,Cargo")] Cliente cliente        
        public async Task<IActionResult> Salvar(Guid Id, int quantPessoas)
        {
            var user = await _userManager.GetUserAsync(User);           

            if (user == null)
                return RedirectToAction("Logar", "Home");

            var ClienteId = _context.Clientes.Where(c => c.AspNetUserId == user.Id).FirstOrDefault().ClienteId;


            PacoteCliente pc = new PacoteCliente
            {
                PacoteClienteId = Guid.NewGuid(),
                ClienteId = ClienteId,    
                PacoteViagemId = Id,
                QtdPessoas = quantPessoas
            };

            _context.Add(pc);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "PacoteClientes");
        }
    }
}

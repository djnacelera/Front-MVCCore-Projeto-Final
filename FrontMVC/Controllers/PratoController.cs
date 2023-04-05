using FrontMVC.Interfaces;
using FrontMVC.Models.Prato;
using FrontMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontMVC.Controllers
{
    public class PratoController : Controller
    {
        private readonly IService<PratoModel> pratoService;

        public PratoController(IService<PratoModel> pratoService)
        {
            this.pratoService = pratoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await pratoService.Listar();
            return View(list);
        }
    }
}

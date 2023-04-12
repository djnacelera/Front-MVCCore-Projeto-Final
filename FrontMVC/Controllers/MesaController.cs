using AutoMapper;
using FrontMVC.Interfaces;
using FrontMVC.Models.Mesa;
using Microsoft.AspNetCore.Mvc;

namespace FrontMVC.Controllers
{
    public class MesaController : Controller
    {
        private readonly IService<MesaModel> _service;
        
        public MesaController(IService<MesaModel> service)
        {
            _service = service;
        }

        public async Task <IActionResult> Index()
        {
            IEnumerable<MesaModel> list = await _service.Listar();
            return View(list);
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            MesaIncluir Novo = new MesaIncluir();
            return View(Novo);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(MesaModel Novo)
        {
            
            var retorno = await _service.Adicionar(Novo);

            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Erro");

        }
    }
}

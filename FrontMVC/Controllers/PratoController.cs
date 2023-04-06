using FrontMVC.Interfaces;
using FrontMVC.Models.Prato;
using FrontMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace FrontMVC.Controllers
{
    public class PratoController : Controller
    {
        private readonly IServicePrato<PratoModel> pratoService;

        public PratoController(IServicePrato<PratoModel> pratoService)
        {
            this.pratoService = pratoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await pratoService.Listar();

            foreach (var imagem in list)
            {
                imagem.ConverterBase64ParaJpg();
            }


            return View(list);
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            PratoModel Novo = new PratoModel();
            return View(Novo);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(PratoModel Novo)
        {
            Novo.Foto = await pratoService.ConverteImg(Novo.FotoBase);

            var retorno = await pratoService.Adicionar(Novo);

            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Erro");
        }


    }
}

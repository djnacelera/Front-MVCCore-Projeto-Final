﻿using FrontMVC.Interfaces;
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

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var retorno = await pratoService.Excluir(id);

            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Erro");
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            var lista = await pratoService.FiltrarId(id);

            lista.ConverterBase64ParaJpg();


            return View(lista);
        }

        [HttpPost]
        public async Task<ActionResult> Alterar(PratoModel prato)
        {
            if (prato.FotoBase != null)
                prato.Foto = await pratoService.ConverteImg(prato.FotoBase);

            var retorno = await pratoService.Atualizar(prato, prato.Id);

            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Erro");
        }

        [HttpPost]
        public async Task<ActionResult> AtivarPrato(Guid id,bool status)
        {
            var retorno = await pratoService.AtivarPrato(id, status);

            if (retorno != null)
            {
                ViewData["Mensagem"] = "Operação realizada com sucesso!";
                return RedirectToAction("Index");
            }

            else
                throw new Exception("Erro");


        }

        [HttpPost]
        public async Task<ActionResult> InativarPrato(Guid id, bool status)
        {
            var retorno = await pratoService.InativarPrato(id, status);

            if (retorno != null)
                return RedirectToAction("Index");

            else
                throw new Exception("Erro");


        }

        public IActionResult DeleteConfirmationModal(string id)
        {
            ViewBag.Id = id;
            return PartialView("_DeleteConfirmationModal");
        }



    }
}

using AutoMapper;
using FrontMVC.Interfaces;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Prato;
using FrontMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontMVC.Controllers
{
    public class MesaController : Controller
    {
        private readonly IServiceMesa<MesaModel> _service;
        private readonly IMapper _mapper;
        public MesaController(IServiceMesa<MesaModel> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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

        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            MesaModel mesa = await _service.FiltrarId(id);                   
            
            return View(_mapper.Map<MesaAlterar>(mesa));
        }

        [HttpPost]
        public async Task<ActionResult> Alterar(MesaModel mesa)
        {          

            var retorno = await _service.Atualizar(mesa, mesa.Id);

            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Erro");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            MesaModel mesa = await _service.FiltrarId(id);

            return View(mesa);
        }
        [HttpGet]
        public async Task<IActionResult> OcuparMesa(Guid id)
        {
            MesaModel mesa = await _service.FiltrarId(id);
            OcuparMesa model = _mapper.Map<OcuparMesa>(mesa);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> OcuparMesa(OcuparMesa ocuparMesa)
        {
            var retorno = await _service.OcuparMesa(ocuparMesa);
            if (retorno != null)
                return RedirectToAction("Index");
            else
                throw new Exception("Erro");
        }
        
        [HttpGet]
        public async Task<IActionResult> DesocuparMesa(Guid id)
        {
            var retorno = await _service.DesocuparMesa(id);
            if (retorno)
                return RedirectToAction("Index");
            else
                throw new Exception("Erro");
        }


    }
}

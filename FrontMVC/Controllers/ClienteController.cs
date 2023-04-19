using FrontMVC.Interfaces;
using FrontMVC.Models.Cliente;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Prato;
using FrontMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontMVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IServiceCliente<ClienteModel> _clienteService;

        public ClienteController(IServiceCliente<ClienteModel> clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ClienteModel> list = await _clienteService.Listar();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var cliente = await _clienteService.FiltrarId(id);
            return View(cliente);
        }

        [HttpGet]
        public async Task<ClienteModel> BuscarCpf(string cpf)
        {
            var cliente = await _clienteService.FiltrarPorCpf(cpf);
            return cliente;
        }
    }
}

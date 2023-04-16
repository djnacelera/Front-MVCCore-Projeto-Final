using FrontMVC.Interfaces;
using FrontMVC.Models.Log;
using FrontMVC.Models.Prato;
using Microsoft.AspNetCore.Mvc;

namespace FrontMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly IServiceLog<LogModel> _logService;

        public LogController(IServiceLog<LogModel> logService)
        {
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _logService.Listar();
            return View(list);
        }

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var Log = await _logService.FiltrarId(id);
            return View(Log);
        }
    }
}

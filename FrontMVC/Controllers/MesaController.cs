using Microsoft.AspNetCore.Mvc;

namespace FrontMVC.Controllers
{
    public class MesaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

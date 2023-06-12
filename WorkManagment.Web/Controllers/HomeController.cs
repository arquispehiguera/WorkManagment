using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkManagment.Core.Entities;

namespace WorkManagment.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            TempData["Nombres"] = ViewBag.Message = HttpContext.Session.GetString("Nombres");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    


    }
}

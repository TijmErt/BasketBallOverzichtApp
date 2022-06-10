using BasketBallASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BasketBallASPNET.Controllers
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
            if(HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
            
        }

        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
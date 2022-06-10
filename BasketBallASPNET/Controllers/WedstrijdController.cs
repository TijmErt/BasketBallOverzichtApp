using BusnLogic.Containers;
using DALMSSQLServer.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BasketBallASPNET.Controllers
{
    public class WedstrijdController : Controller
    {
        private readonly WedstrijdConainer wc = new WedstrijdConainer(new WedstrijdMSSQLDAL());
        public IActionResult Index()
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
    }
}

using BasketBallASPNET.Models;
using BusnLogic.Containers;
using DALMSSQLServer;
using Microsoft.AspNetCore.Mvc;

namespace BasketBallASPNET.Controllers
{
    public class AccountController : Controller
    {
        private GebruikerContainer container = new GebruikerContainer(new GebruikerMSSQLDAL());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AccountVM account)
        {
            string wachtwoord = BCrypt.Net.BCrypt.EnhancedHashPassword(account.Wachtwoord, 13);
            string bevestigWachtwoord = BCrypt.Net.BCrypt.EnhancedHashPassword(account.BevestigWachtwoord, 13);
            if (BCrypt.Net.BCrypt.EnhancedVerify(wachtwoord, bevestigWachtwoord))
            {

            }
            return View();
        }
    }
}

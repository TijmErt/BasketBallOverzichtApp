using BasketBallASPNET.Models;
using BusnLogic.Containers;
using DALMSSQLServer;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using BusnLogic.Entity;

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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM vm)
        {
            Gebruiker g = new Gebruiker(vm.FirstName, vm.LastName, vm.GeboorteDatum, vm.Geslacht, vm.Email);
            container.CreateGebruikerAccount(g, vm.Wachtwoord);
            return View();
        }

        [HttpPost]
        public IActionResult Login(InlogVM vm)
        {
            Gebruiker Ingelogde = container.FindByEmailAndPassWordkGebruiker(vm.Email, vm.Wachtwoord);
            if (Ingelogde == null)
            {
                return Content("Gebruiksnaam en/of wachtwoord is verkeerd");
            }
            else
            {
                HttpContext.Session.SetString("Email", Ingelogde.Email);
                HttpContext.Session.SetString("Name", Ingelogde.GetFullName());
                HttpContext.Session.SetInt32("ID", Ingelogde.ID.Value);
            }
            return Redirect("/");
        }
    }
}

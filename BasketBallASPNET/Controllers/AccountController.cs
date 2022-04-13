using BasketBallASPNET.Models;
using BusnLogic.Containers;
using BusnLogic.Entity;
using DALMSSQLServer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BasketBallASPNET.Controllers
{
    public class AccountController : Controller
    {
        private GebruikerContainer container = new GebruikerContainer(new GebruikerMSSQLDAL());

        [HttpPost]
        public IActionResult Index(AccountVM accountVM)
        {
            /*
            if (ModelState.IsValid)
            {
                if (container.controleerOfGebruikerBestaat(accountVM.Email, accountVM.Wachtwoord))
                {
                    Gebruiker gebruiker = container.GetGebruiker(accountVM.Email);

                    var str = JsonConvert.SerializeObject(gebruiker);
                    HttpContext.Session.SetString("gebruiker", str);

                    return Redirect("/Groep/GroepPagina");
                }
                container.AccountAanmaken(accountVM.FirstName, accountVM.Wachtwoord, accountVM.Email);
                ViewData["Success"] = "Account gecreëerd";
                ViewData["Error"] = "Inloggegevens niet correct";
            } */

            return View();

        }
    }
}

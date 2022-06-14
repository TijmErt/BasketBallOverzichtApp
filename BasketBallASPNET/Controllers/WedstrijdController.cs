using BasketBallASPNET.Models;
using BusnLogic;
using BusnLogic.Containers;
using BusnLogic.Entity;
using DALMSSQLServer;
using DALMSSQLServer.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BasketBallASPNET.Controllers
{
    public class WedstrijdController : Controller
    {
        private readonly WedstrijdConainer wc = new WedstrijdConainer(new WedstrijdMSSQLDAL());
        private readonly ClubContainer cc = new ClubContainer(new ClubMSSQLDAL());
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                List<Wedstrijd> wedstrijden = wc.GetAllFromTeam(HttpContext.Session.GetInt32("TeamID").Value);
                List<WedstrijdVM> vm = new List<WedstrijdVM>();
                foreach (Wedstrijd temp in wedstrijden)
                {
                    Club ThuisClub = cc.GetClubDataFromID(temp.thuisClubID);
                    ClubVM ThuisClubVM = new(ThuisClub.ID, ThuisClub.Name);
                    
                    Club UitClub = cc.GetClubDataFromID(temp.uitClubID);
                    ClubVM UitClubVM = new(UitClub.ID, UitClub.Name);

                    vm.Add(new WedstrijdVM(ThuisClubVM, UitClubVM,temp.thuisTeamID, temp.uitTeamID, temp.speelDatum));
                }
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }


        public IActionResult Detail(int WedstrijdID)
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                
                WedstrijdVM WVM = new WedstrijdVM();
                WedstrijdInzienVM vm = new WedstrijdInzienVM();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Account");
            };
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}

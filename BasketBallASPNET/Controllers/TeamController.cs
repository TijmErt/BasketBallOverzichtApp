using Microsoft.AspNetCore.Mvc;
using BusnLogic;
using DALMSSQLServer;
using BasketBallASPNET.Models;
using BusnLogic.Containers;
using BusnLogic.Entity;

namespace BasketBallASPNET.Controllers
{
    public class TeamController : Controller
    {
        private TeamContainer TMcontainer = new TeamContainer(new TeamMSSQLDAL());
        private GebruikerContainer GBcontainter = new GebruikerContainer(new GebruikerMSSQLDAL());
        private int ClubID;

        [HttpGet]
        public IActionResult Index(int clubID)
        {
            ClubID = clubID;
            List<Team> Lt = TMcontainer.GetAllTeamsFromClub(clubID);
            List<TeamVM> Lvm = new List<TeamVM>();
            foreach(Team T in Lt)
            {
                Lvm.Add(new TeamVM(T));
            }
            return View(Lvm);
        }

        [HttpGet]
        public IActionResult Detail(int TeamID)
        {
            List<Gebruiker> Lc = GBcontainter.GetGebruikerFromTeam(TeamID);
            List<SpelerVM> Lvm = new List<SpelerVM>();
            foreach (Gebruiker c in Lc)
            {
                Lvm.Add(new SpelerVM(c));
            }
            return View(Lvm);
        }

        [HttpPost]
        public IActionResult Index(TeamVM vm)
        {
            Team team = new Team(vm.Name, vm.LeeftijdsCategorieID);
            TMcontainer.CreateTeam(team, ClubID);
            return View(ClubID);
        }

    }
}

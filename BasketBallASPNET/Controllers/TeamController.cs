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

        [HttpGet]
        public IActionResult Index(int clubID)
        {
            HttpContext.Session.SetInt32("TempClubID", clubID);
            List<Team> Lt = TMcontainer.GetAllTeamsFromClub(clubID);
            List<TeamVM> Lvm = new List<TeamVM>();
            foreach (Team T in Lt)
            {
                Lvm.Add(new TeamVM(T));
            }
            TeamCreateAndViewVM vm = new TeamCreateAndViewVM(Lvm);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Detail(int TeamID)
        {
            HttpContext.Session.SetInt32("TempTeamID", TeamID);
            List<Gebruiker> Lc = GBcontainter.GetGebruikerFromTeam(TeamID);
            List<SpelerVM> Lvm = new List<SpelerVM>();
            foreach (Gebruiker c in Lc)
            {
                Lvm.Add(new SpelerVM(c));
            }

            return View(Lvm);
        }

        [HttpPost]
        public IActionResult Index(TeamCreateAndViewVM vm)
        {
            Team team = new Team(vm.Name, vm.LeeftijdsCategorieID);
            int clubID = HttpContext.Session.GetInt32("TempClubID").Value;
            TMcontainer.CreateTeam(team, clubID);
            return Redirect("Club");
        }

        [HttpPost]
        public IActionResult Delete(int teamID)
        {
            TMcontainer.DeleteTeam(teamID);

            return Redirect("/Club");

        }

    }
}

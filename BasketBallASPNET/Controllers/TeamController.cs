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
        private readonly TeamContainer TMcontainer = new(new TeamMSSQLDAL());
        private readonly GebruikerContainer GBcontainter = new(new GebruikerMSSQLDAL());

        [HttpGet]
        public IActionResult Index(int clubID)
        {
            HttpContext.Session.SetInt32("TempClubID", clubID);
            List<Team> Lt = TMcontainer.GetAllTeamsFromClub(clubID);
            List<TeamVM> Lvm = new();
            foreach (Team T in Lt)
            {
                Lvm.Add(new TeamVM(T));
            }
            TeamCreateAndViewVM vm = new(Lvm);
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateTeam(TeamCreateAndViewVM vm)
        {
            Team team = new(vm.Name, vm.LeeftijdsCategorieID);
            int ClubID = HttpContext.Session.GetInt32("TempClubID").Value;
            TMcontainer.CreateTeam(team, ClubID);
            return RedirectToAction("Index", new{ clubID = ClubID });
        }

        [HttpPost]
        public IActionResult DeleteTeam(int teamID)
        {
            TMcontainer.DeleteTeam(teamID);
            RedirectToAction("","");
            return RedirectToAction("Index", new { clubID = HttpContext.Session.GetInt32("TempClubID").Value });

        }

        [HttpGet]
        public IActionResult Detail(int TeamID)
        {
            int ClubID = HttpContext.Session.GetInt32("TempClubID").Value;
            if(TMcontainer.CheckClubTeamLink(TeamID, ClubID) == true)
            {
                HttpContext.Session.SetInt32("TempTeamID", TeamID);
                List<Gebruiker> Lc = GBcontainter.GetAllGebruikersFromClub(ClubID);
                List<SpelerVM> Lvm = new();
                foreach (Gebruiker c in Lc)
                {
                    Lvm.Add(new SpelerVM(c));
                }

                return View(Lvm);
            }
            return RedirectToAction("Index", new { ClubID} );

        }

        [HttpPost]
        public IActionResult InsertPlayerToTeam(int SpelerID)
        {
            int teamID = HttpContext.Session.GetInt32("TempTeamID").Value;
            GBcontainter.InsertGebruikerInToTeam(SpelerID, teamID);
            return RedirectToAction("Detail", new { TeamID = teamID });
        }
        [HttpPost]
        public IActionResult RemoveSpelerFromTeam(int SpelerID)
        {
            int teamID = HttpContext.Session.GetInt32("TempTeamID").Value;
            GBcontainter.RemoveSpelerFromTeam(SpelerID);
            return RedirectToAction("Detail", new { TeamID = teamID });
        }

    }
}

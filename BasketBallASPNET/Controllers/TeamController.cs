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
        private readonly TeamContainer TeamContainer = new(new TeamMSSQLDAL());
        private readonly GebruikerContainer GebruikerContainter = new(new GebruikerMSSQLDAL());

        [HttpGet]
        public IActionResult Index(int clubID)
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                HttpContext.Session.SetInt32("TempClubID", clubID);
                List<Team> Lt = TeamContainer.GetAllTeamsFromClub(clubID);
                List<TeamVM> Lvm = new();
                foreach (Team T in Lt)
                {
                    Lvm.Add(new TeamVM(T));
                }
                TeamCreateAndViewVM vm = new(Lvm);
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
            
        }

        [HttpPost]
        public IActionResult CreateTeam(TeamCreateAndViewVM vm)
        {
            Team team = new(vm.Name, vm.LeeftijdsCategorieID);
            int ClubID = HttpContext.Session.GetInt32("TempClubID").Value;
            TeamContainer.CreateTeam(team, ClubID);
            return RedirectToAction("Index", new{ clubID = ClubID });
        }

        [HttpPost]
        public IActionResult DeleteTeam(int teamID)
        {
            TeamContainer.DeleteTeam(teamID);
            return RedirectToAction("Index", new { clubID = HttpContext.Session.GetInt32("TempClubID").Value });

        }

        [HttpGet]
        public IActionResult Detail(int TeamID)
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                int ClubID = HttpContext.Session.GetInt32("TempClubID").Value;
                if (TeamContainer.CheckClubTeamLink(TeamID, ClubID) == true)
                {
                    HttpContext.Session.SetInt32("TempTeamID", TeamID);
                    List<Gebruiker> Lc = GebruikerContainter.GetAllGebruikersFromClub(ClubID);
                    List<SpelerVM> Lvm = new();
                    foreach (Gebruiker c in Lc)
                    {
                        Lvm.Add(new SpelerVM(c));
                    }

                    return View(Lvm);
                }
                return RedirectToAction("Index", new { ClubID });

            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
            
        }

        [HttpPost]
        public IActionResult InsertPlayerToTeam(int SpelerID)
        {
            int teamID = HttpContext.Session.GetInt32("TempTeamID").Value;
            GebruikerContainter.InsertGebruikerInToTeam(SpelerID, teamID);
            return RedirectToAction("Detail", new { TeamID = teamID });
        }
        [HttpPost]
        public IActionResult RemoveSpelerFromTeam(int SpelerID)
        {
            int teamID = HttpContext.Session.GetInt32("TempTeamID").Value;
            GebruikerContainter.RemoveSpelerFromTeam(SpelerID);
            return RedirectToAction("Detail", new { TeamID = teamID });
        }

    }
}

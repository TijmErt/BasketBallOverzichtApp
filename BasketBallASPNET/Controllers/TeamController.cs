using Microsoft.AspNetCore.Mvc;
using BusnLogic;
using DALMSSQLServer;
using BasketBallASPNET.Models;

namespace BasketBallASPNET.Controllers
{
    public class TeamController : Controller
    {
        private TeamContainer container = new TeamContainer(new TeamMSSQLDAL());

        public IActionResult Index()
        {
            List<Team> Lt = container.GetAllTeams();
            List<TeamVM> Lvm = new List<TeamVM>();
            foreach(Team T in Lt)
            {
                Lvm.Add(new TeamVM(T.ID, T.Name));
            }
            return View(Lvm);
        }


        public IActionResult Detail(long id)
        {
            Team t = container.FindByID(id);
            TeamVM vm = new(t.ID, t.Name);
            return View(vm);
        }
 
    }
}

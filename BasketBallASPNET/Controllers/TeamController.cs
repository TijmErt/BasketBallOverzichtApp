using Microsoft.AspNetCore.Mvc;
using BusnLogic;
using DALMSSQLServer;

namespace BasketBallASPNET.Controllers
{
    public class TeamController : Controller
    {
        private TeamContainer container = new TeamContainer(new TeamMSSQLDAL());

        public IActionResult Index()
        {
            return Content("Hier Komt Mijn Team");
        }
        [Route("~/Team/{id?}")]
        public IActionResult TeamDetail(string id)
        {
            long teamId = Convert.ToInt64(id);
            if (teamId == 0)
            {
                List<Team> teams = container.GetAll();
                string content = "";
                foreach (Team team in teams)
                {
                    content += team.ToString();
                }
                return Content(content);
            }
            else
            {
                Team team = container.FindByID(teamId);
                return Content($"{team}");
            }

        }
    }
}

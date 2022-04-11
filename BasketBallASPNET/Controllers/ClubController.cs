using BasketBallASPNET.Models;
using BusnLogic;
using DALMSSQLServer;
using Microsoft.AspNetCore.Mvc;

namespace BasketBallASPNET.Controllers
{
    public class ClubController : Controller
    {
        private ClubContainer container = new ClubContainer(new ClubMSSQLDAL());
        public IActionResult Index()
        {
            List<Club> Lc = container.GetAll();
            List<ClubVM> Lvm = new List<ClubVM>();
            foreach (Club c in Lc)
            {
                Lvm.Add(new ClubVM(c.ID, c.Name));
            }
            return View(Lvm);
        }

        public IActionResult Team()
        {
            return RedirectToAction("Index", "TeamController");
        }
    }
}

using BasketBallASPNET.Models;
using BusnLogic;
using DALException;
using DALMSSQLServer;
using Microsoft.AspNetCore.Mvc;

namespace BasketBallASPNET.Controllers
{
    public class ClubController : Controller
    {
        private ClubContainer ClubContainer = new ClubContainer(new ClubMSSQLDAL());
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                try
                {
                    List<Club> Lc = ClubContainer.GetAll();
                    List<ClubVM> Lvm = new List<ClubVM>();
                    foreach (Club c in Lc)
                    {
                        Lvm.Add(new ClubVM(c.ID, c.Name));
                    }
                    return View(Lvm);
                }
                catch (TemporaryExceptionDAL ex)
                {
                    ViewBag.Error = ex.Message + " PLS try again later";
                    return RedirectToAction("Error", "Home");
                }
                catch (PermanentExceptionDAL ex)
                {
                    return Content(ex.Message);
                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        public IActionResult Team()
        {
            return RedirectToAction("Index", "TeamController");
        }
    }
}

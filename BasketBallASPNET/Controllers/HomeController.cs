using BasketBallASPNET.Models;
using BusnLogic;
using BusnLogic.Containers;
using BusnLogic.Entity;
using DALException;
using DALMSSQLServer;
using DALMSSQLServer.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BasketBallASPNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WedstrijdConainer wc = new WedstrijdConainer(new WedstrijdMSSQLDAL());
        private readonly ClubContainer cc = new ClubContainer(new ClubMSSQLDAL());
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                try
                {
                    List<Wedstrijd> wedstrijden = wc.GetAllFromTeam(HttpContext.Session.GetInt32("TeamID").Value);
                    List<WedstrijdVM> vm = new List<WedstrijdVM>();
                    foreach (Wedstrijd temp in wedstrijden)
                    {
                        Club ThuisClub = cc.GetClubDataFromID(temp.thuisClubID.Value);
                        ClubVM ThuisClubVM = new(ThuisClub.ID, ThuisClub.Name);

                        Club UitClub = cc.GetClubDataFromID(temp.uitClubID.Value);
                        ClubVM UitClubVM = new(UitClub.ID, UitClub.Name);

                        vm.Add(new WedstrijdVM(ThuisClubVM, UitClubVM, temp.thuisTeamID, temp.uitTeamID, temp.speelDatum, temp.ID));
                    }
                    
                    return View(vm.OrderBy(Date => Date.speelDatum).Where(e => e.speelDatum > DateTime.Now).ToList());
                }

                catch (TemporaryExceptionDAL ex)
                {
                    ViewBag.Error = ex.Message + " PLS try again later";
                    return RedirectToAction("Error","Home");
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

        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
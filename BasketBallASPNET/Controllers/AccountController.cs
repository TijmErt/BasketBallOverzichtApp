using BasketBallASPNET.Models;
using BusnLogic.Containers;
using DALMSSQLServer;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using BusnLogic.Entity;
using DALException;
using BusnLogic;

namespace BasketBallASPNET.Controllers
{
    public class AccountController : Controller
    {
        private readonly GebruikerContainer gebruikerContainer = new(new GebruikerMSSQLDAL());
        private readonly ClubContainer clubContainer = new(new ClubMSSQLDAL());
        private readonly TeamContainer teamContainer = new(new TeamMSSQLDAL());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            try
            {
                List<Club> clubs = clubContainer.GetAll();
                List<ClubVM> clubVMs = new();
                foreach (Club club in clubs)
                {
                    clubVMs.Add(new ClubVM(club.ID, club.Name));
                }
                RegisterVM registerVM = new();
                registerVM.Clubs = clubVMs;
                return View(registerVM);
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult RegisterCreate(RegisterVM vm)
        {
            try
            {
                if (vm.Wachtwoord == vm.BevestigWachtwoord)
                {
                    Gebruiker g = new(vm.FirstName, vm.LastName, vm.GeboorteDatum, vm.Geslacht, vm.Email, vm.RoleID, null, vm.ClubID, null);
                    gebruikerContainer.CreateGebruikerAccount(g, vm.Wachtwoord);
                    ViewData["Success"] = "Account gecreëerd";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return RedirectToAction("Register", "Account");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something went wrong" + ex.Message;
                return RedirectToAction("Register", "Account");
            }

        }

        [HttpPost]
        public IActionResult LoginAction(InlogVM vm)
        {
            try
            {
                Gebruiker Ingelogde = gebruikerContainer.FindGebruikerByEmailAndPassWord(vm.Email, vm.Wachtwoord);
                if (Ingelogde == null)
                {
                    ViewData["Error"] = "Inloggegevens niet correct";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    HttpContext.Session.SetString("Email", Ingelogde.Email);
                    HttpContext.Session.SetString("Name", Ingelogde.GetFullName());
                    HttpContext.Session.SetInt32("ID", Ingelogde.ID.Value);
                    HttpContext.Session.SetInt32("RoleID", Ingelogde.RoleID);
                    if (Ingelogde.TeamID.HasValue)
                    {
                        HttpContext.Session.SetInt32("TeamID", Ingelogde.TeamID.Value);
                        HttpContext.Session.SetString("TeamName", teamContainer.GetTeamDataByID(Ingelogde.TeamID.Value).Name);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("TeamID", 0);
                        HttpContext.Session.SetString("TeamName", "");
                    }
                    HttpContext.Session.SetInt32("ClubID", Ingelogde.ClubID.Value);
                    HttpContext.Session.SetString("ClubName", clubContainer.GetClubDataFromID(Ingelogde.ClubID.Value).Name);
                    if (Ingelogde.SpelerNummer.HasValue)
                    {
                        HttpContext.Session.SetInt32("SpelerNummer", Ingelogde.SpelerNummer.Value);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("SpelerNummer", 0);
                    }
                    HttpContext.Session.SetInt32("LoggedIn", 1);
                }
                return Redirect("/");
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


    }
}

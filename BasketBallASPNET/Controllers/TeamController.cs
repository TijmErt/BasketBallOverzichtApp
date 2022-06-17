﻿using Microsoft.AspNetCore.Mvc;
using BusnLogic;
using DALMSSQLServer;
using BasketBallASPNET.Models;
using BusnLogic.Containers;
using BusnLogic.Entity;
using DALException;

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
                try
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
                catch (TemporaryExceptionDAL ex)
                {
                    ViewBag.Error = ex.Message + " PLS try again later";
                    return Redirect("/");
                }
                catch (PermanentExceptionDAL ex)
                {
                    return Content(ex.Message);
                }
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }

        }

        [HttpPost]
        public IActionResult CreateTeam(TeamCreateAndViewVM vm)
        {
            try
            {
                int ClubID = HttpContext.Session.GetInt32("TempClubID").Value;
                Team team = new(vm.Name, vm.LeeftijdsCategorieID, ClubID);
                TeamContainer.CreateTeam(team, ClubID);
                return RedirectToAction("Index", new { clubID = ClubID });
            }
            catch (TemporaryExceptionDAL ex)
            {
                ViewBag.Error = ex.Message + " PLS try again later";
                return RedirectToAction("Index", new { clubID = HttpContext.Session.GetInt32("TempClubID").Value });
            }
            catch (PermanentExceptionDAL ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteTeam(int teamID)
        {
            try
            {
                TeamContainer.DeleteTeam(teamID);
                return RedirectToAction("Index", new { clubID = HttpContext.Session.GetInt32("TempClubID").Value });
            }
            catch (TemporaryExceptionDAL ex)
            {
                ViewBag.Error = ex.Message + " PLS try again later";
                return RedirectToAction("Index", new { clubID = HttpContext.Session.GetInt32("TempClubID").Value });
            }
            catch (PermanentExceptionDAL ex)
            {
                return Content(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult Detail(int TeamID)
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                int ClubID = HttpContext.Session.GetInt32("TempClubID").Value;
                try
                {
                    if (TeamContainer.CheckClubTeamLink(TeamID, ClubID) == true)
                    {
                        HttpContext.Session.SetInt32("TempTeamID", TeamID);
                        List<Gebruiker> Lc = GebruikerContainter.GetAllGebruikersFromClub(ClubID);
                        List<SpelerVM> Lvm = new();
                        foreach (Gebruiker c in Lc)
                        {
                            Lvm.Add(new SpelerVM(c));
                        }

                        return base.View(Lvm);
                    }
                    return RedirectToAction("Index", new { ClubID });
                }
                catch (TemporaryExceptionDAL ex)
                {
                    ViewBag.Error = ex.Message + " PLS try again later";
                    return RedirectToAction("Index", new { clubID = HttpContext.Session.GetInt32("TempClubID").Value });
                }
                catch (PermanentExceptionDAL ex)
                {
                    return Content(ex.Message);
                }

            }
            else
            {
                return RedirectToAction("Index", "Account");
            }

        }

        [HttpPost]
        public IActionResult InsertPlayerToTeam(int SpelerID, int SpelerNummer)
        {
            try
            {
                int teamID = HttpContext.Session.GetInt32("TempTeamID").Value;
                GebruikerContainter.InsertGebruikerInToTeam(SpelerID, teamID, SpelerNummer);
                return RedirectToAction("Detail", new { TeamID = teamID });
            }
            catch (TemporaryExceptionDAL ex)
            {
                ViewBag.Error = ex.Message + " PLS try again later";
                return RedirectToAction("Index", new { clubID = HttpContext.Session.GetInt32("TempClubID").Value });
            }
            catch (PermanentExceptionDAL ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult RemoveSpelerFromTeam(int SpelerID)
        {
            try
            {
                int teamID = HttpContext.Session.GetInt32("TempTeamID").Value;
                GebruikerContainter.RemoveSpelerFromTeam(SpelerID);
                return RedirectToAction("Detail", new { TeamID = teamID });
            }
            catch (TemporaryExceptionDAL ex)
            {
                ViewBag.Error = ex.Message + " PLS try again later";
                return RedirectToAction("Index", new { clubID = HttpContext.Session.GetInt32("TempClubID").Value });
            }
            catch (PermanentExceptionDAL ex)
            {
                return Content(ex.Message);
            }
        }

    }
}

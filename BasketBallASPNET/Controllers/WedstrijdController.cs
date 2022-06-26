using BasketBallASPNET.Models;
using BusnLogic;
using BusnLogic.Containers;
using BusnLogic.Entity;
using DALException;
using DALMSSQLServer;
using DALMSSQLServer.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BasketBallASPNET.Controllers
{
    public class WedstrijdController : Controller
    {
        private readonly WedstrijdConainer wc = new WedstrijdConainer(new WedstrijdMSSQLDAL());
        private readonly ClubContainer cc = new ClubContainer(new ClubMSSQLDAL());
        private readonly GebruikerContainer gc = new GebruikerContainer(new GebruikerMSSQLDAL());
        private readonly TeamContainer tc = new TeamContainer(new TeamMSSQLDAL());
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

                        Team thuisTeam = tc.GetTeamDataByID(temp.thuisTeamID.Value);
                        Team uitTeam = tc.GetTeamDataByID(temp.uitTeamID.Value);

                        TeamVM thuisTeamVM = new(thuisTeam);
                        TeamVM uitTeamVM = new(uitTeam);

                        vm.Add(new WedstrijdVM(ThuisClubVM, UitClubVM, thuisTeamVM, uitTeamVM, temp.speelDatum, temp.ID));
                    }
                    return View(vm);
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


        public IActionResult Detail(int WedstrijdID)
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                try
                {
                    HttpContext.Session.SetInt32("tempWedstrijdID", WedstrijdID);
                    Wedstrijd wedstrijd = wc.GetWedstrijdByID(WedstrijdID);
                    Club thuisClub = cc.GetClubDataFromID(wedstrijd.thuisClubID.Value);
                    Club UitClub = cc.GetClubDataFromID(wedstrijd.uitClubID.Value);

                    Team thuisTeam = tc.GetTeamDataByID(wedstrijd.thuisTeamID.Value);
                    Team uitTeam = tc.GetTeamDataByID(wedstrijd.uitTeamID.Value);

                    TeamVM thuisTeamVM = new(thuisTeam);
                    TeamVM uitTeamVM = new(uitTeam);

                    ClubVM ThuisClubVM = new ClubVM(thuisClub.ID, thuisClub.Name);
                    ClubVM UitClubVM = new ClubVM(UitClub.ID, UitClub.Name);

                    WedstrijdVM WVM = new WedstrijdVM(ThuisClubVM, UitClubVM, thuisTeamVM, uitTeamVM, wedstrijd.speelDatum, wedstrijd.ID);

                    List<Gebruiker> ThuisSpelers = gc.GetAllGebruikersFromWedstrijdID(WedstrijdID).Where(x => x.TeamID == wedstrijd.thuisTeamID).ToList();
                    List<Gebruiker> UitSpelers = gc.GetAllGebruikersFromWedstrijdID(WedstrijdID).Where(x => x.TeamID == wedstrijd.uitTeamID).ToList();
                    List<SpelerVM> ThuisSpelersVM = new List<SpelerVM>();
                    List<SpelerVM> UitSpelersVM = new List<SpelerVM>();

                    foreach (Gebruiker g in ThuisSpelers)
                    {
                        ThuisSpelersVM.Add(new SpelerVM(g.ID, g.FirstName, g.LastName, g.GeboorteDatum, g.Geslacht,
                                                        g.TeamID, g.ClubID, g.Email, g.SpelerNummer,
                                                        wc.GetPresentie(g.ID.Value, WedstrijdID)));
                    }

                    foreach (Gebruiker g in UitSpelers)
                    {
                        UitSpelersVM.Add(new SpelerVM(g.ID, g.FirstName, g.LastName, g.GeboorteDatum, g.Geslacht,
                                                        g.TeamID, g.ClubID, g.Email, g.SpelerNummer,
                                                        wc.GetPresentie(g.ID.Value, WedstrijdID)));

                    }

                    WedstrijdInzienVM vm;
                    try
                    {
                        bool presentie = wc.GetPresentie(HttpContext.Session.GetInt32("ID").Value, WedstrijdID);
                        vm = new WedstrijdInzienVM(WVM, ThuisSpelersVM, UitSpelersVM, presentie);
                    }
                    catch (Exception)
                    {
                        vm = new WedstrijdInzienVM(WVM, ThuisSpelersVM, UitSpelersVM, false);
                    }


                    return View(vm);
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
            };
        }


        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("RoleID") == 1)
                {
                    try
                    {
                        List<Club> AllClubs = cc.GetAll();
                        List<ClubVM> AllClubsVM = new List<ClubVM>();
                        foreach (Club club in AllClubs)
                        {
                            AllClubsVM.Add(new ClubVM(club.ID, club.Name));
                        }
                        List<Team> AllTeams = tc.GetAllTeams();
                        List<TeamVM> AllTeamVM = new List<TeamVM>();
                        foreach (Team team in AllTeams)
                        {
                            AllTeamVM.Add(new TeamVM(team));
                        }

                        WedstrijdCreateVM WedstrijdCVM = new WedstrijdCreateVM(AllClubsVM, AllTeamVM);
                        return View(WedstrijdCVM);
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
                    return RedirectToAction("Index");
                }
                
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Create(WedstrijdCreateVM vm)
        {
            try
            {
                if (vm.ThuisTeamID != vm.UitTeamID || vm.UitCLubID != vm.ThuisCLubID)
                {
                    int WedstrijdID = wc.CreateWedstrijd(new Wedstrijd(vm.ThuisCLubID, vm.UitCLubID, vm.ThuisTeamID, vm.UitTeamID, vm.speelDatum));
                    List<int> WedstrijdSpelers = gc.GetGebruikerIDFromWedstrijdTeams(vm.ThuisTeamID.Value, vm.UitTeamID.Value);
                    foreach (int i in WedstrijdSpelers)
                    {
                        wc.AddSpelerToeWedstrijd(i, WedstrijdID);
                    }
                    return RedirectToAction("Index", "Wedstrijd");
                }
                else
                {
                    ViewBag.Error = "Teams of CLubs Mogen net hetzelfde zijn";
                    return RedirectToAction("Create");
                }

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


        public IActionResult UpdatePresentie(bool presentie)
        {
            try
            {
                wc.UpdatePresentie(HttpContext.Session.GetInt32("ID").Value, HttpContext.Session.GetInt32("tempWedstrijdID").Value, presentie);
                return RedirectToAction("Detail", new { WedstrijdID = HttpContext.Session.GetInt32("tempWedstrijdID") });
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

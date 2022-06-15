using BasketBallASPNET.Models;
using BusnLogic;
using BusnLogic.Containers;
using BusnLogic.Entity;
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
                List<Wedstrijd> wedstrijden = wc.GetAllFromTeam(HttpContext.Session.GetInt32("TeamID").Value);
                List<WedstrijdVM> vm = new List<WedstrijdVM>();
                foreach (Wedstrijd temp in wedstrijden)
                {
                    Club ThuisClub = cc.GetClubDataFromID(temp.thuisClubID);
                    ClubVM ThuisClubVM = new(ThuisClub.ID, ThuisClub.Name);

                    Club UitClub = cc.GetClubDataFromID(temp.uitClubID);
                    ClubVM UitClubVM = new(UitClub.ID, UitClub.Name);

                    vm.Add(new WedstrijdVM(ThuisClubVM, UitClubVM, temp.thuisTeamID, temp.uitTeamID, temp.speelDatum, temp.ID  ));
                }
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }


        public IActionResult Detail(int WedstrijdID)
        {
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                Wedstrijd wedstrijd = wc.GetWedstrijdByID(WedstrijdID);
                Club thuisClub = cc.GetClubDataFromID(wedstrijd.thuisClubID);
                Club UitClub = cc.GetClubDataFromID(wedstrijd.uitClubID);

                ClubVM ThuisClubVM = new ClubVM(thuisClub.ID, thuisClub.Name);
                ClubVM UitClubVM = new ClubVM(UitClub.ID, UitClub.Name);

                WedstrijdVM WVM = new WedstrijdVM(ThuisClubVM, UitClubVM, wedstrijd.thuisTeamID, wedstrijd.uitTeamID, wedstrijd.speelDatum, wedstrijd.ID);

                List<Gebruiker> ThuisSpelers = gc.GetGebruikersFromTeam(wedstrijd.thuisTeamID);
                List<Gebruiker> UitSpelers = gc.GetGebruikersFromTeam(wedstrijd.uitTeamID);
                List<SpelerVM> ThuisSpelersVM = new List<SpelerVM>();
                List<SpelerVM> UitSpelersVM = new List<SpelerVM>();

                foreach (Gebruiker g in ThuisSpelers)
                {
                    ThuisSpelersVM.Add(new SpelerVM(g));
                }

                foreach (Gebruiker g in UitSpelers)
                {
                    UitSpelersVM.Add(new SpelerVM(g));
                }

                bool presentie = wc.GetPresentie(HttpContext.Session.GetInt32("ID").Value, WedstrijdID);

                WedstrijdInzienVM vm = new WedstrijdInzienVM(WVM, ThuisSpelersVM, UitSpelersVM, presentie);
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            };
        }

        public IActionResult Create()
        {
            Club thuisClub = cc.GetClubDataFromID(HttpContext.Session.GetInt32("ClubID").Value);
            ClubVM ThuisClubVM = new(thuisClub.ID, thuisClub.Name);
            List<Club> AllClubs = cc.GetAll();
            List<ClubVM> AllClubsVM = new List<ClubVM>();
            foreach(Club club in AllClubs)
            {
                AllClubsVM.Add(new ClubVM(club.ID, club.Name));
            }
            List<Team> AllTeams = tc.GetAllTeams();
            List<TeamVM> AllTeamVM = new List<TeamVM>();
            foreach(Team team in AllTeams)
            {
                AllTeamVM.Add(new TeamVM(team));
            }

            WedstrijdCreateVM WedstrijdCVM = new WedstrijdCreateVM(ThuisClubVM, null, AllClubsVM, AllTeamVM, null);
            return View(WedstrijdCVM);
        }
    }
}

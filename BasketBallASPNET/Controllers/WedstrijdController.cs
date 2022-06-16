﻿using BasketBallASPNET.Models;
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
                    Club ThuisClub = cc.GetClubDataFromID(temp.thuisClubID.Value);
                    ClubVM ThuisClubVM = new(ThuisClub.ID, ThuisClub.Name);

                    Club UitClub = cc.GetClubDataFromID(temp.uitClubID.Value);
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
                Club thuisClub = cc.GetClubDataFromID(wedstrijd.thuisClubID.Value);
                Club UitClub = cc.GetClubDataFromID(wedstrijd.uitClubID.Value);

                ClubVM ThuisClubVM = new ClubVM(thuisClub.ID, thuisClub.Name);
                ClubVM UitClubVM = new ClubVM(UitClub.ID, UitClub.Name);

                WedstrijdVM WVM = new WedstrijdVM(ThuisClubVM, UitClubVM, wedstrijd.thuisTeamID, wedstrijd.uitTeamID, wedstrijd.speelDatum, wedstrijd.ID);

                List<Gebruiker> ThuisSpelers = gc.GetGebruikersFromTeam(wedstrijd.thuisTeamID.Value);
                List<Gebruiker> UitSpelers = gc.GetGebruikersFromTeam(wedstrijd.uitTeamID.Value);
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

                WedstrijdInzienVM vm;
                try
                {
                    bool presentie = wc.GetPresentie(HttpContext.Session.GetInt32("ID").Value, WedstrijdID);
                     vm = new WedstrijdInzienVM(WVM, ThuisSpelersVM, UitSpelersVM, presentie);
                }
                catch(Exception)
                {
                     vm = new WedstrijdInzienVM(WVM, ThuisSpelersVM, UitSpelersVM, false);
                }

                
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            };
        }


        [HttpGet]
        public IActionResult Create()
        {
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

            WedstrijdCreateVM WedstrijdCVM = new WedstrijdCreateVM( AllClubsVM, AllTeamVM);
            return View(WedstrijdCVM);
        }

        [HttpPost]
        public IActionResult Create(WedstrijdCreateVM vm)
        {
            wc.CreateWedstrijd(new Wedstrijd(vm.ThuisCLubID, vm.UitCLubID, vm.ThuisTeamID, vm.UiTeamID, vm.speelDatum));

            return RedirectToAction("Index", "Wedstrijd");
        }
    }
}

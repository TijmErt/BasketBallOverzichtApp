﻿using BasketBallASPNET.Models;
using BusnLogic.Containers;
using DALMSSQLServer;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using BusnLogic.Entity;

namespace BasketBallASPNET.Controllers
{
    public class AccountController : Controller
    {
        private readonly GebruikerContainer container = new (new GebruikerMSSQLDAL());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
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
            if(vm.Wachtwoord == vm.BevestigWachtwoord)
            {
                Gebruiker g = new(vm.FirstName, vm.LastName, vm.GeboorteDatum, vm.Geslacht, vm.Email, 3, null, vm.ClubID);
                container.CreateGebruikerAccount(g, vm.Wachtwoord);
                ViewData["Success"] = "Account gecreëerd";
                return View();
            }
            else
            {
                ViewData["Error"] = "Wachtwoord komt niet overheen";
            }

            return RedirectToAction("Register", "Account");


        }

        [HttpPost]
        public IActionResult LoginAction(InlogVM vm)
        {
            Gebruiker Ingelogde = container.FindGebruikerByEmailAndPassWord(vm.Email, vm.Wachtwoord);
            if (Ingelogde == null)
            {
                ViewData["Error"] = "Inloggegevens niet correct";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("Email", Ingelogde.Email);
                HttpContext.Session.SetString("Name", Ingelogde.GetFullName());
                HttpContext.Session.SetInt32("ID", Ingelogde.ID.Value);
                HttpContext.Session.SetInt32("RoleID", Ingelogde.RoleID);
            }
            return Redirect("/");
        }


    }
}

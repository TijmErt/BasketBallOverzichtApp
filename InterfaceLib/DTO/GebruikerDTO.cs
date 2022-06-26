﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib.DTO
{
    public class GebruikerDTO
    {
        public int? ID;
        public string FirstName;
        public string LastName;
        public string Geslacht;
        public DateTime GeboorteDatum;
        public string Email;
        public int RoleID;
        public int? TeamID;
        public int? ClubID;
        public int? SpelerNummer;


        public GebruikerDTO(string firstName, string lastName, DateTime geboorteDatum, string geslacht,
                            string email, int roleID, int? teamID, int? clubID, int? SpelerNummer, int? id = null)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.GeboorteDatum = geboorteDatum;
            this.Geslacht = geslacht;
            this.Email = email;
            this.RoleID = roleID;
            this.TeamID = teamID;
            this.ClubID = clubID;
            this.SpelerNummer = SpelerNummer;
        }

        public GebruikerDTO()
        {

        }
    }
}

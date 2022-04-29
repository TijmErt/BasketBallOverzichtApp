
using InterfaceLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic.Entity
{
    public class Gebruiker
    {
        public int? ID;
        public string FirstName;
        public string LastName;
        public string Geslacht;
        public DateTime GeboorteDatum;
        public string Email;

        public Gebruiker(string firstName, string lastName, DateTime geboorteDatum, string geslacht, string email, int? id = null)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.GeboorteDatum = geboorteDatum;
            this.Geslacht = geslacht;
            this.Email = email;
        }
        public Gebruiker(GebruikerDTO tempDTO)
        {
            this.ID = tempDTO.ID;
            this.FirstName = tempDTO.FirstName;
            this.LastName = tempDTO.LastName;
            this.GeboorteDatum = tempDTO.GeboorteDatum;
            this.Geslacht = tempDTO.Geslacht;
            this.Email = tempDTO.Email;
        }

        public GebruikerDTO GetDTO()
        {
            return new GebruikerDTO(FirstName, LastName, GeboorteDatum, Geslacht, Email, ID);
        }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}

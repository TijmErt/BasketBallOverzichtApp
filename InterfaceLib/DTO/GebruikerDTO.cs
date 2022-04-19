using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib.DTO
{
    public class GebruikerDTO
    {
        public long ID;
        public string FirstName;
        public string LastName;
        public string Geslacht;
        public DateTime GeboorteDatum;
        public string Email;

        public GebruikerDTO(long id, string firstName, string lastName, DateTime geboorteDatum, string geslacht, string wachtwoord, string email)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.GeboorteDatum = geboorteDatum;
            this.Geslacht = geslacht;
            this.Email = email;

        }
        public GebruikerDTO(long id, string firstName, string lastName, DateTime geboorteDatum, string geslacht, string email)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.GeboorteDatum = geboorteDatum;
            this.Geslacht = geslacht;
            this.Email = email;
        }
    }
}

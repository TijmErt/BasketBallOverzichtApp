using BusnLogic.Entity;
using InterfaceLib.DTO;
using InterfaceLib.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic.Containers
{
    public class GebruikerContainer
    {
        IGebruikerContainer Container;

        public GebruikerContainer(IGebruikerContainer container)
        {
            Container = container;
        }

        public bool controleerOfGebruikerBestaat(string gebruikersnaam, string wachtwoord)
        {
            bool check;

            check = Container.CheckGebruiker(gebruikersnaam, wachtwoord);

            return check;
        }
        public Gebruiker GetGebruiker(string Email)
        {
            GebruikerDTO dto= Container.GetGebruiker(Email);
            return new Gebruiker(dto);
        }
    }
}

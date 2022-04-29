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

        /* public Gebruiker controleerOfGebruikerBestaat(string gebruikersnaam, string wachtwoord)
        {
            bool check;

            check = Container.CheckGebruiker(gebruikersnaam, wachtwoord);

            return check;
        }*/

        public Gebruiker GetGebruiker(string Email)
        {
            GebruikerDTO dto = Container.GetGebruiker(Email);
            return new Gebruiker(dto);
        }
        public List<Gebruiker> GetGebruikerFromTeam(int TeamID)
        {
            List<Gebruiker> list = new List<Gebruiker>();
            foreach (GebruikerDTO Item in Container.GetGebruikerFromTeam(TeamID))
            {
                list.Add(new Gebruiker(Item));
            }
            return list;
        }

        public void CreateGebruikerAccount(Gebruiker gebruiker, string wachtwoord)
        {
            GebruikerDTO dto = gebruiker.GetDTO();
            Container.CreateGebruikerAccount(dto, wachtwoord);
        }

        public Gebruiker FindByEmailAndPassWordkGebruiker(string Email, string wachtwoord)
        {
            GebruikerDTO dto = Container.FindByEmailAndPassWordkGebruiker(Email, wachtwoord);
            if (dto == null)
            {
                return null;
            }
            return new Gebruiker(dto);
        }
    }
}

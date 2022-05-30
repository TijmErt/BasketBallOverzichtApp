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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public GebruikerContainer(IGebruikerContainer container)
        {
            Container = container;
        }

        public Gebruiker GetGebruiker(string Email)
        {
            GebruikerDTO dto = Container.GetGebruiker(Email);
            return new Gebruiker(dto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TeamID"></param>
        /// <returns></returns>
        public List<Gebruiker> GetGebruikerFromTeam(int TeamID)
        {
            List<Gebruiker> list = new List<Gebruiker>();
            foreach (GebruikerDTO Item in Container.GetGebruikerFromTeam(TeamID))
            {
                list.Add(new Gebruiker(Item));
            }
            return list;

        }
        /// <summary>
        /// Hier mee haal je alle leden van een specifieke club op.
        /// </summary>
        /// <param name="ClubID">hier moet de ID van de club in komen</param>
        /// <returns></returns>
        public List<Gebruiker> GetAllFromClub(int ClubID)
        {
            List<Gebruiker> list = new List<Gebruiker>();
            foreach (GebruikerDTO Item in Container.GetAllFromClub(ClubID))
            {
                list.Add(new Gebruiker(Item));
            }
            return list;
        }

        /// <summary>
        /// Hier mee wordt er een account voor een gebruiker gemaakt in de database
        /// </summary>
        /// <param name="dto">geef hier een variable mee van het type gebruikerDTO</param>
        /// <param name="wachtwoord" >Geef hier de een string mee voor het wachtwoord van de gebruiker</param>
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

        public void InsertGebruikerInToTeam(int SpelerID, int TeamID)
        {
            Container.InsertGebruikerInToTeam(SpelerID, TeamID);
        }

        public void RemoveSpelerFromTeam(int SpelerID)
        {
            Container.RemoveGebruikerFromTeam(SpelerID);
        }
    }
}

using InterfaceLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib.InterFaces
{
    public interface IGebruikerContainer
    {
        public GebruikerDTO FindByEmailAndPassWordkGebruiker(string Email, string wachtwoord);
        public GebruikerDTO GetGebruiker(string Email);
        public List<GebruikerDTO> GetAllFromClub(int ClubID);
        /// <summary>
        /// Hier mee wordt er een account voor een gebruiker gemaakt in de database
        /// </summary>
        /// <param name="dto">geef hier een variable mee van het type gebruikerDTO</param>
        /// <param name="wachtwoord" >Geef hier de een string mee voor het wachtwoord van de gebruiker</param>
        public void CreateGebruikerAccount(GebruikerDTO dto, string wachtwoord);

        public List<GebruikerDTO> GetGebruikerFromTeam(int TeamID);

        public void InsertGebruikerInToTeam(int SpelerID, int TeamID);

        public void RemoveGebruikerFromTeam(int SpelerID);
    }
}

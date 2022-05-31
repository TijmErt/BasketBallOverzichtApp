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
        /// <summary>
        /// Hier word er met een account ingelogd door te kijken of er een email is die overeen komt met het wachtwoord
        /// </summary>
        /// <param name="Email">Hier word de email van de gebruiker ingevoerd</param>
        /// <param name="wachtwoord">Hier word het wachtwoord van de gebruiker ingevoerd</param>
        /// <returns>Het geeft en GebruikerDTO terug</returns>
        public GebruikerDTO FindGebruikerByEmailAndPassWord(string Email, string wachtwoord);

        /// <summary>
        /// Hier word een gerbuiker terug gegeven als je een email mee geeft
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>er word een gerbuikerDTO terug gegegeven</returns>
        /// <exception cref="Exception"></exception>
        public GebruikerDTO GetGebruikerByEmail(string Email);

        /// <summary>
        /// Hier mee haal je alle leden van een specifieke club op.
        /// </summary>
        /// <param name="ClubID">hier moet de ID van de club in komen</param>
        /// <returns>Het geeft een lijst van gebruikers terug die in de gegeven club zitten</returns>
        public List<GebruikerDTO> GetAllGebruikersFromClub(int ClubID);

        /// <summary>
        /// Hier mee wordt er een account voor een gebruiker gemaakt in de database
        /// </summary>
        /// <param name="dto">geef hier een variable mee van het type gebruikerDTO</param>
        /// <param name="wachtwoord">Geef hier de een string mee voor het wachtwoord van de gebruiker</param>
        public void CreateGebruikerAccount(GebruikerDTO dto, string wachtwoord);

        /// <summary>
        /// Hier mee haal je ale gebruikers op die in een team zitten
        /// </summary>
        /// <param name="TeamID">Geef hier het ID van de Team mee</param>
        /// <returns>Het geeft een lijst van Gebruikers die in de gegeven team zitten</returns>
        public List<GebruikerDTO> GetGebruikerFromTeam(int TeamID);

        /// <summary>
        /// Hier mee zet je een gebruiker in een Team
        /// </summary>
        /// <param name="GebruikerID">Geef hier de Gebruiker ID mee</param>
        /// <param name="TeamID">Geef hier de Team ID mee</param>
        public void InsertGebruikerInToTeam(int GebruikerID, int TeamID);

        /// <summary>
        /// Hier mee haal je een Gebruiker uit een team
        /// </summary>
        /// <param name="GebruikerID">Geef hier de ID mee van de speler die er uit moet</param>
        public void RemoveGebruikerFromTeam(int GebruikerID);
    }
}

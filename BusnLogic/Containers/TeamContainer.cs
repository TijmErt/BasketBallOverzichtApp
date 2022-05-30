using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic
{
    public class TeamContainer  
    {
        private readonly ITeamContainer container;
        public TeamContainer(ITeamContainer containerTemp)
        {
            this.container = containerTemp;
        }

        /// <summary>
        /// Hier kun je de team gegevens krijgen met het teamID
        /// </summary>
        /// <param name="id">je geeft hier de team id mee</param>
        /// <returns>Geeft een Team terug</returns>
        public Team GetTeamDataByID(int id)
        {
            TeamDTO dto = container.GetTeamDataByID(id);
            if(dto == null)
            {
                return null;
            }
            return new Team(dto);
        }


        /// <summary>
        /// je haalt hier alle teams op die er bestaan
        /// </summary>
        /// <returns>geeft een lijst van teams terug</returns>
        public List<Team> GetAllTeams()
        {
            List<Team> list = new();
            foreach (TeamDTO item in container.GetAllTeams())
            {
                list.Add(new Team(item));
            }
            return list;
        }

        /// <summary>
        /// Hier haal je alle teams van een club op
        /// </summary>
        /// <param name="ClubID">je geeft hier de club ID mee</param>
        /// <returns>Geeft een lijst van Teams terug</returns>
        public List<Team> GetAllTeamsFromClub(int ClubID)
        {
            List<Team> list = new();
            foreach (TeamDTO item in container.GetAllTeamsFromClub(ClubID))
            {
                list.Add(new Team(item));
            }
            return list;
        }

        /// <summary>
        /// Hier creeër je een team voor je club
        /// </summary>
        /// <param name="temp">Je geeft hier een dto met de team gegevens mee</param>
        /// <param name="ClubID">Je geeft hier de club id mee waar de team aan wordt toegevoegd</param>
        public void CreateTeam(Team temp, int ClubID)
        {
            TeamDTO dto = temp.GetDTO();
            container.CreateTeam(dto, ClubID);
        }

        /// <summary>
        /// Hier verwijder je een team 
        /// </summary>
        /// <param name="TeamID">je geeft hier de team id mee van het team dat verwijderd wordt</param>
        public void DeleteTeam(int TeamID)
        {
            container.DeleteTeam(TeamID);
        }

        /// <summary>
        /// Checked of de team wel bij de club hoort
        /// </summary>
        /// <param name="TeamID">hier geef je de team id mee</param>
        /// <param name="ClubID">hier geef je de club id mee</param>
        /// <returns>Geeft een Boolean terug ((false als het niet bestaat), (true als het wel bestaat))</returns>
        public bool CheckClubTeamLink(int TeamID, int ClubID)
        {
            return container.CheckClubTeamLink(TeamID, ClubID);
        }
    }
}

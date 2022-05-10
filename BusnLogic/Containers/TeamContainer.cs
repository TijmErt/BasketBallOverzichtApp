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
        private ITeamContainer container;
        public TeamContainer(ITeamContainer containerTemp)
        {
            this.container = containerTemp;
        }

        public Team FindByID(int id)
        {
            TeamDTO dto = container.FindByID(id);
            if(dto == null)
            {
                return null;
            }
            return new Team(dto);
        }

        public List<Team> GetAllTeams()
        {
            List<Team> list = new List<Team>();
            foreach (TeamDTO item in container.GetAllTeams())
            {
                list.Add(new Team(item));
            }
            return list;
        }

        public List<Team> GetAllTeamsFromClub(int clubID)
        {
            List<Team> list = new List<Team>();
            foreach (TeamDTO item in container.GetAllTeamsFromClub(clubID))
            {
                list.Add(new Team(item));
            }
            return list;
        }
        public void CreateTeam(Team temp, int ClubID)
        {
            TeamDTO dto = temp.GetDTO();
            container.CreateTeam(dto, ClubID);
        }

        public void DeleteTeam(int TeamID)
        {
            container.DeleteTeam(TeamID);
        }
    }
}

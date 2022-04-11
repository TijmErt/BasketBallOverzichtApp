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

        public Team FindByID(long id)
        {
            TeamDTO dto = container.FindByID(id);
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
    }
}

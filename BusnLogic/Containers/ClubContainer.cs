using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic
{
    public class ClubContainer
    {
        private readonly IClubContainer container;
        public ClubContainer(IClubContainer containerTemp)
        {
            this.container = containerTemp;
        }
        /// <summary>
        /// Je haalt hier mee gegevens op van de meegegven club ID
        /// </summary>
        /// <param name="ClubID">Je voert hier je club ID in</param>
        /// <returns>Het geeft een Club terug</returns>
        public Club? GetClubDataFromID(int ClubID)
        {
            ClubDTO dto = container.GetClubDataFromID(ClubID);
            if(dto == null)
            {
                return null;
            }
            return new Club(dto);
        }

        /// <summary>
        /// Hier haal je alle clubs op die er bestaan
        /// </summary>
        /// <returns>Het geeft een lijst van Club's terug</returns>
        public List<Club> GetAll()
        {
            List<Club> list = new();
            foreach (ClubDTO item in container.GetAll())
            {
                list.Add(new Club(item));
            }
            return list;
        }
    }
}

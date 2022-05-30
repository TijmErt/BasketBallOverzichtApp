using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public interface IClubContainer
    {
        /// <summary>
        /// Je haalt hier mee gegevens op van de meegegven club ID
        /// </summary>
        /// <param name="ClubID">Je voert hier je club ID in</param>
        /// <returns>Het geeft een ClubDTO terug</returns>
        public ClubDTO GetClubDataFromID(int ClubID);

        /// <summary>
        /// Hier haal je alle clubs op die er bestaan
        /// </summary>
        /// <returns>Het geeft een lijst van ClubDTO's terug</returns>
        public List<ClubDTO> GetAll();
    }
}

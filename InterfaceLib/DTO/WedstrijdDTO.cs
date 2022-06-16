using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib.DTO
{
    public class WedstrijdDTO
    {
        public int? ID;

        public int? thuisClubID;

        public int? uitClubID;

        public int? thuisTeamID;

        public int? uitTeamID;

        public DateTime? speelDatum;

        public WedstrijdDTO( int? thuisClubID, int? uitClubID, int? thuisTeamID, int? uitTeamID, DateTime? speelDatum, int? iD = null)
        {
            ID = iD;
            this.thuisClubID = thuisClubID;
            this.uitClubID = uitClubID;
            this.thuisTeamID = thuisTeamID;
            this.uitTeamID = uitTeamID;
            this.speelDatum = speelDatum;
        }
    }
}

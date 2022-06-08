using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic.Entity
{
    public class Wedstrijd
    {
        public int thuisClubID;

        public int uitClubID;

        public int thuisTeamID;

        public int uitTeamID;

        public DateTime speelDatum;

        public Wedstrijd(int thuisClubID, int uitClubID, int thuisTeamID, int uitTeamID, DateTime speelDatum)
        {
            this.thuisClubID = thuisClubID;
            this.uitClubID = uitClubID;
            this.thuisTeamID = thuisTeamID;
            this.uitTeamID = uitTeamID;
            this.speelDatum = speelDatum;
        }
    }
}

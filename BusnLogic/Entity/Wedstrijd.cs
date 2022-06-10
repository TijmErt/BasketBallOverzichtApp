using InterfaceLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogic.Entity
{
    public class Wedstrijd
    {
        public int? ID;

        public int thuisClubID;

        public int uitClubID;

        public int thuisTeamID;

        public int uitTeamID;

        public DateTime speelDatum;

        public Wedstrijd(int thuisClubID, int uitClubID, int thuisTeamID,
                        int uitTeamID, DateTime speelDatum, int? ID = null)
        {
            this.thuisClubID = thuisClubID;
            this.uitClubID = uitClubID;
            this.thuisTeamID = thuisTeamID;
            this.uitTeamID = uitTeamID;
            this.speelDatum = speelDatum;
            this.ID = ID;
        }

        public Wedstrijd(WedstrijdDTO TempDTO)
        {
            this.thuisClubID = TempDTO.thuisClubID;
            this.uitClubID = TempDTO.uitClubID;
            this.thuisTeamID = TempDTO.thuisTeamID;
            this.uitTeamID = TempDTO.uitTeamID;
            this.speelDatum = TempDTO.speelDatum;
            this.ID = TempDTO.ID;
        }

        public WedstrijdDTO GetDTO()
        {
            return new WedstrijdDTO(thuisClubID, uitClubID, thuisTeamID, uitTeamID, speelDatum, ID);
        }
    }
}

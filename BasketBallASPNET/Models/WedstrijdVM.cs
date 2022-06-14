﻿namespace BasketBallASPNET.Models
{
    public class WedstrijdVM
    {
        public int? ID { get; set; }

        public ClubVM? thuisClub { get; set; }

        public ClubVM? uitClub { get; set; }

        public int? thuisTeamID { get; set; }

        public int? uitTeamID { get; set; }

        public DateTime? speelDatum { get; set; }

        public WedstrijdVM(ClubVM thuisClubID, ClubVM uitClubID, int thuisTeamID,
                        int uitTeamID, DateTime speelDatum, int? ID = null)
        {
            this.thuisClub = thuisClubID;
            this.uitClub = uitClubID;
            this.thuisTeamID = thuisTeamID;
            this.uitTeamID = uitTeamID;
            this.speelDatum = speelDatum;
            this.ID = ID;
        }

        public WedstrijdVM()
        {

        }
    }
}

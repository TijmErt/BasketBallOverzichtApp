namespace BasketBallASPNET.Models
{
    public class WedstrijdVM
    {
        public int? ID { get; set; }

        public ClubVM? thuisClub { get; set; }

        public ClubVM? uitClub { get; set; }

        public int? thuisTeamID { get; set; }

        public int? uitTeamID { get; set; }

        public DateTime? speelDatum { get; set; }

        public WedstrijdVM( ClubVM? thuisClub, ClubVM? uitClub, int? thuisTeamID, int? uitTeamID, DateTime? speelDatum, int? iD= null)
        {
            ID = iD;
            this.thuisClub = thuisClub;
            this.uitClub = uitClub;
            this.thuisTeamID = thuisTeamID;
            this.uitTeamID = uitTeamID;
            this.speelDatum = speelDatum;
        }

        public WedstrijdVM()
        {

        }
    }
}

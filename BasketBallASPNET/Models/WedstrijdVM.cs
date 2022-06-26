namespace BasketBallASPNET.Models
{
    public class WedstrijdVM
    {
        public int? ID { get; set; }

        public ClubVM? thuisClub { get; set; }

        public ClubVM? uitClub { get; set; }

        public TeamVM? thuisTeam { get; set; }

        public TeamVM? uitTeam { get; set; }

        public DateTime? speelDatum { get; set; }

        public WedstrijdVM( ClubVM? thuisClub, ClubVM? uitClub, TeamVM? thuisTeam, TeamVM? uitTeam, DateTime? speelDatum, int? iD= null)
        {
            ID = iD;
            this.thuisClub = thuisClub;
            this.uitClub = uitClub;
            this.thuisTeam = thuisTeam;
            this.uitTeam = uitTeam;
            this.speelDatum = speelDatum;
        }

        public WedstrijdVM()
        {

        }
    }
}

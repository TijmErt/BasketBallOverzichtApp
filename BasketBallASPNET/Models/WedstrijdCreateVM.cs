namespace BasketBallASPNET.Models
{
    public class WedstrijdCreateVM
    {
        public int? ID { get; set; }

        public int? thuisClubID { get; set; }

        public int? uitClubID { get; set; }

        public int? thuisTeamID { get; set; }

        public int? uitTeamID { get; set; }

        public DateTime? speelDatum { get; set; }

        public WedstrijdCreateVM(int thuisClubID, int uitClubID, int thuisTeamID,
                        int uitTeamID, DateTime speelDatum, int? ID = null)
        {
            this.thuisClubID = thuisClubID;
            this.uitClubID = uitClubID;
            this.thuisTeamID = thuisTeamID;
            this.uitTeamID = uitTeamID;
            this.speelDatum = speelDatum;
            this.ID = ID;
        }

        public WedstrijdCreateVM()
        {

        }
    }
}

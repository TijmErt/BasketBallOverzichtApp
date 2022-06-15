namespace BasketBallASPNET.Models
{
    public class WedstrijdCreateVM
    {
        public ClubVM? ThuisCLub { get; set; }
        public ClubVM? UitCLub { get; set; }
        public List<ClubVM>? Clubs { get; set; }
        public List<TeamVM>? Teams { get; set; }
        public DateTime? speelDatum { get; set; }

        public WedstrijdCreateVM(ClubVM? thuisCLub, ClubVM? uitCLub, List<ClubVM>? clubs, List<TeamVM>? teams, DateTime? speelDatum)
        {
            ThuisCLub = thuisCLub;
            UitCLub = uitCLub;
            Clubs = clubs;
            Teams = teams;
            this.speelDatum = speelDatum;
        }

        public WedstrijdCreateVM()
        {

        }
    }
}

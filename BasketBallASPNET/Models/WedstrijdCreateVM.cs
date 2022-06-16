namespace BasketBallASPNET.Models
{
    public class WedstrijdCreateVM
    {
        public int? ThuisCLubID { get; set; }
        public int? UitCLubID { get; set; }
        public int? ThuisTeamID { get; set; }
        public int? UiTeamID { get; set; }
        public List<ClubVM>? Clubs { get; set; }
        public List<TeamVM>? Teams { get; set; }
        public DateTime? speelDatum { get; set; }

        public WedstrijdCreateVM(List<ClubVM>? clubs, List<TeamVM>? teams)
        {
            Clubs = clubs;
            Teams = teams;
        }

        public WedstrijdCreateVM()
        {

        }
    }
}

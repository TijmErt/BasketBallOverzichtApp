namespace BasketBallASPNET.Models
{
    public class WedstrijdInzienVM
    {
        public WedstrijdVM wedstrijd { get; set; }

        public List<SpelerVM> ThuisTeam { get; set; }
        public List<SpelerVM> UitTeam { get; set; }
        public bool Presentie { get; set; }

        public WedstrijdInzienVM(WedstrijdVM wedstrijd, List<SpelerVM> thuisTeam, List<SpelerVM> uitTeam, bool presentie)
        {
            this.wedstrijd = wedstrijd;
            ThuisTeam = thuisTeam;
            UitTeam = uitTeam;
            Presentie = presentie;
        }

        public WedstrijdInzienVM()
        {

        }
    }
}

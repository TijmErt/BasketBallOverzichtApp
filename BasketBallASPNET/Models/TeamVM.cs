namespace BasketBallASPNET.Models
{
    public class TeamVM
    {
        public long ID;
        public string Name;

        public TeamVM(long iD, string name)
        {
            ID = iD;
            Name = name;
        }
        public TeamVM()
        {

        }
    }
}

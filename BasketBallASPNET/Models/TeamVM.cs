namespace BasketBallASPNET.Models
{
    public class TeamVM
    {
        public int ID;
        public string Name;

        public TeamVM(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public TeamVM()
        {

        }
    }
}

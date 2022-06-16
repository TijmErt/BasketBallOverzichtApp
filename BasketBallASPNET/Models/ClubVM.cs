namespace BasketBallASPNET.Models
{
    public class ClubVM
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ClubVM(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public ClubVM()
        {

        }
    }
}

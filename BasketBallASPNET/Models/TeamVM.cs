using BusnLogic;

namespace BasketBallASPNET.Models
{
    public class TeamVM
    {
        public int? ID { get; set; }
        public int? ClubID { get; set; }
        public string? Name { get; set; }
        public int? LeeftijdsCategorieID { get; set; }

        public TeamVM(string? name, int? leeftijdsCategorieID, int? clubID, int? id = null)
        {
            ID = id;
            Name = name;
            LeeftijdsCategorieID = leeftijdsCategorieID;
            ClubID = clubID;
        }
        public TeamVM(Team temp)
        {
            this.ID = temp.ID;
            this.Name = temp.Name;
            this.LeeftijdsCategorieID = temp.LeeftijdsCategorieID;
            this.ClubID = temp.ClubID;
        }
        
        public TeamVM()
        {

        }
    }
}

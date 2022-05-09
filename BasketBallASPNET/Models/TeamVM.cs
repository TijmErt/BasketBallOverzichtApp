using BusnLogic;

namespace BasketBallASPNET.Models
{
    public class TeamVM
    {
        public int? ID;
        public string Name;
        public int LeeftijdsCategorieID;

        public TeamVM(string name, int leeftijdsCategorieID, int? id = null)
        {
            ID = id;
            Name = name;
            LeeftijdsCategorieID = leeftijdsCategorieID;
        }
        public TeamVM(Team temp)
        {
            this.ID = temp.ID;
            this.Name = temp.Name;
            this.LeeftijdsCategorieID = temp.LeeftijdsCategorieID;
        }
        
        public TeamVM()
        {

        }
    }
}

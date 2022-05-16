using BusnLogic;

namespace BasketBallASPNET.Models
{
    public class TeamCreateAndViewVM
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public int? LeeftijdsCategorieID { get; set; }
        public List<TeamVM>? Teams { get; set; }
        public TeamCreateAndViewVM(List<TeamVM> tempList, string? name = null, int? leeftijdsCategorieID = null, int? id = null)
        {
            Teams = tempList;
            ID = id;
            Name = name;
            LeeftijdsCategorieID = leeftijdsCategorieID;
        }

        public TeamCreateAndViewVM(int? iD, string? name, int? leeftijdsCategorieID)
        {
            ID = iD;
            Name = name;
            LeeftijdsCategorieID = leeftijdsCategorieID;
        }

        public TeamCreateAndViewVM()
        {

        }
    }
}

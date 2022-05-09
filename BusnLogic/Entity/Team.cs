using InterfaceLib;

namespace BusnLogic
{
    public class Team
    {
        public int? ID;
        public string Name;
        public int LeeftijdsCategorieID;

        public Team(string name, int leeftijdsCategorieID, int? id = null)
        {
            ID = id;
            Name = name;
            LeeftijdsCategorieID = leeftijdsCategorieID;
        }
        public Team(TeamDTO tempDTO)
        {
            ID = tempDTO.ID;
            Name = tempDTO.Name;
            LeeftijdsCategorieID = tempDTO.LeeftijdsCategorieID;
        }
        public TeamDTO GetDTO()
        {
            return new TeamDTO(Name, LeeftijdsCategorieID, ID);
        }
        public override string? ToString()
        {
            return $"Team met naam: {Name} en ID: {ID} \n ";
        }
    }
}
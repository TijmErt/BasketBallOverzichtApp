using InterfaceLib;

namespace BusnLogic
{
    public class Team
    {
        public int? ID;
        public int? ClubID;
        public string Name;
        public int? LeeftijdsCategorieID;
        public int? LeeftijdsCategorieNaam;

        public Team(string name, int? leeftijdsCategorieID, int? clubID, int? id = null)
        {
            ID = id;
            Name = name;
            LeeftijdsCategorieID = leeftijdsCategorieID;
            ClubID = clubID;
        }
        public Team(TeamDTO tempDTO)
        {
            ID = tempDTO.ID;
            Name = tempDTO.Name;
            LeeftijdsCategorieID = tempDTO.LeeftijdsCategorieID;
            ClubID = tempDTO.ClubID;
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
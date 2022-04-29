using InterfaceLib;

namespace BusnLogic
{
    public class Team
    {
        public int ID;
        public string Name;

        public Team(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public Team(TeamDTO tempDTO)
        {
            ID = tempDTO.ID;
            Name = tempDTO.Name;
        }

        public override string? ToString()
        {
            return $"Team met naam: {Name} en ID: {ID} \n ";
        }
    }
}
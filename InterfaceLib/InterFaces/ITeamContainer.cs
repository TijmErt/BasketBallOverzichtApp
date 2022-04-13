namespace InterfaceLib
{
    public interface ITeamContainer
    {
        public TeamDTO FindByID(long id);

        public List<TeamDTO> GetAllTeams();

        public List<TeamDTO> GetAllTeamsFromClub(long clubID);

    }
}
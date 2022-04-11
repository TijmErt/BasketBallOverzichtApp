namespace InterfaceLib
{
    public interface ITeamContainer
    {
        public TeamDTO FindByID(long id);

        public List<TeamDTO> GetAllTeams();

    }
}
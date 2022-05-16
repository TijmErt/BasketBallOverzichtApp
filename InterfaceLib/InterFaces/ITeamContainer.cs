namespace InterfaceLib
{
    public interface ITeamContainer
    {
        public TeamDTO FindByID(int id);

        public List<TeamDTO> GetAllTeams();

        public List<TeamDTO> GetAllTeamsFromClub(int clubID);

        public void CreateTeam(TeamDTO dto, int ClubID);

        public void DeleteTeam(int teamID);

        public bool CheckClubTeamLink(int TeamID, int ClubID);

    }
}
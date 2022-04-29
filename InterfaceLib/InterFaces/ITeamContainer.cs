﻿namespace InterfaceLib
{
    public interface ITeamContainer
    {
        public TeamDTO FindByID(int id);

        public List<TeamDTO> GetAllTeams();

        public List<TeamDTO> GetAllTeamsFromClub(int clubID);

    }
}
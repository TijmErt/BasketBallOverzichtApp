namespace InterfaceLib
{
    public interface ITeamContainer
    {
        /// <summary>
        /// Hier kun je de team gegevens krijgen met het teamID
        /// </summary>
        /// <param name="id">je geeft hier de team id mee</param>
        /// <returns>Geeft een TeamDTO terug</returns>
        public TeamDTO FindByID(int id);

        /// <summary>
        /// je haalt hier alle teams op die er bestaan
        /// </summary>
        /// <returns>geeft een lijst van teams terug</returns>
        public List<TeamDTO> GetAllTeams();

        /// <summary>
        /// Hier haal je alle teams van een club op
        /// </summary>
        /// <param name="clubID">je geeft hier de club ID mee</param>
        /// <returns>Geeft een lijst van Teams terug</returns>
        public List<TeamDTO> GetAllTeamsFromClub(int clubID);

        /// <summary>
        /// Hier creeër je een team voor je club
        /// </summary>
        /// <param name="dto">Je geeft hier een dto met de team gegevens mee</param>
        /// <param name="ClubID">Je geeft hier de club id mee waar de team aan wordt toegevoegd</param>

        public void CreateTeam(TeamDTO dto, int ClubID);

        /// <summary>
        /// Hier verwijder je een team 
        /// </summary>
        /// <param name="TeamID">je geeft hier de team id mee van het team dat verwijderd wordt</param>
        public void DeleteTeam(int TeamID);

        /// <summary>
        /// Checked of de team wel bij de club hoort
        /// </summary>
        /// <param name="TeamID">hier geef je de team id mee</param>
        /// <param name="ClubID">hier geef je de club id mee</param>
        /// <returns>Geeft een Boolean terug ((false als het niet bestaat), (true als het wel bestaat))</returns>
        public bool CheckClubTeamLink(int TeamID, int ClubID);

    }
}
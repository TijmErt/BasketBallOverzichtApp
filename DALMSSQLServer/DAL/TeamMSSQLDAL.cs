using InterfaceLib;
using System.Data;
using System.Data.SqlClient;

namespace DALMSSQLServer
{
    public class TeamMSSQLDAL : ITeamContainer
    {
        private static SqlConnection databaseConnection = new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");

        public TeamDTO FindByID(int id)
        {
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT ID, TeamName FROM Team WHERE ID = @TeamID";
            cmd = new SqlCommand(sql, databaseConnection);
            databaseConnection.Open();
            cmd.Parameters.AddWithValue("@TeamID", id );
            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                TeamDTO dto = new TeamDTO(reader.GetInt32("ID"), reader.GetString("TeamName"));
                databaseConnection.Close();
                return dto;

            }
            return null;


        }

        public List<TeamDTO> GetAllTeams()
        {
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT ID,TeamName FROM Team";
            cmd = new SqlCommand(sql, databaseConnection);
            databaseConnection.Open();

            reader = cmd.ExecuteReader();

            List<TeamDTO> list = new List<TeamDTO>();

            while (reader.Read())
            {
                list.Add(new TeamDTO(reader.GetInt32("ID"), reader.GetString("TeamName")));
            }
            databaseConnection.Close();

            return list;
        }

        public List<TeamDTO> GetAllTeamsFromClub(int clubID)
        {
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT T.ID, T.TeamName FROM Team T JOIN Club C ON t.Club_ID = C.ID AND T.Club_ID = @ClubID";
            cmd = new SqlCommand(sql, databaseConnection);
            databaseConnection.Open();
            cmd.Parameters.AddWithValue("@ClubID", clubID);
            reader = cmd.ExecuteReader();

            List<TeamDTO> list = new List<TeamDTO>();
            while (reader.Read())
            {
                list.Add(new TeamDTO(reader.GetInt32("ID"), reader.GetString("TeamName")));
            }
            databaseConnection.Close();
            return list;
        }
    }
}
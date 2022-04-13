using InterfaceLib;
using System.Data;
using System.Data.SqlClient;

namespace DALMSSQLServer
{
    public class TeamMSSQLDAL : ITeamContainer
    {
        private static SqlConnection databaseConnection = new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");


        private SqlDataReader QueryForDataBase(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query, databaseConnection);
            cmd.CommandTimeout = 60;

            if (databaseConnection.State == ConnectionState.Open)
            {
                databaseConnection.Close();
            }

            databaseConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            return (reader);

        }


        public TeamDTO FindByID(long id)
        {
            SqlDataReader reader = QueryForDataBase($"SELECT ID, TeamName FROM Team WHERE ID ='{id}'");
            reader.Read();
            if (reader.HasRows)
            {
                return new TeamDTO(reader.GetInt64("ID"), reader.GetString("TeamName"));
            }
            throw new Exception("bestaat niet");


        }

        public List<TeamDTO> GetAllTeams()
        {
            SqlDataReader reader = QueryForDataBase("SELECT ID,TeamName FROM Team");

            List<TeamDTO> list = new List<TeamDTO>();
            while (reader.Read())
            {
                list.Add(new TeamDTO(reader.GetInt64("ID"), reader.GetString("TeamName")));
            }

            return list;
        }

        public List<TeamDTO> GetAllTeamsFromClub(long clubID)
        {
            SqlDataReader reader = QueryForDataBase($"SELECT T.ID, T.TeamName FROM Team T JOIN Club C ON t.Club_ID = C.ID AND T.Club_ID = '{clubID}'");

            List<TeamDTO> list = new List<TeamDTO>();
            while (reader.Read())
            {
                list.Add(new TeamDTO(reader.GetInt64("ID"), reader.GetString("TeamName")));
            }

            return list;
        }
    }
}
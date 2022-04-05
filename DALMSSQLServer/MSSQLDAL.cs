using InterfaceLib;
using System.Data;
using System.Data.SqlClient;

namespace DALMSSQLServer
{
    public class MSSQLDAL : ITeamContainer
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
            return new TeamDTO(id, $"team({id})");
        }

        public List<TeamDTO> GetAll()
        {
            SqlDataReader reader = QueryForDataBase("SELECT ID,TeamName FROM Team");

            List<TeamDTO> list = new List<TeamDTO>();
            while(reader.Read())
            {
                list.Add(new TeamDTO(reader.GetInt64("ID"), reader.GetString("TeamName")));
            }

            return list;
        }
    }
}
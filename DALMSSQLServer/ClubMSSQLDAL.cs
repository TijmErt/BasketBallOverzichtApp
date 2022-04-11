using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALMSSQLServer
{
    public class ClubMSSQLDAL : IClubContainer
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


        public ClubDTO FindByID(long id)
        {
            SqlDataReader reader = QueryForDataBase("SELECT ID, ClubName FROM Club WHERE ID ='" + id + "'");
            reader.Read();
            if (reader.HasRows)
            {
                return new ClubDTO(reader.GetInt64("ID"), reader.GetString("ClubName"));
            }
            throw new Exception("bestaat niet");
        }

        public List<ClubDTO> GetAll()
        {
            SqlDataReader reader = QueryForDataBase("SELECT ID, ClubName FROM Club");

            List<ClubDTO> list = new List<ClubDTO>();
            while (reader.Read())
            {
                list.Add(new ClubDTO(reader.GetInt64("ID"), reader.GetString("ClubName")));
            }

            return list;
        }
    }
}

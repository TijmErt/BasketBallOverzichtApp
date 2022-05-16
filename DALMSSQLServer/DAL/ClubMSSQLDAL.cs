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

        public ClubDTO FindByID(int clubID)
        {
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT ID, ClubName FROM Club WHERE ID = @clubID";
            cmd = new SqlCommand(sql, databaseConnection);
            databaseConnection.Open();
            cmd.Parameters.AddWithValue("@clubID", clubID);
            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                ClubDTO dto = new ClubDTO(reader.GetInt32("ID"), reader.GetString("ClubName"));
                databaseConnection.Close();
                return dto;
            }
            databaseConnection.Close();
            throw null;
        }

        public List<ClubDTO> GetAll()
        {
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT ID, ClubName FROM Club";
            cmd = new SqlCommand(sql, databaseConnection);
            databaseConnection.Open();
            reader = cmd.ExecuteReader();
            List<ClubDTO> list = new List<ClubDTO>();
            while (reader.Read())
            {
                list.Add(new ClubDTO(reader.GetInt32("ID"), reader.GetString("ClubName")));
            }
            databaseConnection.Close();
            return list;
        }
    }
}

using InterfaceLib.DTO;
using InterfaceLib.InterFaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALMSSQLServer
{
    public class GebruikerMSSQLDAL : IGebruikerContainer
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


        public bool CheckGebruiker(string Email, string wachtwoord)
        {
            SqlCommand cmd;
            SqlDataReader reader;
            string sql = "SELECT Email FROM Gebruiker WHERE Email = @gebruikersnaam AND PassWord = @wachtwoord";
            cmd = new SqlCommand(sql, databaseConnection);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@wachtwoord", wachtwoord);
            if (databaseConnection.State == ConnectionState.Open)
            {
                databaseConnection.Close();
            }

            databaseConnection.Open();
            reader = cmd.ExecuteReader();
            bool returnValue = reader.HasRows;
            return returnValue;
        }

        public GebruikerDTO GetGebruiker(string Email)
        {
            SqlDataReader reader = QueryForDataBase("SELECT * FROM Gebruiker WHERE Email ='" + Email + "'");
            reader.Read();
            if (reader.HasRows)
            {
                return new GebruikerDTO(

                    reader.GetInt64("ID"), 
                    reader.GetString("FirstName"),
                    reader.GetString("lastName"), 
                    reader.GetDateTime("BirthDate"), 
                    reader.GetString("Geslacht"), 
                    reader.GetString("PassWord"), 
                    reader.GetString("Email")

                    );
            }
            throw new Exception("bestaat niet");
        }
        public List<GebruikerDTO> GetGebruikerFromTeam(long TeamID)
        {
            SqlDataReader reader = QueryForDataBase($"SELECT g.* FROM Gebruiker g JOIN Team t ON g.Team_ID = t.ID AND g.Team_ID = {TeamID}");

            List<GebruikerDTO> list = new List<GebruikerDTO>();
            while (reader.Read())
            {
                list.Add(new GebruikerDTO(reader.GetInt64("ID"), reader.GetString("FirstName"), reader.GetString("LastName"), reader.GetDateTime("BirthDate"), reader.GetString("Geslacht"), reader.GetString("Email")));
            }

            return list;
        }
        public void CreateGebruikerAccount()
        {
            QueryForDataBase("INSERT");
        }


    }
}

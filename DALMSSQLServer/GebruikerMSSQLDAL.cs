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
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT* FROM Gebruiker WHERE Email = @Email ";
            cmd = new SqlCommand(sql, databaseConnection);
            cmd.Parameters.AddWithValue("@Email", Email);

            if (databaseConnection.State == ConnectionState.Open)
            {
                databaseConnection.Close();
            }

            databaseConnection.Open();
            reader = cmd.ExecuteReader();

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
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT g.* FROM Gebruiker g JOIN Team t ON g.Team_ID = t.ID AND g.Team_ID = @TeamID";
            cmd = new SqlCommand(sql, databaseConnection);
            cmd.Parameters.AddWithValue("@TeamID", TeamID);

            if (databaseConnection.State == ConnectionState.Open)
            {
                databaseConnection.Close();
            }

            databaseConnection.Open();
            reader = cmd.ExecuteReader();

            List<GebruikerDTO> list = new List<GebruikerDTO>();
            while (reader.Read())
            {
                list.Add(

                    new GebruikerDTO(

                    reader.GetInt64("ID"),
                    reader.GetString("FirstName"),
                    reader.GetString("LastName"),
                    reader.GetDateTime("BirthDate"),
                    reader.GetString("Geslacht"),
                    reader.GetString("Email")
                    )
                    );
            }

            return list;


        }
        public void CreateGebruikerAccount(GebruikerDTO dto, string wachtwoord)
        {
            SqlCommand cmd;
            string sql = "INSERT INTO Gebruiker(FirstName, LastName, Email, PassWord, BirthDate, Geslacht) Values(" +
                "@Firstname," +
                "@LastName," +
                "@Email," +
                "@wachtwoord," +
                "@BirthDate," +
                "@Geslacht)";

            cmd = new SqlCommand(sql, databaseConnection);

            cmd.Parameters.AddWithValue("@Firstname", dto.FirstName);
            cmd.Parameters.AddWithValue("@LastName", dto.LastName);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@BirthDate", dto.GeboorteDatum);
            cmd.Parameters.AddWithValue("@Geslacht", dto.Geslacht);
            cmd.Parameters.AddWithValue("@wachtwoord", wachtwoord);

            if (databaseConnection.State == ConnectionState.Open)
            {
                databaseConnection.Close();
            }

            databaseConnection.Open();

            cmd.ExecuteNonQuery();
        }


    }
}

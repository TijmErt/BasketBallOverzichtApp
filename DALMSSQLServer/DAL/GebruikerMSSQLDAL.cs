using InterfaceLib.DTO;
using InterfaceLib.InterFaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace DALMSSQLServer
{
    public class GebruikerMSSQLDAL : IGebruikerContainer
    {
        private static SqlConnection databaseConnection = new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");

        public GebruikerDTO FindByEmailAndPassWordkGebruiker(string Email, string wachtwoord)
        {
            SqlCommand cmd;
            SqlDataReader reader;
            string sql = "SELECT * FROM Gebruiker WHERE Email = @Email ";
            cmd = new SqlCommand(sql, databaseConnection);
            cmd.Parameters.AddWithValue("@Email", Email);
            databaseConnection.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (BCrypt.Net.BCrypt.EnhancedVerify(wachtwoord, reader.GetString("PassWord")))
                {
                    GebruikerDTO temp = new GebruikerDTO(

                        reader.GetString("FirstName"),
                        reader.GetString("lastName"),
                        reader.GetDateTime("BirthDate"),
                        reader.GetString("Geslacht"),
                        reader.GetString("Email"),
                        reader.GetInt32("Role_ID"),
                        reader.GetInt32("Team_ID"),
                        reader.GetInt32("Club_ID"),
                        reader.GetInt32("ID"));
                    databaseConnection.Close();
                    return temp;
                }
            }
            databaseConnection.Close();
            return null;
        }

        public GebruikerDTO GetGebruiker(string Email)
        {
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT * FROM Gebruiker WHERE Email = @Email ";
            cmd = new SqlCommand(sql, databaseConnection);
            cmd.Parameters.AddWithValue("@Email", Email);
            databaseConnection.Open();

            reader = cmd.ExecuteReader();

            reader.Read();
            if (reader.HasRows)
            {
                GebruikerDTO temp = new GebruikerDTO(

                    reader.GetString("FirstName"),
                    reader.GetString("lastName"),
                    reader.GetDateTime("BirthDate"),
                    reader.GetString("Geslacht"),
                    reader.GetString("Email"),
                    reader.GetInt32("Role_ID"),
                    reader.GetInt32("Team_ID"),
                    reader.GetInt32("Club_ID"),
                    reader.GetInt32("ID"));
                databaseConnection.Close();
                return temp;
            }
            throw new Exception("bestaat niet");
        }
        public List<GebruikerDTO> GetGebruikerFromTeam(int TeamID)
        {
            SqlDataReader reader;
            SqlCommand cmd;

            string sql = "SELECT g.* FROM Gebruiker g JOIN Team t ON g.Team_ID = t.ID AND g.Team_ID = @TeamID";
            cmd = new SqlCommand(sql, databaseConnection);
            cmd.Parameters.AddWithValue("@TeamID", TeamID);

            databaseConnection.Open();
            reader = cmd.ExecuteReader();

            List<GebruikerDTO> list = new List<GebruikerDTO>();
            while (reader.Read())
            {
                list.Add(

                    new GebruikerDTO(

                    reader.GetString("FirstName"),
                    reader.GetString("LastName"),
                    reader.GetDateTime("BirthDate"),
                    reader.GetString("Geslacht"),
                    reader.GetString("Email"),
                    reader.GetInt32("Role_ID"),
                    reader.GetInt32("Team_ID"),
                    reader.GetInt32("Club_ID"),
                    reader.GetInt32("ID")
                    )
                    );
            }
            databaseConnection.Close();
            return list;


        }
        public void CreateGebruikerAccount(GebruikerDTO dto, string wachtwoord)
        {
            SqlCommand cmd;
            string sql = "INSERT INTO Gebruiker(FirstName, LastName, Email, PassWord, BirthDate, Geslacht, Club_ID, Role_ID) Values(" +
                "@Firstname," +
                "@LastName," +
                "@Email," +
                "@wachtwoord," +
                "@BirthDate," +
                "@Geslacht, " +
                "@ClubID, " +
                "@RoleID)";

            cmd = new SqlCommand(sql, databaseConnection);

            string hash = BCrypt.Net.BCrypt.EnhancedHashPassword(wachtwoord, 13);

            cmd.Parameters.AddWithValue("@Firstname", dto.FirstName);
            cmd.Parameters.AddWithValue("@LastName", dto.LastName);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@BirthDate", dto.GeboorteDatum);
            cmd.Parameters.AddWithValue("@Geslacht", dto.Geslacht);
            cmd.Parameters.AddWithValue("@ClubID", dto.ClubID);
            cmd.Parameters.AddWithValue("@RoleID", dto.RoleID);
            cmd.Parameters.AddWithValue("@wachtwoord", hash);

            databaseConnection.Open();

            cmd.ExecuteNonQuery();
            databaseConnection.Close();
        }


    }
}

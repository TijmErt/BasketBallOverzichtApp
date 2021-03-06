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
using DALException;

namespace DALMSSQLServer
{
    public class GebruikerMSSQLDAL : IGebruikerContainer
    {
        private static readonly SqlConnection databaseConnection = new("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");

        /// <summary>
        /// Hier word er met een account ingelogd door te kijken of er een email is die overeen komt met het wachtwoord
        /// </summary>
        /// <param name="Email">Hier word de email van de gebruiker ingevoerd</param>
        /// <param name="wachtwoord">Hier word het wachtwoord van de gebruiker ingevoerd</param>
        /// <returns>Het geeft en GebruikerDTO terug</returns>
        public GebruikerDTO FindGebruikerByEmailAndPassWord(string Email, string wachtwoord)
        {
            try
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
                        GebruikerDTO temp = new(

                            reader.GetString("FirstName"),
                            reader.GetString("lastName"),
                            reader.GetDateTime("BirthDate"),
                            reader.GetString("Geslacht"),
                            reader.GetString("Email"),
                            reader.GetInt32("Role_ID"),
                            reader.IsDBNull("Team_ID") ? null : reader.GetInt32("Team_ID"),
                            reader.GetInt32("Club_ID"),
                            reader.IsDBNull("PlayerNumber") ? null : reader.GetInt32("PlayerNumber"),
                            reader.GetInt32("ID"));
                        databaseConnection.Close();
                        return temp;
                    }
                }
                databaseConnection.Close();
                return null;
            }
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }
        }

        /// <summary>
        /// Hier word een gerbuiker terug gegeven als je een email mee geeft
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>er word een gerbuikerDTO terug gegegeven</returns>
        /// <exception cref="Exception"></exception>
        public GebruikerDTO GetGebruikerByEmail(string Email)
        {
            try
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
                    GebruikerDTO temp = new(

                        reader.GetString("FirstName"),
                        reader.GetString("lastName"),
                        reader.GetDateTime("BirthDate"),
                        reader.GetString("Geslacht"),
                        reader.GetString("Email"),
                        reader.GetInt32("Role_ID"),
                        reader.IsDBNull("Team_ID") ? null : reader.GetInt32("Team_ID"),
                        reader.GetInt32("Club_ID"),
                         reader.IsDBNull("PlayerNumber") ? null : reader.GetInt32("PlayerNumber"),
                        reader.GetInt32("ID"));
                    databaseConnection.Close();
                    return temp;
                }
                databaseConnection.Close();
                return null;
            }
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }
        }

        /// <summary>
        /// Hier mee haal je ale gebruikers op die in een team zitten
        /// </summary>
        /// <param name="TeamID">Geef hier het ID van de Team mee</param>
        /// <returns>Het geeft een lijst van Gebruikers die in de gegeven team zitten</returns>
        public List<GebruikerDTO> GetGebruikersFromTeam(int TeamID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT g.* FROM Gebruiker g WHERE Team_ID = @TeamID";
                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@TeamID", TeamID);

                databaseConnection.Open();
                reader = cmd.ExecuteReader();

                List<GebruikerDTO> list = new();
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
                        reader.IsDBNull("Team_ID") ? null : reader.GetInt32("Team_ID"),
                        reader.GetInt32("Club_ID"),
                        reader.IsDBNull("PlayerNumber") ? null : reader.GetInt32("PlayerNumber"),
                        reader.GetInt32("ID")
                        )
                        );
                }
                databaseConnection.Close();
                return list;
            }
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }

        }

        /// <summary>
        /// Hier mee wordt er een account voor een gebruiker gemaakt in de database
        /// </summary>
        /// <param name="dto">geef hier een variable mee van het type gebruikerDTO</param>
        /// <param name="wachtwoord">Geef hier de een string mee voor het wachtwoord van de gebruiker</param>
        public void CreateGebruikerAccount(GebruikerDTO dto, string wachtwoord)
        {
            try
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
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }
        }

        /// <summary>
        /// Hier mee zet je een gebruiker in een Team
        /// </summary>
        /// <param name="GebruikerID">Geef hier de Gebruiker ID mee</param>
        /// <param name="TeamID">Geef hier de Team ID mee</param>

        public void InsertGebruikerInToTeam(int GebruikerID, int TeamID, int SpelerNummer)
        {
            try
            {
                SqlCommand cmd;
                string sql = "UPDATE Gebruiker SET Team_ID = @TeamID, PlayerNumber = @SpelerNummer WHERE ID = @SpelerID";
                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@SpelerID", GebruikerID);
                cmd.Parameters.AddWithValue("@TeamID", TeamID);
                cmd.Parameters.AddWithValue("@SpelerNummer", SpelerNummer);

                databaseConnection.Open();
                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }
        }

        /// <summary>
        /// Hier mee haal je een Gebruiker uit een team
        /// </summary>
        /// <param name="GebruikerID">Geef hier de ID mee van de speler die er uit moet</param>
        public void RemoveGebruikerFromTeam(int GebruikerID)
        {
            try
            {
                SqlCommand cmd;
                string sql = "UPDATE Gebruiker SET Team_ID = null WHERE ID = @SpelerID";
                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@SpelerID", GebruikerID);

                databaseConnection.Open();
                cmd.ExecuteNonQuery();
                databaseConnection.Close();
            }
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }
        }


        /// <summary>
        /// Hier mee haal je alle leden van een specifieke club op.
        /// </summary>
        /// <param name="ClubID">hier moet de ID van de club in komen</param>
        /// <returns>Het geeft een lijst van gebruikers terug die in de gegeven club zitten</returns>
        public List<GebruikerDTO> GetAllGebruikersFromClub(int ClubID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT g.* FROM Gebruiker g WHERE g.Club_ID = @ClubID";
                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@ClubID", ClubID);

                databaseConnection.Open();
                reader = cmd.ExecuteReader();

                List<GebruikerDTO> list = new();
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
                        reader.IsDBNull("Team_ID") ? null : reader.GetInt32("Team_ID"),
                        reader.GetInt32("Club_ID"),
                        reader.IsDBNull("PlayerNumber") ? null : reader.GetInt32("PlayerNumber"),
                        reader.GetInt32("ID")
                        ));
                }
                databaseConnection.Close();
                return list;
            }
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }
        }

        public List<int> GetGebruikerIDFromWedstrijdTeams(int ThuisTeam, int UitTeam)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT ID FROM Gebruiker WHERE Team_ID = @ThuisTeamID OR Team_ID = @UitTeamID ";
                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@ThuisTeamID", ThuisTeam);
                cmd.Parameters.AddWithValue("@UitTeamID", UitTeam);

                databaseConnection.Open();
                reader = cmd.ExecuteReader();

                List<int> list = new();
                while (reader.Read())
                {
                    list.Add(reader.GetInt32("ID"));
                }
                databaseConnection.Close();
                return list;
            }
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }
        }

        public List<GebruikerDTO> GetAllGebruikersFromWedstrijdID(int wedstrijdID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT * FROM WedstrijdGebruiker, Gebruiker WHERE Wedstrijd_ID = @wedstrijdID AND  Gebruiker.ID = Gebruiker_ID";
                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@wedstrijdID", wedstrijdID);
                databaseConnection.Open();
                reader = cmd.ExecuteReader();

                List<GebruikerDTO> list = new();
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
                        reader.IsDBNull("Team_ID") ? null : reader.GetInt32("Team_ID"),
                        reader.GetInt32("Club_ID"),
                        reader.IsDBNull("PlayerNumber") ? null : reader.GetInt32("PlayerNumber"),
                        reader.GetInt32("ID")
                        ));
                }
                databaseConnection.Close();
                return list;
            }
            catch (InvalidOperationException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                databaseConnection.Close();
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                databaseConnection.Close();
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }
        }
    }
}

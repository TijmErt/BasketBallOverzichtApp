using DALException;
using InterfaceLib;
using System.Data;
using System.Data.SqlClient;

namespace DALMSSQLServer
{
    public class TeamMSSQLDAL : ITeamContainer
    {
        private static readonly SqlConnection databaseConnection = new ("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");


        /// <summary>
        /// Checked of de team wel bij de club hoort
        /// </summary>
        /// <param name="TeamID">hier geef je de team id mee</param>
        /// <param name="ClubID">hier geef je de club id mee</param>
        /// <returns>Geeft een Boolean terug ((false als het niet bestaat), (true als het wel bestaat))</returns>
        public bool CheckClubTeamLink(int TeamID, int ClubID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;
                string sql = "SELECT * FROM Team WHERE ID = @TeamID AND Club_ID = @ClubID";

                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@TeamID", TeamID);
                cmd.Parameters.AddWithValue("@ClubID", ClubID);

                databaseConnection.Open();
                reader = cmd.ExecuteReader();
                bool check = reader.HasRows;
                databaseConnection.Close();
                return check;
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
        /// Hier creeër je een team voor je club
        /// </summary>
        /// <param name="dto">Je geeft hier een dto met de team gegevens mee</param>
        /// <param name="ClubID">Je geeft hier de club id mee waar de team aan wordt toegevoegd</param>
        public void CreateTeam(TeamDTO dto, int ClubID)
        {
            try
            {
                SqlCommand cmd;
                string sql = "INSERT INTO Team(TeamName, LeeftijdsCategorieën_ID, Club_ID) Values(" +
                    "@Name," +
                    "@LeeftijdsCategorieID," +
                    "@clubID)";

                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@Name", dto.Name);
                cmd.Parameters.AddWithValue("@LeeftijdsCategorieID", dto.LeeftijdsCategorieID);
                cmd.Parameters.AddWithValue("@clubID", ClubID);

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
        /// Hier verwijder je een team 
        /// </summary>
        /// <param name="TeamID">je geeft hier de team id mee van het team dat verwijderd wordt</param>
        public void DeleteTeam(int TeamID)
        {
            try
            {
                SqlCommand cmd;
                string sql = "DELETE FROM Team WHERE ID = @teamID";

                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@teamID", TeamID);

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
        /// Hier kun je de team gegevens krijgen met het teamID
        /// </summary>
        /// <param name="id">je geeft hier de team id mee</param>
        /// <returns>Geeft een TeamDTO terug</returns>
        public TeamDTO GetTeamDataByID(int id)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT * FROM Team T, LeeftijdsCategorieën LG WHERE T.ID = @TeamID AND LeeftijdsCategorieën_ID = LG.ID";
                cmd = new SqlCommand(sql, databaseConnection);
                databaseConnection.Open();
                cmd.Parameters.AddWithValue("@TeamID", id);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    TeamDTO dto = new(reader.GetString("TeamName"), reader.GetInt32("LeeftijdsCategorieën_ID"), reader.GetInt32("Club_ID"), reader.GetInt32("ID"));
                    dto.LeeftijdsCategorieNaam = reader.GetInt32("LeeftijdsCategorieënNaam");
                    databaseConnection.Close();
                    return dto;

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
        /// je haalt hier alle teams op die er bestaan
        /// </summary>
        /// <returns>geeft een lijst van teams terug</returns>
        public List<TeamDTO> GetAllTeams()
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT * FROM Team";
                cmd = new SqlCommand(sql, databaseConnection);
                databaseConnection.Open();

                reader = cmd.ExecuteReader();

                List<TeamDTO> list = new();

                while (reader.Read())
                {
                    list.Add(new(reader.GetString("TeamName"), reader.GetInt32("LeeftijdsCategorieën_ID"), reader.GetInt32("Club_ID"), reader.GetInt32("ID")));
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
        /// Hier haal je alle teams van een club op
        /// </summary>
        /// <param name="ClubID">je geeft hier de club ID mee</param>
        /// <returns>Geeft een lijst van Teams terug</returns>
        public List<TeamDTO> GetAllTeamsFromClub(int ClubID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT * FROM Team T WHERE Club_ID = @ClubID";
                cmd = new SqlCommand(sql, databaseConnection);
                databaseConnection.Open();
                cmd.Parameters.AddWithValue("@ClubID", ClubID);
                reader = cmd.ExecuteReader();

                List<TeamDTO> list = new();
                while (reader.Read())
                {
                    list.Add(new(reader.GetString("TeamName"), reader.GetInt32("LeeftijdsCategorieën_ID"), reader.GetInt32("Club_ID"), reader.GetInt32("ID")));
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
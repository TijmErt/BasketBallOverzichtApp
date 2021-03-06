using InterfaceLib.DTO;
using InterfaceLib.InterFaces;
using System.Data.SqlClient;
using System.Data;
using DALException;

namespace DALMSSQLServer.DAL
{
    public class WedstrijdMSSQLDAL : IWedstrijdContainer
    {
        private static readonly SqlConnection databaseConnection = new("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");

        public void AddSpelerToeWedstrijd(int SpelerID, int WedstrijdID)
        {
            try
            {
                SqlCommand cmd;
                string sql = @"INSERT INTO WedstrijdGebruiker(Wedstrijd_ID, Gebruiker_ID, Presentie) 
                           VALUES(@WedstrijdID,@SpelerID,0)";

                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@SpelerID", SpelerID);
                cmd.Parameters.AddWithValue("@WedstrijdID", WedstrijdID);

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

        public int CreateWedstrijd(WedstrijdDTO dto)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;
                string sql = @"INSERT INTO Wedstrijd(ThuisClubID, UitClubID, ThuisTeamID, UitTeamID, DatumTijd) 
                           OUTPUT inserted.ID AS WedstrijdID
                           VALUES(@ThuisClubID, @UitClubID, @ThuisTeamID,@UitTeamID , @SpeelDatum)";

                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@ThuisClubID", dto.thuisClubID);
                cmd.Parameters.AddWithValue("@UitClubID", dto.uitClubID);
                cmd.Parameters.AddWithValue("@ThuisTeamID", dto.thuisTeamID);
                cmd.Parameters.AddWithValue("@UitTeamID", dto.uitTeamID);
                cmd.Parameters.AddWithValue("@SpeelDatum", dto.speelDatum);

                databaseConnection.Open();

                reader = cmd.ExecuteReader();
                int WedstrijdID = 0;
                List<int> IDs = new List<int>();
                while (reader.Read())
                {
                    WedstrijdID = reader.GetInt32("WedstrijdID");

                }
                databaseConnection.Close();
                return WedstrijdID;
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

        public List<WedstrijdDTO> GetAllFromTeam(int TeamID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT * FROM Wedstrijd WHERE UitTeamID = @TeamID OR ThuisTeamID = @TeamID";
                cmd = new SqlCommand(sql, databaseConnection);
                databaseConnection.Close();
                databaseConnection.Open();
                cmd.Parameters.AddWithValue("@TeamID", TeamID);
                reader = cmd.ExecuteReader();

                List<WedstrijdDTO> list = new();
                while (reader.Read())
                {
                    list.Add(new WedstrijdDTO(
                        reader.GetInt32("ThuisClubID"),
                        reader.GetInt32("UitClubID"),
                        reader.GetInt32("ThuisTeamID"),
                        reader.GetInt32("UitTeamID"),
                        reader.GetDateTime("DatumTijd"),
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

        public bool GetPresentie(int SpelerID, int WedstrijdID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = @"SELECT Presentie FROM WedstrijdGebruiker 
                           WHERE Gebruiker_ID = @SpelerID AND Wedstrijd_ID = @WedstrijdID";
                cmd = new SqlCommand(sql, databaseConnection);
                databaseConnection.Open();
                cmd.Parameters.AddWithValue("@SpelerID", SpelerID);
                cmd.Parameters.AddWithValue("@WedstrijdID", WedstrijdID);
                reader = cmd.ExecuteReader();
                reader.Read();

                bool presentie = reader.GetBoolean("Presentie");
                databaseConnection.Close();
                return presentie;
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

        public WedstrijdDTO GetWedstrijdByID(int WedstrijdID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = @"SELECT * FROM Wedstrijd
                           WHERE ID = @WedstrijdID";
                cmd = new SqlCommand(sql, databaseConnection);
                databaseConnection.Open();
                cmd.Parameters.AddWithValue("@WedstrijdID", WedstrijdID);
                reader = cmd.ExecuteReader();
                reader.Read();

                WedstrijdDTO wedstrijd = new WedstrijdDTO(
                                        reader.GetInt32("ThuisClubID"),
                                        reader.GetInt32("UitClubID"),
                                        reader.GetInt32("ThuisTeamID"),
                                        reader.GetInt32("UitTeamID"),
                                        reader.GetDateTime("DatumTijd"));
                databaseConnection.Close();
                return wedstrijd;
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

        public void UpdatePresentie(int SpelerID, int WedstrijdID,bool Presentie)
        {
            try
            {
                SqlCommand cmd;
                string sql = @"UPDATE WedstrijdGebruiker 
                           SET Presentie = @Presentie 
                           WHERE Wedstrijd_ID = @WedstrijdID AND Gebruiker_ID = @SpelerID";

                cmd = new SqlCommand(sql, databaseConnection);
                cmd.Parameters.AddWithValue("@SpelerID", SpelerID);
                cmd.Parameters.AddWithValue("@WedstrijdID", WedstrijdID);
                cmd.Parameters.AddWithValue("@Presentie", Presentie);

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


    }
}

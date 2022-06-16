using InterfaceLib.DTO;
using InterfaceLib.InterFaces;
using System.Data.SqlClient;
using System.Data;

namespace DALMSSQLServer.DAL
{
    public class WedstrijdMSSQLDAL : IWedstrijdContainer
    {
        private static readonly SqlConnection databaseConnection = new("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");

        public void AddSpelerToeWedstrijd(int SpelerID, int WedstrijdID)
        {
            SqlCommand cmd;
            string sql = @"INSERT INTO WedstrijdGebruiker(Wedstrijd_ID, Gebruiker_ID, Presentie) 
                           VALUES(@WedstrijdID,@SpelerID,1)";

            cmd = new SqlCommand(sql, databaseConnection);
            cmd.Parameters.AddWithValue("@SpelerID", SpelerID);
            cmd.Parameters.AddWithValue("@WedstrijdID", WedstrijdID);

            databaseConnection.Open();

            cmd.ExecuteNonQuery();
            databaseConnection.Close();
        }

        public void CreateWedstrijd(WedstrijdDTO dto)
        {
            SqlCommand cmd;
            string sql = @"INSERT INTO Wedstrijd(ThuisClubID, UitClubID, ThuisTeamID, UitTeamID, DatumTijd) 
                           VALUES(@ThuisClubID, @UitClubID, @ThuisTeamID,@UitTeamID , @SpeelDatum)";

            cmd = new SqlCommand(sql, databaseConnection);
            cmd.Parameters.AddWithValue("@ThuisClubID", dto.thuisClubID);
            cmd.Parameters.AddWithValue("@UitClubID", dto.uitClubID);
            cmd.Parameters.AddWithValue("@ThuisTeamID", dto.thuisTeamID);
            cmd.Parameters.AddWithValue("@UitTeamID", dto.uitTeamID);
            cmd.Parameters.AddWithValue("@SpeelDatum", dto.speelDatum);

            databaseConnection.Open();

            cmd.ExecuteNonQuery();
            databaseConnection.Close();
        }

        public List<WedstrijdDTO> GetAllFromTeam(int TeamID)
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

        public bool GetPersentie(int SpelerID, int WedstrijdID)
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

        public WedstrijdDTO GetWedstrijdByID(int WedstrijdID)
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

        public void UpdatePresentie(int SpelerID, int WedstrijdID,bool Presentie)
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


    }
}

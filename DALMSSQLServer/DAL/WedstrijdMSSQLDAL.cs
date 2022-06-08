using InterfaceLib.DTO;
using InterfaceLib.InterFaces;
using System.Data.SqlClient;
using System.Data;

namespace DALMSSQLServer.DAL
{
    public class WedstrijdMSSQLDAL : IWedstrijdContainer
    {
        private static readonly SqlConnection databaseConnection = new("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");

        public void CreateWedstrijd(WedstrijdDTO dto)
        {
            SqlCommand cmd;
            string sql = "INSERT INTO Team(TeamName, LeeftijdsCategorieën_ID, Club_ID) Values(" +
                "@Name," +
                "@LeeftijdsCategorieID," +
                "@clubID)";

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
                    reader.GetDateTime("SpeelDatum")
                    ));
            }
            databaseConnection.Close();
            return list;
        }
    }
}

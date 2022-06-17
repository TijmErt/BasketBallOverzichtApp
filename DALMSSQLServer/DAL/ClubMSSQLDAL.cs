using DALException;
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
        private static readonly SqlConnection databaseConnection = new("Server=mssqlstud.fhict.local;Database=dbi486333_basketbal;User Id=dbi486333_basketbal;Password=Basketbal");


        /// <summary>
        /// Je haalt hier mee gegevens op van de meegegven club ID
        /// </summary>
        /// <param name="ClubID">Je voert hier je club ID in</param>
        /// <returns>Het geeft een ClubDTO terug</returns>
        /// <exception cref="NullReferenceException"></exception>
        public ClubDTO GetClubDataFromID(int ClubID)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT ID, ClubName FROM Club WHERE ID = @clubID";
                cmd = new SqlCommand(sql, databaseConnection);
                databaseConnection.Open();
                cmd.Parameters.AddWithValue("@clubID", ClubID);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    ClubDTO dto = new(reader.GetInt32("ID"), reader.GetString("ClubName"));
                    databaseConnection.Close();
                    return dto;
                }
                databaseConnection.Close();
                throw null;
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
            }

        }
        /// <summary>
        /// Hier haal je alle clubs op die er bestaan
        /// </summary>
        /// <returns>Het geeft een lijst van ClubDTO's terug</returns>
        public List<ClubDTO> GetAll()
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd;

                string sql = "SELECT ID, ClubName FROM Club";
                cmd = new SqlCommand(sql, databaseConnection);
                databaseConnection.Open();
                reader = cmd.ExecuteReader();
                List<ClubDTO> list = new();
                while (reader.Read())
                {
                    list.Add(new ClubDTO(reader.GetInt32("ID"), reader.GetString("ClubName")));
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

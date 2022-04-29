using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DatabaseConnectie
    {
        public static SqlConnection SqlConnectie { get; private set; }

        public static void SetConnection(string connectieString)
        {
            SqlConnectie = new SqlConnection(connectieString);
        }

        public static void Open()
        {
            if (SqlConnectie.State != ConnectionState.Open)
            {
                SqlConnectie.Open();
            }
        }

        public static void Close()
        {
            if (SqlConnectie.State != ConnectionState.Closed)
            {
                SqlConnectie.Close();
            }
        }

        public SqlDataReader LoadQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, SqlConnectie);
            cmd.CommandTimeout = 60;
            SqlDataReader reader;
            SqlConnectie.Open();
            reader = cmd.ExecuteReader();

            return (reader);
        }
    }
}

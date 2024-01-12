using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnectionLayer
{
    public class Connection
    {
        public static void Main()
        {

        }

        private SqlConnection conn;

        public Connection()
        {
            //Connection string to connect to the DB
            string connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = ".\\SQLEXPRESS",
                InitialCatalog = "oopHome",
                IntegratedSecurity = true,
                UserID = "LAPTOP-LUCAS\\lucas",
            }.ConnectionString;

            //This is to populate the Sqlconnection
            conn = new SqlConnection(connectionString);
        }

        public SqlConnection ConnectionToDB()
        {
            try
            {
                // Openening the connection
                conn.Open();

                // Checking if the program can connect
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(" ");
                } else
                {
                    Console.WriteLine("Error Connecting to DB");
                }

                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection unsuccessful. Error: {ex.Message}");
                return null;
            }
        }

        public void CloseDB()
        {
            // Closing the connection
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}

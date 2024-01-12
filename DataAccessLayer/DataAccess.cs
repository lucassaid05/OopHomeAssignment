using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using ConnectionLayer;
using System.Runtime.Remoting.Contexts;

namespace DataAccessLayer
{
    public class DataAccess
    {
        public static void Main()
        {

        }

        // Creating an instance of the Connection class
        Connection connection = new Connection();
        public SqlConnection openDB()
        {
            // Calling ConnectionToDB() method
            return connection.ConnectionToDB();
        }

        public void closeDB()
        {
            // Calling CloseDB() method
            connection.CloseDB();
        }

        public Exception addPlayer(Players players, SqlConnection sqlConnection)
        {
            try
            {
                string addPlayerQuery = "INSERT INTO [dbo].[Players] ([Username], [Password]) " +
                        "VALUES ('"+ players.Username + "', '"+ players.Password + "')";

                SqlCommand addPlayersCMD = new SqlCommand(addPlayerQuery, sqlConnection);


                // Execute the command
                addPlayersCMD.ExecuteNonQuery();

                //To see if the players is added
                Console.WriteLine("Player Added");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding Player");
                return ex;
            }
        }


        public int selectPlayer(Players players, SqlConnection sqlConnection)
        {
            try
            {
                string checkPlayerQuery = "SELECT COUNT(*) FROM [dbo].[Players] WHERE Username LIKE '%" + players.Username + "%'";
                SqlCommand checkPlayerCmd = new SqlCommand(checkPlayerQuery, sqlConnection);
                int playerCount = (int)checkPlayerCmd.ExecuteScalar();
                return playerCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking for Players");
                return 0;
            }
        }

        public Exception addGame(Games games, SqlConnection sqlConnection)
        {
            try
            {
                //SQL string that represnts adding the Player
                string addPlayerQuery = "INSERT INTO [dbo].[Game] ([Title], [CreatorFK], [OpponentFK], [Complete]) " +
                                        "VALUES ('" + games.Title + "', " + games.CreatorFK + ", " + games.OpponentFK + ", 0)";


                //Adding the player object into the database
                SqlCommand addPlayersCMD = new SqlCommand(addPlayerQuery, sqlConnection);

                // Execute the commands
                addPlayersCMD.ExecuteNonQuery();

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding Game");
                return ex;
            }
        }

        public Exception addShipConfiguration(GameShipConfiguration shipConfig, SqlConnection sqlConnection)
        {
            try
            {
                string addShipConfigQuery = "INSERT INTO [dbo].[GameShipConfiguration] ([PlayerFK], [GameFK], [Coordinate]) " +
                                            "VALUES ('"+shipConfig.PlayerFK+"', "+shipConfig.GameFK+", '"+shipConfig.Coordinate+"')";


                SqlCommand command = new SqlCommand(addShipConfigQuery, sqlConnection);

                command.ExecuteNonQuery();

                Console.WriteLine("Ship Configuration Added");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding Configuration");
                return ex;
            }
        }

        public bool shipOverlap(GameShipConfiguration shipConfig, SqlConnection sqlConnection)
        {
            try
            {
                string checkOverlapQuery = "SELECT COUNT(*) FROM [dbo].[GameShipConfiguration] " +
                                           "WHERE [Coordinate] = '"+shipConfig.Coordinate+"' AND [PlayerFK] = '"+shipConfig.PlayerFK+"'";

                SqlCommand command = new SqlCommand(checkOverlapQuery, sqlConnection);

                int overlapCount = (int)command.ExecuteScalar();

                if (overlapCount>0)
                {
                    return true;
                }else
                {
                    return false;
                }
    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking for overlap");
                return false;
            }
        }

        public int getPlayerId(string username, SqlConnection sqlConnection)
        {
            try
            {
                string playerId_query = "SELECT ID FROM [dbo].[Players] WHERE Username = '" + username + "'";

                SqlCommand idGetter = new SqlCommand(playerId_query, sqlConnection);
                int playerId = (int)idGetter.ExecuteScalar();

                return playerId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting Player Id");
                return 0;
            }
        }


        public int getGameId(string title, SqlConnection sqlConnection)
        {
            try
            {
                string gameIdQuery = "SELECT ID FROM [dbo].[Game] WHERE Title = '" + title + "'";

                SqlCommand gameIDgetter = new SqlCommand(gameIdQuery, sqlConnection);

                int gameId = (int)gameIDgetter.ExecuteScalar();

                return gameId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting Game Id");
                return 0;
            }
        }

        public List<string> getShipCoordinate(int gameId,string username, SqlConnection sqlConnection)
        {
            try
            {
                List<string> coordinates = new List<string>();

                string query = "SELECT Coordinate FROM [dbo].[GameShipConfiguration] WHERE GameFK = " + gameId+ " AND PlayerFK = '"+username+"'";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string coordinate = reader["Coordinate"].ToString();
                    coordinates.Add(coordinate);
                }

                reader.Close();

                return coordinates;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with the Ship Coordinate");
                return null;
            }
        }

        public Exception gameOver(int gameId, SqlConnection sqlConnection)
        {
            try
            {
                string gameIdQuery = "UPDATE [dbo].[Game] SET Complete WHERE ID ="+gameId;
                SqlCommand updateCmd = new SqlCommand(gameIdQuery, sqlConnection);
                updateCmd.ExecuteNonQuery();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with the game");
                return ex;
            }
        }

    }
}

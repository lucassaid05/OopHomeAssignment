using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DataLayer
{
    public class Data
    {
        public static void Main()
        {

        }

        DataAccess dal = new DataAccess();

        public Exception addPlayer(Players players, SqlConnection sqlConnection)
        {
            return dal.addPlayer(players, sqlConnection);
        }

        public int selectPlayer(Players players, SqlConnection sqlConnection)
        {
            return dal.selectPlayer(players, sqlConnection);
        }

        public Exception addGame(Games games, SqlConnection sqlConnection)
        {
            return dal.addGame(games, sqlConnection);
        }

        public Exception addShipConfiguration(GameShipConfiguration shipConfig, SqlConnection sqlConnection)
        {
            return dal.addShipConfiguration(shipConfig, sqlConnection);
        }

        public bool shipOverlap(GameShipConfiguration shipConfig, SqlConnection sqlConnection)
        {
            return dal.shipOverlap(shipConfig, sqlConnection);
        }

        public int getPlayerId(string username, SqlConnection sqlConnection)
        {
            return dal.getPlayerId(username, sqlConnection);
        }

        public int getGameId(string title, SqlConnection sqlConnection)
        {
            return dal.getGameId(title, sqlConnection);
        }

        public List<string> getShipCoordinate(int gameId,string username, SqlConnection sqlConnection)
        {
            return dal.getShipCoordinate(gameId,username, sqlConnection);
        }

        public Exception gameOver(int gameId, SqlConnection sqlConnection)
        {
            return dal.gameOver(gameId, sqlConnection);
        }
    }
}

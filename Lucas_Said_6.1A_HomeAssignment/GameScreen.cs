using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas_Said_6._1A_HomeAssignment
{
    public class GameScreen
    {
        private List<Cell> cells;

        List<string> shipCoordinatesPlayerOne = new List<string>();
        List<string> shipCoordinatesPlayerTwo = new List<string>();

        //Instatiate objects
        Data data = new Data();
        static DataAccess dal = new DataAccess();
        SqlConnection myconnection = dal.openDB();


        int game_id = 0;

        public GameScreen(List<Cell> cells)
        {
            this.cells = cells;
        }

        public void LoadGrid(Players playerDetail)
        {
            game_id = data.getGameId(Presentation.gameTitle, myconnection);

            shipCoordinatesPlayerOne = data.getShipCoordinate(game_id, playerDetail.PlayerOne, myconnection);
            shipCoordinatesPlayerTwo = data.getShipCoordinate(game_id, playerDetail.PlayerTwo, myconnection);
        }

        public void PrintGrid(Players playerDetail, string player, string attacks)
        {
            StringBuilder grid = new StringBuilder();
            int rowSize = 5;

            grid.Append("   1   2   3   4   5\n");
            grid.Append(" +---+---+---+---+---+\n");

            for (int y = 0; y < rowSize; y++)
            {
                char note = (char)('A' + y);
                grid.Append(note + "| ");

                for (int x = 1; x <= rowSize; x++)
                {
                    string cellCoordinate = note.ToString() + x;

                    // Check if the current player is Player 1
                    if (player == playerDetail.PlayerOne)
                    {
                        // For Player 1, check if the current cell coordinate is in the list of ship coordinates for Player 1
                        if (shipCoordinatesPlayerOne.Contains(cellCoordinate))
                        {
                            // Cell has a ship
                            grid.Append("S ");
                        }
                        else
                        {
                            // Cell does not have a ship
                            // Check if the current cell coordinate is in the list of attack coordinates for Player 1
                            bool isAttackCoordinate = shipCoordinatesPlayerTwo.Contains(cellCoordinate);
                            // Iterate through the cells list to find and print the appropriate cell type
                            foreach (var cell in cells)
                            {
                                if (cell.Coordinate == cellCoordinate)
                                {
                                    // If it's an AttackCell, use the specific PrintCell method
                                    if (cell is AttackCell)
                                    {
                                        ((AttackCell)cell).PrintCell(isAttackCoordinate);
                                    }
                                    else
                                    {
                                        cell.PrintCell();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        // For Player 2, check if the current cell coordinate is in the list of ship coordinates for Player 2
                        if (shipCoordinatesPlayerTwo.Contains(cellCoordinate))
                        {
                            // Cell has a ship
                            grid.Append("S ");
                        }
                        else
                        {
                            // Cell does not have a ship
                            // Check if the current cell coordinate is in the list of attack coordinates for Player 2
                            bool isAttackCoordinate = shipCoordinatesPlayerOne.Contains(cellCoordinate);
                            // Iterate through the cells list to find and print the appropriate cell type
                            foreach (var cell in cells)
                            {
                                if (cell.Coordinate == cellCoordinate)
                                {
                                    // If it's an AttackCell, use the specific PrintCell method
                                    if (cell is AttackCell)
                                    {
                                        ((AttackCell)cell).PrintCell(isAttackCoordinate);
                                    }
                                    else
                                    {
                                        cell.PrintCell();
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    if (x != rowSize)
                    {
                        grid.Append("| ");
                    }
                }
                grid.AppendLine("\n +---+---+---+---+---+");
            }
            Console.WriteLine(grid.ToString());
        }



        public void AttackMode(Players playerDetail, string player, string attacks)
        {
            if (player == playerDetail.PlayerOne)
            {

                if (shipCoordinatesPlayerTwo.Count == 0)
                {
                    Console.WriteLine("Player 1 Wins!");
                    data.gameOver(game_id, myconnection);
                    Environment.Exit(0);
                }
                if (shipCoordinatesPlayerTwo.Contains(attacks))
                {
                    Console.WriteLine("Hit");
                    shipCoordinatesPlayerTwo.Remove(attacks);
                }
                else
                {
                    Console.WriteLine("You missed");
                }
            }
            else
            {
                if (shipCoordinatesPlayerOne.Count == 0)
                {
                    Console.WriteLine("Player 2 Wins!");
                    data.gameOver(game_id, myconnection);
                    Environment.Exit(0);
                }

                if (shipCoordinatesPlayerOne.Contains(attacks))
                {
                    Console.WriteLine("Hit");
                    shipCoordinatesPlayerOne.Remove(attacks);
                }
                else
                {
                    Console.WriteLine("You missed");
                }
            }

            Console.WriteLine("Player 1 ships remaining: "+shipCoordinatesPlayerOne.Count());
            Console.WriteLine("Player 2 ships remaining:"+shipCoordinatesPlayerTwo.Count());
        }

        public static List<Cell> LoadCells()
        {
            List<Cell> cells = new List<Cell>();

            // Example: Add ShipCells to the list
            for (int i = 0; i < 8; i++)
            {
                cells.Add(new ShipCell());
            }

            // Example: Add AttackCells to the list
            for (int i = 0; i < 8; i++)
            {
                cells.Add(new AttackCell("X"));
            }

            return cells;
        }
    }

    public abstract class Cell
    {
        // Common properties for both a cell showing a ship or a successful/missed attack
        // This has to be implemented differently in the next two classes shown here
        // for a cell representing a ship or a cell representing a Ship or a Cell representing a successful attack or a missed one
        // If it's a ship, print S
        // If it's missed, print M
        // If it's successful, print X
        public abstract void PrintCell();

        // Add a property to store the coordinate of the cell
        public string Coordinate { get; set; }
    }

    public class ShipCell : Cell
    {
        public override void PrintCell()
        {
            Console.Write("S");
        }
    }

    public class AttackCell : Cell
    {
        private string status;

        public AttackCell(string status)
        {
            this.status = status;
        }

        public override void PrintCell()
        {
            Console.Write(status + " ");
        }

        public void PrintCell(bool isAttackCoordinate)
        {
            if (isAttackCoordinate)
            {
                Console.Write("X ");
            }
            else
            {
                Console.Write("- ");
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Morpion3Dimension.Model
{
    public enum Symbol { empty = 0, circle = 1, cross = -1 }
    public class Grid
    {
        public Square[,,] grid = new Square[3, 3, 3];
        public Grid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        grid[i, j, k] = new Square();
                    }
                }
            }
        }

        public object this[int i, int j, int k]
        {
            get
            {
                return ((Square)this.grid[i, j, k]);
            }
            set
            {
                this.grid[i, j, k] = (Square)value;
            }
        }

        public Square FindSquare(Move move)
        {
            return ((Square)grid[move.x, move.y, move.z]);
        }

        public void PlayMove(Move move, Symbol symbol)
        {
            Square square = this.FindSquare(move);
            square.symbol = symbol;
        }
        public override string ToString()
        {
            string representation = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        representation = representation + grid[i, j, k].ToString();
                    }
                    representation = representation + "\n";
                }
                representation = representation + "\n";
            }
            return (representation);
        }

        public string gridToString()
        {
            string data = "GAMESTATE";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        data = data + "|" + ((int)grid[i, j, k].symbol).ToString();
                    }
                }
            }
            return (data);
        }

        public static Grid stringToGrid(string data)
        {
            Grid grid = new Grid();

            if (data.Substring(0, 9) != "GAMESTATE")
            {
                throw (new Exception("wrong gamestate format"));
            }

            Console.WriteLine(data);

            string[] dataString = data.Substring(10).Split('|');
            int[] states = new int[27];
            for (int i = 0; i < 27; i++)
            {
                Console.WriteLine(dataString[i]);
                try
                {
                    states[i] = Int32.Parse(dataString[i]);
                }
                catch
                {
                    Console.Write($"could not convert {dataString[i]}");
                }
            }

            int currentIndex = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Square square = new Square();
                        square.symbol = (Symbol)states[currentIndex];
                        grid[i, j, k] = square;
                        currentIndex++;
                    }
                }
            }
            return (grid);
        }
    }
}

using Morpion3Dimension.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morpion3Dimension.ConsoleClient
{
    public class ConsoleInterface
    {
        public ConsoleInterface()
        {
            
            bool success = false;
            while (success == false)
            {
                try
                {
                    Console.WriteLine("Write the ip you want to connect");
                    string arg = Console.ReadLine();
                    new ConnectionClient(this).Connect(arg);
                    success = true;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                        }
            }
            
        }
        public Move AskMove()
        {
            Console.WriteLine("choose square");

            string[] choice = Console.ReadLine().Split(',');



            var coord = (int[]) choice.Select(i => Int32.Parse(i)).ToArray();
            // Console.WriteLine(coord.ToString() + " | " + coord.Length);
            return (new Move(coord));
        }

        public void DisplayGameOver(GameOverMessage mes)
        {
            bool win = mes.win;
            Console.WriteLine($"Game is over, you {(win ? "won" : "lost")}");
        }

        public void DisplayNewGrid(Grid grid)
        {
            Console.WriteLine("Grid");
            Console.WriteLine(grid.ToString());

        }
    }
}

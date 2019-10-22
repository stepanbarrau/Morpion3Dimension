using System;
using System.Collections.Generic;
using System.Text;
using Morpion3Dimension.Model;

namespace Morpion3Dimension.ModelUnitTest
{
    class RandomPlayer:IPlayer
    {
        Symbol symbol;
        Random rand = new Random();
        public RandomPlayer(Symbol symbol)
        {
            this.symbol = symbol;
        }
        public Symbol GetSymbol()
        {
            return (symbol);
        }
        public Move AskMove()
        {
            int x = rand.Next(0, 2);
            int y = rand.Next(0, 2);
            int z = rand.Next(0, 2);
            int[] coord = { x, y, z };

            return (new Move(coord));
        }
        public void SendGameOver(WinType winType, Position[] winningSequence)
        {
            Console.WriteLine($"Game Over, is {symbol} the winner? {winType}");
        }

        public void SendGrid(Grid grid) {}
    }
}

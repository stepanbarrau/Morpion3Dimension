using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morpion3Dimension.Model
{
    public class Rules
    {
        public Position[] winningSequence = new Position[3];

        public bool isValidMove(Move move, Symbol symbol, Grid grid)
        {
            if (grid.FindSquare(move).symbol == Symbol.empty)
            {
                return (true);
            }
            return (false);
        }
        public bool winCheck(Move move, Symbol symbol, Grid grid)
        {
            var range = Enumerable.Range(0, 3);
            var directions_ = from x in range from y in range from z in range select new { x, y, z };
            var directions = directions_.Select((i) => new { x = i.x - 1, y = i.y - 1, z = i.z - 1 });

            foreach (var direction in directions)
            {
                if ((direction.x, direction.y, direction.z) != (0, 0, 0))
                {
                    Position position = new Position(move.x, move.y, move.z);
                    int consecutives = 0;
                    this.winningSequence = new Position[3];
                    this.winningSequence[0] = position;

                    try
                    {
                        Square next = (Square)grid[position.x + direction.x, position.y + direction.y, position.z + direction.z];
                        while (next.symbol == symbol)
                        {
                            this.winningSequence[consecutives+1] = position;
                            position = new Position(position.x + direction.x, position.y + direction.y, position.z + direction.z );
                            consecutives++;

                            next = (Square)grid[position.x + direction.x, position.y + direction.y, position.z + direction.z];
                        }
                    }
                    catch { };
                    position = new Position(move.x, move.y, move.z);
                    try
                    {
                        Square next = (Square)grid[position.x - direction.x, position.y - direction.y, position.z - direction.z];
                        while (next.symbol == symbol)
                        {
                            this.winningSequence[consecutives+1] = position;
                            position = new Position(position.x - direction.x, position.y - direction.y, position.z - direction.z);
                            consecutives++;
                            next = (Square)grid[position.x - direction.x, position.y - direction.y, position.z - direction.z];



                        }
                    }
                    catch { };

                    if (consecutives >= 2)
                    {
                        return (true);
                    }

                }

            }
            return (false);


        }

        public bool IsDraw(Grid grid)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Square square = (Square)grid[i, j, k];
                        if (square.symbol == Symbol.empty)
                        {
                            return (false);
                        }
                    }
                }
            }
            return (true);
        }
    }

}

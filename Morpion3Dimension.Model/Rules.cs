using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morpion3Dimension.Model
{
    class Rules
    {

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
                        var position = new { x = move.x, y = move.y, z = move.z };
                        int consecutives = 0;

                        try
                        {
                            Square next = (Square)grid[position.x + direction.x, position.y + direction.y, position.z + direction.z];
                            while (next.symbol == symbol)
                            {
                                position = new { x = position.x + direction.x, y = position.y + direction.y, z = position.z + direction.z };
                                consecutives++;

                                next = (Square)grid[position.x + direction.x, position.y + direction.y, position.z + direction.z];


                            }
                        }
                        catch { };
                        position = new { x = move.x, y = move.y, z = move.z };
                        try
                        {
                            Square next = (Square)grid[position.x - direction.x, position.y - direction.y, position.z - direction.z];
                            while (next.symbol == symbol)
                            {
                                position = new { x = position.x - direction.x, y = position.y - direction.y, z = position.z - direction.z };
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

        internal bool IsDraw(Grid grid)
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

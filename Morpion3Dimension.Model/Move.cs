using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morpion3Dimension.Model
{
    public class Move
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public int z { get; private set; }
        public Move(int[] coord)
        {
            (this.x, this.y, this.z) = (coord[0], coord[1], coord[2]);
        }

        //this part is for easy serialisation
        public string moveToString()
        {
            return ($"MOVE|{x}|{y}|{z}");
        }

        public static Move StringToMove(string data)
        {
            if (data.Substring(0, 4) != "MOVE")
            {
                throw (new Exception("wrong move format"));
            }
            int[] coord = new int[3];
            string dataString = data.Substring(5);
            coord = (int[])dataString.Split('|').Select(n => Int32.Parse(n)).ToArray();

            return (new Move(coord));
        }

    }
}

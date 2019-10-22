using System;
using System.Collections.Generic;
using System.Text;

namespace Morpion3Dimension.Model
{
    public class Position
    {
        public int x;
        public int y;
        public int z;

        public override string ToString()
        {
            return($"{x},{y},{z}");
        }

        public static Position StringToPosition(string stringData)
        {
            string[] coordStrings = stringData.Split(',');
            int x_ = Int32.Parse(coordStrings[0]);
            int y_ = Int32.Parse(coordStrings[1]);
            int z_ = Int32.Parse(coordStrings[2]);

            return (new Position(x_, y_, z_));
        }

        public Position(int x, int y, int z)
        {
            (this.x, this.y, this.z) = (x, y, z);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morpion3Dimension.Model
{
    public class Move : Message
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public int z { get; private set; }
        public Move(int[] coord)
        {
            (this.x, this.y, this.z) = (coord[0], coord[1], coord[2]);
            type = MessageType.move;
        }

        

        public override string DataToString()
        {
            return $"{x}|{y}|{z}";
        }

        public Move(byte[] bytes)
        {   
            if( Message.GetMessageType(bytes) != MessageType.move)
            {
                throw new Exception("Error: trying to deserialize to move an object with wrong Messagetype");
            }
            var data = Message.GetData(bytes);

            int[] coord = new int[3];
            try
            {
                coord = (int[])data.Split('|').Select(n => Int32.Parse(n)).ToArray();
                (this.x, this.y, this.z) = (coord[0], coord[1], coord[2]);
                type = MessageType.move;
            }
            catch
            {
                throw (new Exception("Error : couldn't deserialize the move"));
            }
        }
    }
}

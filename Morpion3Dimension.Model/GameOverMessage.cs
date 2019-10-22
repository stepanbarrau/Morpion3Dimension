using System;
using System.Collections.Generic;
using System.Text;

namespace Morpion3Dimension.Model
{
    public enum WinType
    {
        win = 0,
        lose = 1,
        noContest = 2,
    }

    public class GameOverMessage : Message
    {
        public WinType winType;
        public Position[] winningSequence;

        public GameOverMessage(WinType winType, Position[] winningSequence)
        {
            this.type = MessageType.gameOver;
            this.winType = winType;
            this.winningSequence = winningSequence;
        }

        public GameOverMessage(byte[] bytes)
        {
            if (Message.GetMessageType(bytes) != MessageType.gameOver)
            {
                throw new Exception("Error: trying to deserialize to move an object with wrong Messagetype");
            }
            var data = Message.GetData(bytes);
            string[] dataString = data.Split('|');
            try
            {
                this.winType = (WinType)Int32.Parse(dataString[0]);
                Position p1 = Position.StringToPosition(dataString[1]);
                Position p2 = Position.StringToPosition(dataString[2]);
                Position p3 = Position.StringToPosition(dataString[3]);
                this.winningSequence = new Position[]{ p1, p2, p3 };
            }
            catch
            {
                throw (new Exception("Error : couldn't deserialize the game over message"));
            }

        }
        public override string DataToString()
        {
            return($"{winType}|{winningSequence[0].ToString()}|{winningSequence[1].ToString()}|{winningSequence[2].ToString()}");
        }
    }
}

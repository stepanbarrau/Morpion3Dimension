using System;
using System.Collections.Generic;
using System.Text;

namespace Morpion3Dimension.Model
{
    public class GameOverMessage : Message
    {
        private bool win;

        public GameOverMessage(bool win)
        {
            this.type = MessageType.gameOver;
            this.win = win;
        }

        public GameOverMessage(byte[] bytes)
        {
            if (Message.GetMessageType(bytes) != MessageType.gameOver)
            {
                throw new Exception("Error: trying to deserialize to move an object with wrong Messagetype");
            }
            var data = Message.GetData(bytes); try
            {
                win = bool.Parse(data);
                type = MessageType.gameOver;
            }
            catch
            {
                throw (new Exception("Error : couldn't deserialize the game over message"));
            }

        }
        public override string DataToString()
        {
            return win.ToString();
        }
    }
}

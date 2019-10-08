using System;
using System.Collections.Generic;
using System.Text;

namespace Morpion3Dimension.Model
{
    public enum MessageType : int  { move, grid, gameOver, unknown};
    public abstract class Message
    {
        public MessageType type;
        public abstract string DataToString();

        public static MessageType GetMessageType(byte[] data)
        {
            var strData = Encoding.UTF8.GetString(data);
            string type = strData.Split('|')[0];
            switch (type)
            {
                case "GRID":
                    return MessageType.grid;
                case "OVER":
                    return MessageType.gameOver;
                case "MOVE":
                    return MessageType.move;
                default:
                    return MessageType.unknown;

            }
        }
        public static string GetData(byte[] data)
        {
            var strData = Encoding.UTF8.GetString(data);
            int offset = strData.Split('|')[0].Length;
            return strData.Substring(offset + 1);

        }

        public string TypeToString()
        {
            switch (type)
            {
                case MessageType.grid:
                    return "GRID";
                case MessageType.gameOver:
                    return "OVER";
                case MessageType.move:
                    return "MOVE";
                
                // if type doesn't belong to the known types, return "UNKN
                default:
                    return "UNKNOWN";
            }
        }

        public string MessageToString()
        {
            var body = TypeToString() + "|" + DataToString();
            return body;
        }
        


    }
}

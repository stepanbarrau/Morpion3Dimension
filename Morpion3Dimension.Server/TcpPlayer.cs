using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Morpion3Dimension.Model;

/* int length;
                while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    builder.Append(Encoding.ASCII.GetString(bytes, 0, length));
                }

                var msg = builder.ToString();*/
namespace Morpion3Dimension.Server
{
    public class TcpPlayer : IPlayer
    {
        TcpClient client;
        NetworkStream stream;
        Symbol symbol;

        public TcpPlayer(TcpClient client, Symbol symbol)
        {
            this.client = client;
            this.symbol = symbol;
            stream = client.GetStream();
        }
        public Move AskMove()
        {
            Byte[] bytes = new Byte[1024];
            bool waitingMove = true;
            Move move = null;
            while (waitingMove){

                try
                {
                    stream.Write(Encoding.UTF8.GetBytes("MOVE"));
                    stream.Read(bytes, 0, bytes.Length);
                    move = new Move(bytes);
                    waitingMove = false;

                }
                catch { }                
            }
            return move;

        }
    

        public Symbol GetSymbol()
        {
            return symbol;
        }

        public void SendGameOver(bool victory)
        {
            var data = Encoding.UTF8.GetBytes("");
            stream.Write(data, 0, data.Length);
        }

        public void SendGrid(Grid grid)
        {
            throw new NotImplementedException();
        }
    }
}

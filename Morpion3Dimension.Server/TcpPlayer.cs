using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        IPEndPoint ipep;

        // use to alert the game that the client is disconnected
        public Model.Del disconnect;

        public TcpPlayer(TcpClient client, Symbol symbol)
        {
            this.client = client;
            this.ipep = (IPEndPoint)client.Client.RemoteEndPoint;
            this.symbol = symbol;
            stream = client.GetStream();
            Console.WriteLine($"new TCPPlayer created : IP = {ipep.Address.ToString()}, port = {ipep.Port.ToString()}");
        }

  
        
        public bool checkConnection()
        {
            try
            {
                if (!client.Connected)
                {
                    Console.WriteLine("TCPclient is not connected anymore");
                    disconnect(this);
                    return false;
                }
            } catch (Exception e)
            {
                Console.WriteLine("error occured while checking client connection", e.StackTrace);
                disconnect(this);
                return false;
            }
            return true;
        }
        
        public Move AskMove()
        {   
            Byte[] bytes = new Byte[1024];
            bool waitingMove = true;
            Move move = null;
            while (waitingMove){
                if (!checkConnection())
                {
                    return null;
                }
                try
                {
                    stream.Write(Encoding.UTF8.GetBytes("MOVE"));
                    Console.WriteLine($"asked a move from IP = {ipep.Address.ToString()}, port = {ipep.Port.ToString()}");
                    Int32 i = stream.Read(bytes, 0, bytes.Length);
                    move = new Move(bytes.Take(i).ToArray());
                    Console.WriteLine($"received a move from IP = {ipep.Address.ToString()}, port = {ipep.Port.ToString()}");
                    waitingMove = false;

                }
                catch(System.ObjectDisposedException e) { disconnect(this); }
                catch (Exception e) { Console.WriteLine(e.ToString()); }                
            }
            return move;

        }
    

        public Symbol GetSymbol()
        {
            return symbol;
        }

        public void SendGameOver(WinType winType, Position[] winningSequence)
        {
            var gameOver = new GameOverMessage(winType, winningSequence);
            var data = Encoding.UTF8.GetBytes(gameOver.MessageToString());
            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch (System.IO.IOException e)
            {
            }
        }

        public void SendGrid(Grid grid)
        {
            if (!checkConnection())
            {
                return;
            }
            var data = Encoding.UTF8.GetBytes(grid.MessageToString());
            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch (System.IO.IOException e)
            {
            }
        }

        public void SetDisconnection(Model.Del handler)
        {
            this.disconnect = handler;
        }
    }
}

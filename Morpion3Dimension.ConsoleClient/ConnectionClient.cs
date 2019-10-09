using Morpion3Dimension.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Morpion3Dimension.ConsoleClient
{
    public class ConnectionClient
    {
        IPEndPoint ipe;
        TcpClient client;
        NetworkStream stream;
        byte[] data = new Byte[1024];
        bool playing;
        private ConsoleInterface inte;

        public ConnectionClient( ConsoleInterface inte)
        {
            this.inte = inte;
        }
        
        public void Connect(string arg)
        {
            IPAddress ipAddress;
            try
            {
                ipAddress = IPAddress.Parse(arg);
            }
            catch
            {
                throw new Exception("This is not an ipAddress, an ip adress looks like this 0.0.0.0");
            }
            ipe = new IPEndPoint(ipAddress, 8080);
            client = new TcpClient();
            client.Connect(ipe);
            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            stream = client.GetStream();

            playing = true;

            // stream.BeginRead(data, 0, data.Length, handleMessage, null);
            while(playing)
            {
                
                Int32 i = stream.Read(data, 0, data.Length);
                byte[] responseData = data.Take(i).ToArray();
                if (i > 0)
                {
                    Console.WriteLine($"Received: {Encoding.UTF8.GetString(responseData)}");
                }
                handleMessage(responseData);
            }


        }

    

        private void handleMessage(byte[] data)
        {
            MessageType type = Message.GetMessageType(data);
            switch(type)
            {
                case MessageType.move:
                    var move = inte.AskMove();
                    SendMove(move);
                    break;
                case MessageType.gameOver:
                    var mes = new GameOverMessage(data);
                    inte.DisplayGameOver(mes);
                    break;
                case MessageType.grid:
                    var grid = new Grid(data);
                    inte.DisplayNewGrid(grid);
                    break;
                default:
                    break;
            }
        }

        public void SendMove(Move move)
        {
            data = Encoding.UTF8.GetBytes(move.MessageToString());
            stream.Write(data, 0, data.Length);
        }

        private void StopConnection()
        {
            playing = false;
            client.Close();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Morpion3Dimension.Server
{
    public class ConnectionListener
    {
        // Singleton 
        static ConnectionListener instance = null;
        static IPAddress adress = IPAddress.Parse("127.0.0.1");
        TcpListener listener;
        private GameStarter gameStarter;

        public static ConnectionListener Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectionListener();
                }
                return instance;
            }
        }

        private ConnectionListener()
        {
            gameStarter = new GameStarter();
        }

        public void startListening()
        {
            Console.WriteLine("Start listening");
            listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();
            ThreadPool.QueueUserWorkItem(this.ListenerWorker, null);
        }
        private void ListenerWorker(object state)
        {
            while (listener != null)
            {
                var client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(this.HandleClientWorker, client);
            }
        }

        private void HandleClientWorker(object token)
        {
            Byte[] bytes = new Byte[1024];
            StringBuilder builder = new StringBuilder();
            var client = token as TcpClient;
            var stream = client.GetStream();

                
                // pass client to the GameStarter class
            gameStarter.AddClient(client);
            while(client.Connected)
            {

                try
                {
                    // ping client
                    var b = new byte[0];
                    client.GetStream().Write(b,0,b.Length);
                } catch (System.IO.IOException e) { }
                Thread.Sleep(100);
            }
            Console.WriteLine("A client has disconnected");
            gameStarter.RemoveClient(client);
            client.Dispose();

        }



        public void stopListening()
        {
            if (listener != null)
            {
                listener.Stop();
                listener = null;
            }
        }
    }
}

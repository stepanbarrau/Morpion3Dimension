using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Morpion3Dimension.Model;

namespace Morpion3Dimension.Server
{
    public class GameStarter
    {
        private ConcurrentQueue<TcpClient> tcpClients = null;

        private TcpClient cl1;

        private TcpClient cl2;

        public void AddClient(TcpClient client)
        {
            tcpClients.Enqueue(client);
            if (tcpClients.Count >= 2)

            {
                tcpClients.TryDequeue(out cl1);
                tcpClients.TryDequeue(out cl2);
                if (cl1 != null & cl2 != null)
                {
                    ThreadPool.QueueUserWorkItem(startGame);
                }
            }
        }

        private void startGame(object state)
        {
            new Game(new TcpPlayer(cl1, Symbol.circle), new TcpPlayer(cl2, Symbol.cross));
        }
    }
}

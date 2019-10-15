using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
/*using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;*/

namespace Morpion3Dimension.Server
{
    public class Program
    {
        static ConnectionListener _instance;
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            Console.WriteLine("Hello World");
            _instance = ConnectionListener.Instance;
            _instance.startListening();
            while(true)
            {
                Thread.Sleep(100);
            }
        }

       /* public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();*/
    }
}

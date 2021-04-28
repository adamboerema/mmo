using System;
using Common.Network.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Server
{
    public class Program
    {
        private static SocketServer socketServer;

        public static void Main(string[] args)
        {
            socketServer = new SocketServer("127.0.0.1", 7777);
            socketServer.Listen();

            Console.ReadKey();
            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

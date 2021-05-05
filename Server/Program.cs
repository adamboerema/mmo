using System;
using Common.Network.Server.Socket;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Server
{
    public class Program
    {
        private static TcpSocketServer server;

        public static void Main(string[] args)
        {
            server = new TcpSocketServer(7777);
            server.Start();

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

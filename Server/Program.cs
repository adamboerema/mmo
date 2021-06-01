using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Server.Configuration;
using Sever;

namespace Server
{
    public class Program
    {
        private static GameServer server;

        public static void Main(string[] args)
        {
            var configuration = new ServerConfiguration();
            server = new GameServer(configuration);
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

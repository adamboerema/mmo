using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesktopClient
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new DesktopClient();
            game.Run();

            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<GameServerWorker>();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IServerConfiguration, ServerConfiguration>();
                    services.AddSingleton<IServer, TcpSocketServer>();
                    services.AddSingleton<GameServer>();

                    services.AddSingleton<IReceiverPacketBus, ReceiverPacketBus>();
                    services.AddSingleton<IDispatchPacketBus, DispatchPacketBus>();

                    services.AddSingleton<IConnectionManager, ConnectionManager>();
                    services.AddSingleton<IConnectionDispatch, ConnectionDispatch>();
                    services.AddSingleton<IConnectionReceiver, ConnectionReceiver>();

                    services.AddSingleton<AuthHandler>();
                });
        }
    }
}

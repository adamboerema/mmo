using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Bus.Connection;
using Server.Bus.Packet;
using Server.Configuration;
using Server.Network.Connection;
using Server.Network.Handler;
using Server.Network.Server;
using Sever;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                    services.AddSingleton<IConnectionBus, ConnectionBus>();

                    services.AddSingleton<IConnectionManager, ConnectionManager>();
                    services.AddSingleton<IConnectionDispatch, ConnectionDispatch>();
                    services.AddSingleton<IConnectionReceiver, ConnectionReceiver>();

                    services.AddSingleton<AuthHandler>();
                });
        }
    }
}

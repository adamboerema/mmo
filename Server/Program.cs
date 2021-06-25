using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Bus.Connection;
using Server.Bus.Packet;
using Server.Configuration;
using Server.Engine.Player;
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
                    services.AddScoped<IServerConfiguration, ServerConfiguration>();
                    services.AddScoped<IServer, TcpSocketServer>();
                    services.AddScoped<GameServer>();

                    services.AddScoped<IReceiverPacketBus, ReceiverPacketBus>();
                    services.AddScoped<IDispatchPacketBus, DispatchPacketBus>();
                    services.AddScoped<IConnectionBus, ConnectionBus>();

                    services.AddScoped<IConnectionManager, ConnectionManager>();
                    services.AddScoped<IConnectionDispatch, ConnectionDispatch>();
                    services.AddScoped<IConnectionReceiver, ConnectionReceiver>();

                    services.AddScoped<IPlayerManager, PlayerManager>();

                    services.AddSingleton<AuthHandler>();
                });
        }
    }
}

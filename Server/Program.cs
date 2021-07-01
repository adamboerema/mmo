using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Auth;
using Server.Bus.Connection;
using Server.Bus.Packet;
using Server.Configuration;
using Server.Engine.Player;
using Server.Network.Connection;
using Server.Network.Handler;
using Server.Network.Handler.Factory;
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
                    // Server setup
                    services.AddScoped<IServerConfiguration, ServerConfiguration>();
                    services.AddScoped<IServer, TcpSocketServer>();
                    services.AddScoped<GameServer>();

                    // Packet Buses
                    services.AddScoped<IReceiverPacketBus, ReceiverPacketBus>();
                    services.AddScoped<IDispatchPacketBus, DispatchPacketBus>();
                    services.AddScoped<IConnectionBus, ConnectionBus>();

                    // Connection routing
                    services.AddScoped<IConnectionManager, ConnectionManager>();
                    services.AddScoped<IConnectionDispatch, ConnectionDispatch>();
                    services.AddScoped<IConnectionReceiver, ConnectionReceiver>();

                    // Managers
                    services.AddScoped<IPlayerManager, PlayerManager>();
                    services.AddScoped<IAuthManager, AuthManager>();

                    // Receiver handlers
                    services.AddScoped<IHandlerRouter, HandlerRouter>();
                    services.AddScoped<AuthHandler>();
                    services.AddScoped<MovementHandler>();
                });
        }

    }
}

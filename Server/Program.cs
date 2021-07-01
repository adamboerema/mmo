using System;
using System.Collections.Generic;
using Common.Network.Definitions;
using Common.Network.Packets.Auth;
using Common.Network.Packets.Movement;
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
                    BuildPacketHandlers(services);
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
                    services.AddScoped<IAuthManager, AuthManager>();
                });
        }

        private static IServiceCollection BuildPacketHandlers(IServiceCollection services)
        {
            services.AddScoped<IPacketHandler<LoginRequestPacket>, AuthHandler>();
            services.AddScoped<IPacketHandler<MovementInputPacket>, MovementHandler>();

            services.AddScoped<IHandlerFactory, HandlerFactory>((builder) =>
            {
                var factory = new HandlerFactory();
                factory.RegisterHandler(builder.GetRequiredService<IPacketHandler<LoginRequestPacket>>());
                factory.RegisterHandler(builder.GetRequiredService<IPacketHandler<LoginRequestPacket>>());
                return factory;
            });

            return services;
        }

    }
}

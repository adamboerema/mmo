using System;
using Common.Model;
using CommonClient.Bus.Packet;
using CommonClient.Configuration;
using CommonClient.Engine.Player;
using CommonClient.Network.Handler;
using CommonClient.Network.Handler.Router;
using CommonClient.Network.Socket;
using CommonClient.Player;
using CommonClient.Store;
using Microsoft.Extensions.DependencyInjection;

namespace CommonClient
{
    public class GameServices
    {
        public IServiceProvider ServiceProvider;

        public GameServices(IGameConfiguration configuration)
        {
            var serviceCollection = BuildServiceCollection(configuration);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private IServiceCollection BuildServiceCollection(
            IGameConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();
            BuildConfiguration(serviceCollection, configuration);
            BuildServices(serviceCollection);
            BuildHandlers(serviceCollection);
            return serviceCollection;
        }

        /// <summary>
        /// Build Configuration
        /// </summary>
        /// <param name="configuration">Custom configuration</param>
        private void BuildConfiguration(
            IServiceCollection serviceCollection,
            IGameConfiguration configuration)
        {
            serviceCollection.AddScoped((builder) =>
            {
                return configuration;
            });
        }

        /// <summary>
        /// Build Services
        /// </summary>
        private void BuildServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IClient, TcpSocketClient>();
            serviceCollection.AddScoped<IDispatchPacketBus, DispatchPacketBus>();
            serviceCollection.AddScoped<IReceiverPacketBus, ReceiverPacketBus>();

            serviceCollection.AddScoped<IPlayerStore, PlayerStore>();
            serviceCollection.AddScoped<IPlayerManager, PlayerManager>();
        }

        /// <summary>
        /// Build Handlers
        /// </summary>
        private void BuildHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IHandlerRouter, HandlerRouter>();
            serviceCollection.AddScoped<PlayerHandler>();
            serviceCollection.AddScoped<MovementHandler>();
        }
    }
}

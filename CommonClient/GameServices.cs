using System;
using CommonClient.Bus.Packet;
using CommonClient.Configuration;
using CommonClient.Engine.Movement;
using CommonClient.Engine.Player;
using CommonClient.Network.Handler;
using CommonClient.Network.Handler.Router;
using CommonClient.Network.Receiver;
using CommonClient.Network.Socket;
using CommonClient.Store;
using Microsoft.Extensions.DependencyInjection;

namespace CommonClient
{
    public static class GameServices
    {
        private static IServiceProvider ServiceProvider;

        /// <summary>
        /// Initialize the container
        /// </summary>
        /// <param name="configuration"></param>
        public static void Initialize(IGameConfiguration configuration)
        {
            var serviceCollection = BuildServiceCollection(configuration);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Get Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Builds all of the containers
        /// </summary>
        /// <param name="configuration">Game configuration</param>
        /// <returns></returns>
        private static IServiceCollection BuildServiceCollection(
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
        private static void BuildConfiguration(
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
        private static void BuildServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IClient, TcpSocketClient>();
            serviceCollection.AddScoped<IDispatchPacketBus, DispatchPacketBus>();
            serviceCollection.AddScoped<IReceiverPacketBus, ReceiverPacketBus>();
            serviceCollection.AddScoped<IConnectionReceiver, ConnectionReceiver>();

            serviceCollection.AddScoped<IPlayerStore, PlayersStore>();
            serviceCollection.AddScoped<IPlayerManager, PlayerManager>();
            serviceCollection.AddScoped<IMovementManager, MovementManager>();
        }

        /// <summary>
        /// Build Handlers
        /// </summary>
        private static void BuildHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IHandlerRouter, HandlerRouter>();
            serviceCollection.AddScoped<PlayerHandler>();
            serviceCollection.AddScoped<MovementHandler>();
        }
    }
}

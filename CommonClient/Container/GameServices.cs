using System;
using CommonClient.Bus.Packet;
using CommonClient.Configuration;

namespace CommonClient.Container
{
    public static class GameServices
    {
        private static bool isInitialized = false;

        private static IGameClient _gameClient;

        private static IDispatchPacketBus _dispatchPacketBus = new DispatchPacketBus();

        private static IReceiverPacketBus _receiverPacketBus = new ReceiverPacketBus();
            
        public static void Initialize(IGameConfiguration configuration)
        {
            isInitialized = true;
            _gameClient = new GameClient(configuration);
            GameContainer.AddService(_gameClient);
            GameContainer.AddService(_dispatchPacketBus);
            GameContainer.AddService(_receiverPacketBus);
        }

        public static T GetService<T>()
        {
            if(!isInitialized)
            {
                throw new Exception("Game Services must be initalized");
            }
            return GameContainer.GetService<T>();
        }

        public static T GetLazyService<T>()
        {
            if (!isInitialized)
            {
                throw new Exception("Game Services must be initalized");
            }
            return GameContainer.GetLazyService<T>();
        }
    }
}

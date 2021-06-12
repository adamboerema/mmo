using System;
using CommonClient;
using CommonClient.Configuration;
using DesktopClient.Configuration;

namespace DesktopClient.Container
{
    public static class GameServices
    {
        private static bool isInitialized = false;

        private static IGameConfiguration _gameConfiguration = new ClientConfiguration();

        private static IGameClient _gameClient = new GameClient(_gameConfiguration);
            
        public static void Initialize()
        {
            isInitialized = true;
            GameContainer.AddService(_gameClient);
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

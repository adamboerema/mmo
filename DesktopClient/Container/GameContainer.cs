using System;
using Microsoft.Xna.Framework;

namespace DesktopClient.Container
{
    public static class GameContainer
    {
        private static GameServiceContainer container;

        public static GameServiceContainer Instance
        {
            get
            {
                if (container == null)
                {
                    container = new GameServiceContainer();
                }
                return container;
            }
        }

        public static T GetService<T>()
        {
            return (T)Instance.GetService(typeof(T));
        }

        public static T GetLazyService<T>()
        {
            return ((Lazy<T>)Instance.GetService(typeof(Lazy<T>))).Value;
        }

        public static void AddService<T>(T service)
        {
            Instance.AddService(typeof(T), service);
        }

        public static void AddLazyService<T>(Lazy<T> service)
        {
            Instance.AddService(typeof(Lazy<T>), service);
        }

        public static void RemoveService<T>()
        {
            Instance.RemoveService(typeof(T));
        }
    }
}

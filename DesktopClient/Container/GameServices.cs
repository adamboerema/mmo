using System;
using CommonClient.Bus.Packet;

namespace DesktopClient.Container
{
    public static class GameServices
    {
        private static Lazy<DispatchPacketBus> _dispatchPacketBus
            = new Lazy<DispatchPacketBus>(() => new DispatchPacketBus());

        private static Lazy<ReceiverPacketBus> _receiverPacketBus
            = new Lazy<ReceiverPacketBus>(() => new ReceiverPacketBus());

        public static void Initialize()
        {
            GameContainer.AddLazyService(_dispatchPacketBus);
            GameContainer.AddLazyService(_receiverPacketBus);
        }
    }
}

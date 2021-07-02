using System;
using Common.Bus;
using Common.Definitions;
using CommonClient.Bus.Packet;

namespace CommonClient
{
    public interface IGameClient
    {
        public void Start();

        public void Close();

        public void Send(IPacket packet);

        public void RegisterListener(IEventBusListener<PacketEvent> eventListener);

        public void UnregisterListener(IEventBusListener<PacketEvent> eventListener);
    }
}

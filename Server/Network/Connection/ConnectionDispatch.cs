using System;
using Common.Network.Packet.Definitions;
using Server.Bus;
using Server.Bus.Packet;

namespace Server.Network.Connection
{
    public class ConnectionDispatch: IConnectionDispatch, IEventBusListener<PacketEvent>
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IDispatchPacketBus _dispatchBus;

        public ConnectionDispatch(
            IConnectionManager connectionManager,
            IDispatchPacketBus dispatchBus)
        {
            _connectionManager = connectionManager;
            _dispatchBus = dispatchBus;
            _dispatchBus.Subscribe(this);
        }

        public void Dispatch(string connectionId, IPacket packet)
        {
            _connectionManager.Send(connectionId, packet);
        }

        public void Handle(PacketEvent eventObject)
        {
            _connectionManager.Send(eventObject.ConnectionId, eventObject.Packet);
        }

        public void Close()
        {
            _dispatchBus.Unsubscribe(this);
        }
    }
}

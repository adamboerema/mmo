using System;
using Common.Network.Packet.Definitions;
using Server.Bus;
using Server.Bus.Packet;

namespace Server.Network.Connection
{
    public class ConnectionDispatch: IConnectionDispatch, IEventBusListener<IPacketEvent>
    {
        private readonly ConnectionManager _connectionManager;
        private readonly IDispatchPacketBus _dispatchBus;

        public ConnectionDispatch(
            ConnectionManager connectionManager,
            IDispatchPacketBus dispatchBus)
        {
            _connectionManager = connectionManager;
            _dispatchBus = dispatchBus;
            _dispatchBus.Subscribe(this);
        }

        public void Dispatch(string connectionId, IPacketEvent packet)
        {
            _connectionManager.Send(connectionId, packet);
        }

        public void Handle(IPacketEvent eventObject)
        {
            Dispatch(pac)
        }
    }
}

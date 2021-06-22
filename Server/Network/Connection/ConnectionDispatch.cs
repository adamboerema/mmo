using System;
using Common.Bus;
using Common.Network.Packet.Definitions;
using Server.Bus.Packet;

namespace Server.Network.Connection
{
    public class ConnectionDispatch: IConnectionDispatch, IEventBusListener<DispatchPacketEvent>
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

        public void Handle(DispatchPacketEvent eventObject)
        {
            switch(eventObject.Type)
            {
                case DispatchType.ALL:
                    _connectionManager.SendAll(eventObject.Packet);
                    break;
                case DispatchType.CONNECTION:
                    _connectionManager.Send(eventObject.ConnectionId, eventObject.Packet);
                    break;
            }

        }

        public void Close()
        {
            _dispatchBus.Unsubscribe(this);
        }
    }
}

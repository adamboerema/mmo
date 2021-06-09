using System;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Definitions.Schema.Auth;
using Server.Bus;
using Server.Bus.Packet;
using Server.Network.Handler;

namespace Server.Network.Connection
{
    public class ConnectionReceiver: IConnectionReceiver
    {
        private readonly IReceiverPacketBus _packetBus;

        public ConnectionReceiver(IReceiverPacketBus packetBus)
        {
            _packetBus = packetBus;
        }

        public void Receive(string connectionId, IPacketEvent packet)
        {
            switch(packet.Id)
            {
                case Definitions.LOGIN_REQUEST:
                    var handler = new AuthHandler(_packetBus);
                    handler.Handle(connectionId, packet as LoginRequestPacket);
                    break;
            }
        }
    }
}

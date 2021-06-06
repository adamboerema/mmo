using System;
using Common.Network.Packet.Definitions;

namespace Server.Bus.Packet
{
    public class ReceiverPacketBus: EventBus<PacketEvent>, IReceiverPacketBus
    {
        public void Publish(string connectionId, IPacket packet)
        {
            Publish(new PacketEvent
            {
                ConnectionId = connectionId,
                Packet = packet
            });
        }
    }
}

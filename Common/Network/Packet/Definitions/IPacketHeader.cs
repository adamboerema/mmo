using System;
namespace Common.Network.Packet.Definitions
{
    public interface IPacketHeader
    {
        public int PacketId { get; set; }

        public string ConnectionId { get; set; }

    }
}

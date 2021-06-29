using System;
namespace Common.Network.Definitions
{
    public interface IPacketHeader
    {
        public int PacketId { get; set; }

        public string ConnectionId { get; set; }

    }
}

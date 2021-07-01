using System;
using Common.Network.IO;

namespace Common.Network.Definitions
{
    public interface IPacket
    {
        public PacketType Id { get; }

        public IPacket ReadData(PacketReader packetReader);

        public byte[] WriteData(PacketWriter packetWriter);
    }
}

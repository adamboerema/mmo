using System;
using Common.Network.Definitions;
using Common.Network.IO;

namespace Common.Network.Packets.Player
{
    public class PlayerDisconnectPacket : IPacket
    {

        public PacketType Id => PacketType.PLAYER_DISCONNECTED;

        public string PlayerId { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            PlayerId = packetReader.ReadString();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(PlayerId);
            return packetWriter.ToBytes();
        }
    }
}

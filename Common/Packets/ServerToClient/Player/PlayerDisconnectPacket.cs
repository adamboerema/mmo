using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Player
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

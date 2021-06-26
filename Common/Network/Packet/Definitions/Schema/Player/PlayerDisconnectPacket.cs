using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Schema.Player
{
    public class PlayerDisconnectPacket : IPacket
    {
        public PlayerDisconnectPacket()
        {
        }

        public int Id => Definitions.PLAYER_DISCONNECTED;

        public string PlayerId { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            PlayerId = packetReader.ReadString();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger(Id);
            packetWriter.WriteString(PlayerId);
            return packetWriter.ToBytes();
        }
    }
}

using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Schema.Player
{
    public class PlayerConnectPacket: IPacket
    {
        public PlayerConnectPacket()
        {
        }

        public int Id => Definitions.PLAYER_CONNECTED;

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

using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ClientToServer.Player
{
    public class PlayerAttackStartPacket: IPacket
    {
        public PacketType Id => PacketType.PLAYER_ATTACK_START;
        public string PlayerId { get; set; }
        public string TargetId { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            PlayerId = packetReader.ReadString();
            TargetId = packetReader.ReadString();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(PlayerId);
            packetWriter.WriteString(TargetId);
            return packetWriter.ToBytes();
        }
    }
}

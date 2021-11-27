using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ClientToServer.Player
{
    public class PlayerAttackStartPacket: IPacket
    {
        public PacketType Id => PacketType.PLAYER_ATTACK_START;
        public string TargetId { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            TargetId = packetReader.ReadString();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(TargetId);
            return packetWriter.ToBytes();
        }
    }
}

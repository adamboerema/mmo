using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Player
{
    public class PlayerAttackOutputPacket: IPacket
    {
        public PacketType Id => PacketType.PLAYER_ATTACK_OUTPUT;
        public string PlayerId { get; set; }
        public string TargetId { get; set; }
        public int Damage { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            PlayerId = packetReader.ReadString();
            TargetId = packetReader.ReadString();
            Damage = packetReader.ReadInteger();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int) Id);
            packetWriter.WriteString(PlayerId);
            packetWriter.WriteString(TargetId);
            packetWriter.WriteInteger(Damage);
            return packetWriter.ToBytes();
        }
    }
}

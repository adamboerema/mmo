using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ClientToServer.Player
{
    public class PlayerAttackEndPacket: IPacket
    {
        public PacketType Id => PacketType.PLAYER_ATTACK_START;

        public IPacket ReadData(PacketReader packetReader)
        {
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            return packetWriter.ToBytes();
        }
    }
}

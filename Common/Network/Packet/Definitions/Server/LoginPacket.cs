using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Server
{
    public class LoginPacket: IPacket
    {
        public ServerPacketType Type => ServerPacketType.LOGIN;

        public LoginPacket()
        {
        }

        public IPacket ReadData(PacketReader packetReader)
        {
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            return new byte[] { };
        }
    }
}

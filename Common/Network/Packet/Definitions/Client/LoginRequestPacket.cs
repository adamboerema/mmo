using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Client
{
    public class LoginRequestPacket: IPacket
    {
        public int Id => ClientDefinitions.LOGIN_REQUEST;

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

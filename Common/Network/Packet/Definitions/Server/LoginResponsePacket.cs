using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Server
{
    public class LoginResponsePacket: IPacket
    {
        public int Id => ServerDefinitions.LOGIN_RESPONSE;


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

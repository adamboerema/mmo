using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Server
{
    public class LoginPacket: IPacket
    {
        public LoginPacket()
        {
        }

        public IPacket ParseData(PacketReader packetReader)
        {
            return this;
        }
    }
}

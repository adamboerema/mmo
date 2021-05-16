using System;
using Common.Network.Packet.Definitions;

namespace Common.Network.Packet.Manager
{
    public interface IPacketManager
    {
        byte[] Write(IPacket packet);

        IPacket Receive(byte[] bytes);
    }
}

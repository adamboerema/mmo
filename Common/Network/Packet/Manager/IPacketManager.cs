using System;
using Common.Network.Packet.Definitions;

namespace Common.Network.Packet.Manager
{
    public interface IPacketManager
    {
        byte[] Write(byte[] bytes);

        IPacket Receive(byte[] bytes);
    }
}

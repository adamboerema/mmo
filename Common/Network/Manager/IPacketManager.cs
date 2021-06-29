using System;
using Common.Network.Definitions;

namespace Common.Network.Manager
{
    public interface IPacketManager
    {
        byte[] Write(IPacket packet);

        IPacket Receive(byte[] bytes);
    }
}

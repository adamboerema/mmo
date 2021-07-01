using System;
using Common.Definitions;
using Common.Network.Definitions;

namespace Common.Network.Manager
{
    public interface IPacketManager
    {
        byte[] Write(IPacket packet);

        IPacket Receive(byte[] bytes);
    }
}

using System;
namespace Common.Network.Packet.Manager
{
    public interface IPacketManager
    {
        void HandleReceivePacket(byte[] bytes);
    }
}

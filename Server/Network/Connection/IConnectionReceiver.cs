using System;
using Common.Network.Packet.Definitions;

namespace Server.Network.Connection
{
    public interface IConnectionReceiver
    {
        public void Receive(string connectionId, IPacket packet);
    }
}

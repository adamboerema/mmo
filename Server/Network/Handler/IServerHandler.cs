using System;
using Common.Network.Packet.Definitions;

namespace Server.Network.Handler
{
    public interface IServerHandler<T> where T : IPacketEvent
    {
        public void Handle(string connectionId, T packet);
    }
}

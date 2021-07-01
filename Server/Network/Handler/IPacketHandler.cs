using System;
using Common.Definitions;

namespace Server.Network.Handler
{
    public interface IPacketHandler<T> where T : IPacket
    {
        public void Handle(string connectionId, T packet);
    }
}
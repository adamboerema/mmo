using System;
using Common.Definitions;

namespace CommonClient.Network.Handler
{
    public interface IPacketHandler<T> where T : IPacket
    {
        public void Handle(T packet);
    }
}

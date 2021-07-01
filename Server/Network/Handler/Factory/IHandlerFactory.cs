using System;
using Common.Network.Definitions;

namespace Server.Network.Handler.Factory
{
    public interface IHandlerFactory
    {
        public IPacketHandler<T> GetHandler<T>() where T : IPacket;
    }
}

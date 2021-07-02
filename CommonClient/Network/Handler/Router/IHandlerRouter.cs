using System;
using Common.Definitions;

namespace CommonClient.Network.Handler.Router
{
    public interface IHandlerRouter
    {
        public void Route(IPacket packet);
    }
}

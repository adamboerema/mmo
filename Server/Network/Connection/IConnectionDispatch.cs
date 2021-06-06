using System;
using Common.Network.Packet.Definitions;

namespace Server.Network.Connection
{
    public interface IConnectionDispatch
    {
        public void Dispatch(string connectionId, IPacket packet);
    }
}

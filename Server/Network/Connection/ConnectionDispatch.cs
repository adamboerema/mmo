using System;
using Common.Network.Packet.Definitions;

namespace Server.Network.Connection
{
    public class ConnectionDispatch: IConnectionDispatch
    {
        private readonly ConnectionManager _connectionManager;

        public ConnectionDispatch(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Dispatch(string connectionId, IPacket packet)
        {
            _connectionManager.Send(connectionId, packet);
        }
    }
}

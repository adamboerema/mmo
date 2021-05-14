using System.Collections.Generic;
using Common.Network.Packet.Definitions;
using Common.Network.Server.Socket;

namespace Common.Network.Server.Manager
{
    public class ConnectionManager: IConnectionManager
    {
        private Dictionary<string, IConnection> connections;

        public ConnectionManager(int maxConnections)
        {
            connections = new Dictionary<string, IConnection>(maxConnections);
        }

        public void AddConnection(IConnection connection)
        {
            connections.Add(connection.Id, connection);
        }

        public IConnection GetConnection(string id)
        {
            return connections[id];
        }

        public void CloseAllConnections()
        {
            foreach(IConnection connection in connections.Values)
            {
                connection.CloseConnection();
            }
        }

        public void Send(string connectionId, IPacket packet)
        {
            var connection = connections[connectionId];
            connection.Send(packet);
        }
    }
}

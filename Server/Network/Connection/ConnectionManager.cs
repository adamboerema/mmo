using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions;
using Server.Configuration;

namespace Server.Network.Connection
{
    public class ConnectionManager: IConnectionManager, IConnectionReceiver
    {
        private Dictionary<string, IConnection> _connections;

        public ConnectionManager(IServerConfiguration configuration)
        {
            _connections = new Dictionary<string, IConnection>(configuration.MaxConnections);
        }

        public void AddConnection(IConnection connection)
        {
            _connections.Add(connection.Id, connection);
        }

        public IConnection GetConnection(string id)
        {
            return _connections[id];
        }

        public void CloseAllConnections()
        {
            foreach(IConnection connection in _connections.Values)
            {
                connection.CloseConnection();
            }
        }

        public void Send(string connectionId, IPacket packet)
        {
            var connection = _connections[connectionId];
            connection?.Send(packet);
        }

        public void Receive(string connectionId, IPacket packet)
        {
            Console.WriteLine($"Received Packet {packet} from connection {connectionId}");
        }
    }
}

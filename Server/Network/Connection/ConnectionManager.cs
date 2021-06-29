using System;
using System.Collections.Generic;
using Common.Network.Definitions;
using Server.Configuration;

namespace Server.Network.Connection
{
    public class ConnectionManager: IConnectionManager
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

        public IConnection GetConnection(string connectionId)
        {
            return _connections[connectionId];
        }

        public void CloseConnection(string connectionId)
        {
            var connection = _connections[connectionId];
            connection?.CloseConnection();
            _connections.Remove(connectionId);
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

        public void SendAll(IPacket packet)
        {
            foreach(IConnection connection in _connections.Values)
            {
                connection.Send(packet);
            }
        }
    }
}

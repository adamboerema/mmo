using System;
using System.Collections.Concurrent;
using Common.Definitions;
using Server.Configuration;

namespace Server.Network.Connection
{
    public class ConnectionManager: IConnectionManager
    {
        private ConcurrentDictionary<string, IConnection> _connections;
        private const int CONCURRENT_LEVEL = 2;

        public ConnectionManager(IServerConfiguration configuration)
        {
            _connections = new ConcurrentDictionary<string, IConnection>(
                CONCURRENT_LEVEL,
                configuration.MaxConnections);
        }

        public void AddConnection(IConnection connection)
        {
            _connections[connection.Id] = connection;
        }

        public IConnection GetConnection(string connectionId)
        {
            return _connections[connectionId];
        }

        public void CloseConnection(string connectionId)
        {
            if (_connections.ContainsKey(connectionId))
            {
                var connection = _connections[connectionId];
                connection?.CloseConnection();
                _connections.TryRemove(connectionId, out IConnection _);
            }
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
            if(_connections.ContainsKey(connectionId))
            {
                _connections[connectionId].Send(packet);
            }
        }

        public void SendAll(IPacket packet)
        {
            foreach(IConnection connection in _connections.Values)
            {
                connection.Send(packet);
            }
        }

        public void SendAllExcept(string exceptConnectionId, IPacket packet)
        {
            foreach(IConnection connection in _connections.Values)
            {
                if(connection.Id != exceptConnectionId)
                {
                    connection.Send(packet);
                }
            }
        }
    }
}

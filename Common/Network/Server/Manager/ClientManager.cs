using System;
using System.Collections;
using System.Collections.Generic;
using Common.Network.Server.Socket;

namespace Common.Network.Server.Manager
{
    public class ConnectionManager: IConnectionManager
    {
        private Hashtable connections;

        public ConnectionManager(int maxConnections)
        {
            connections = new Hashtable(maxConnections);
        }

        public void AddConnection(IConnection connection)
        {
            connections.Add(connection.Id, connection);
        }
    }
}

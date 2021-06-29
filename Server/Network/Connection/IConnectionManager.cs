using System;
using Common.Network.Definitions;

namespace Server.Network.Connection
{
    public interface IConnectionManager
    {
        /// <summary>
        /// Add Connection to the manager set
        /// </summary>
        /// <param name="connection">Connection to add to the pool</param>
        void AddConnection(IConnection connection);

        /// <summary>
        /// Get Connection from pool
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IConnection GetConnection(string connectionId);

        /// <summary>
        /// Close connection
        /// </summary>
        /// <param name="id"></param>
        void CloseConnection(string connectionId);

        /// <summary>
        /// Close all Connections in pool
        /// </summary>
        void CloseAllConnections();

        /// <summary>
        /// Send packet to connection
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        void Send(string connectionId, IPacket packet);

        /// <summary>
        /// Broadcast to all connections
        /// </summary>
        /// <param name="packet"></param>
        void SendAll(IPacket packet);
    }
}

﻿using System;
using Common.Network.Packet.Definitions;

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
        IConnection GetConnection(string id);

        /// <summary>
        /// Close all Connections in pool
        /// </summary>
        void CloseAllConnections();

        /// <summary>
        /// Send packet to connection
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        void Send(string connectionId, IPacketEvent packet);
    }
}
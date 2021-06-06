using System;
using System.Net.Sockets;
using Common.Network.Packet.Definitions;

namespace Server.Network.Connection
{
    public interface IConnection
    {
        /// <summary>
        /// Connection ID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Connection receives a packet from the client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="packet"></param>
        public delegate void OnReceive(string id, IPacket packet);

        /// <summary>
        /// Start the Connection
        /// </summary>
        public void Start();

        /// <summary>
        /// Close the connection
        /// </summary>
        public void CloseConnection();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes">Packet bytes</param>
        public void Send(IPacket bytes);
    }
}

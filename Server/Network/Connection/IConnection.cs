using System;
using System.Net.Sockets;
using Common.Network.Packet.Definitions;

namespace Server.Network.Connection
{
    public interface IConnection
    {
        public string Id { get; }

        public delegate void OnReceive(string id, IPacket packet);

        public void Start();

        public void CloseConnection();

        public void Send(IPacket bytes);
    }
}

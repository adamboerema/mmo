using System;
using System.Net.Sockets;
using Common.Network.Packet.Definitions;

namespace Common.Network.Server.Socket
{
    public interface IConnection
    {
        public string Id { get; }

        public void Start();

        public void CloseConnection();

        public void Send(IPacket bytes);
    }
}

using System;
using System.Net.Sockets;

namespace Common.Network.Server.Socket
{
    public interface IConnection
    {
        public string Id { get; }

        public void Start();

        public void CloseConnection();

        public void Send(byte[] bytes);
    }
}

using System;
using System.Net.Sockets;

namespace Common.Network.Server.Socket
{
    public interface IConnection
    {
        string Id { get; }

        TcpClient Socket { get;  }

        void Start();

        void CloseConnection();
    }
}

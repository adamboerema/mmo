using System;
using System.Net.Sockets;
using Common.Network.Server.Socket;

namespace Common.Network.Server.Manager
{
    public interface IConnectionManager
    {
        void AddConnection(IConnection connection);
    }
}

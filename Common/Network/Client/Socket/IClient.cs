using System;
using System.Net.Sockets;

namespace Common.Network.Client.Socket
{
    public interface IClient
    {
        public void Start();

        public void CloseSocket();

    }
}

using System;
using System.Net.Sockets;
using Common.Network.Packet.Definitions;

namespace Common.Network.Client.Socket
{
    public interface IClient
    {
        public void Start();

        public void Send(IPacket packet);

        public void CloseSocket();

    }
}

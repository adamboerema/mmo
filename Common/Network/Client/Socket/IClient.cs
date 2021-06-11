using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common.Network.Packet.Definitions;

namespace Common.Network.Client.Socket
{
    public interface IClient
    {
        public Task Start();

        public void Send(IPacket packet);

        public void CloseSocket();

    }
}

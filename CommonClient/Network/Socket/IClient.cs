using System;
using System.Threading.Tasks;
using Common.Network.Packet.Definitions;

namespace CommonClient.Network.Socket
{
    public interface IClient
    {
        public Task Start(string ipAddress, int port);

        public void Send(IPacket packet);

        public void Close();

    }
}

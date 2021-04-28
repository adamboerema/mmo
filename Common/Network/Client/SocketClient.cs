using System;
using System.Text;

namespace Common.Network.Client
{
    public class SocketClient: SocketConnection
    {
        public SocketClient(string ipAddress, int port)
            : base(ipAddress, port)
        {
        }

        public override void ReceiveDataHandler(StateBuffer state)
        {
            Console.WriteLine(
                "RECEIVED: Endpoint - {0}: Value - {1} ",
                endpointReceive.ToString(),
                Encoding.ASCII.GetString(state.Buffer, 0, stateBuffer.Buffer.Length));
        }
    }
}

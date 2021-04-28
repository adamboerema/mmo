using System;
using System.Text;

namespace Common.Network.Server
{
    public class SocketServer: SocketConnection
    {

        public SocketServer(string ipAddress, int port)
            : base(ipAddress, port)
        {
        }

        /// <summary>
        /// Handles the incoming data from the 
        /// </summary>
        /// <param name="result"></param>
        public override void ReceiveDataHandler(StateBuffer state)
        {
            Console.WriteLine(
                "RECEIVED: Endpoint - {0}: Value - {1} ",
                endpointReceive.ToString(),
                Encoding.ASCII.GetString(state.Buffer, 0, stateBuffer.Buffer.Length));
        }

    }
}

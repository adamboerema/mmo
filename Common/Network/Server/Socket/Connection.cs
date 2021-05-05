using System;
using System.Net;
using System.Net.Sockets;
using Common.Network.Shared;

namespace Common.Network.Server.Socket
{
    public class Connection: IConnection
    {
        public string Id => _id;
        public TcpClient Socket => _socket;

        private string _id;
        private TcpClient _socket;
        private StateBuffer stateBuffer;
        private NetworkStream stream;
        private byte[] readBuffer;

        public Connection(string id, TcpClient socket)
        {
            _id = id;
            _socket = socket;
            _socket.NoDelay = true;
            readBuffer = new byte[Constants.BUFFER_CLIENT_SIZE];
            socket.SendBufferSize = Constants.BUFFER_CLIENT_SIZE;
            socket.ReceiveBufferSize = Constants.BUFFER_CLIENT_SIZE;
            stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);
        }

        public void Start()
        {
            stream = _socket.GetStream();
            BeginStreamRead(stateBuffer);
        }

        public void CloseConnection()
        {
            _socket.Close();
        }

        /// <summary>
        /// Handle the receive data callback
        /// </summary>
        /// <param name="result"></param>
        private void HandleDataReceive(IAsyncResult result)
        {
            var offset = 0;
            var readByteCount = stream.EndRead(result);
            if (readByteCount <= 0)
            {
                CloseConnection();
            }

            var packetBytes = new byte[readByteCount];
            Buffer.BlockCopy(
                readBuffer,
                offset,
                packetBytes,
                offset,
                readByteCount);

            BeginStreamRead(result.AsyncState as StateBuffer);
        }

        /// <summary>
        /// Begin a stream read with state object
        /// </summary>
        /// <param name="state">State object</param>
        private void BeginStreamRead(StateBuffer state)
        {
            var offset = 0;
            stream.BeginRead(
                readBuffer,
                offset,
                Constants.BUFFER_CLIENT_SIZE,
                new AsyncCallback(HandleDataReceive),
                state);
        }
    }
}

﻿using System;
using System.Net.Sockets;
using Common.Network;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Manager;
using Common.Network.Shared;
using Server.Bus.Packet;

namespace Server.Network.Connection
{
    public class TcpSocketConnection: IConnection
    {
        public string Id => id;

        private string id;
        private readonly TcpClient _socket;
        private readonly StateBuffer _stateBuffer;
        private readonly IPacketManager _packetManager;
        private readonly IReceiverPacketBus _receiverPacketBus;
        private NetworkStream _stream;
        private byte[] _readBuffer;

        public TcpSocketConnection(
            string connectionId,
            TcpClient connectionSocket,
            IReceiverPacketBus receiverPacketBus)
        {
            id = connectionId;
            _socket = connectionSocket;
            _socket.NoDelay = true;
            _socket.SendBufferSize = Constants.BUFFER_CLIENT_SIZE;
            _socket.ReceiveBufferSize = Constants.BUFFER_CLIENT_SIZE;
            _readBuffer = new byte[Constants.BUFFER_CLIENT_SIZE];
            _stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);

            var serverDefinitions = new Definitions();
            _packetManager = new PacketManager(serverDefinitions);
            _receiverPacketBus = receiverPacketBus;
        }

        public void Start()
        {
            _stream = _socket.GetStream();
            BeginStreamRead(_stateBuffer);
        }

        public void CloseConnection()
        {
            _socket.Close();
        }

        public void Send(IPacketEvent packet)
        {
            var writeBytes = _packetManager.Write(packet);
            _stream.Write(writeBytes, 0, writeBytes.Length);
        }

        /// <summary>
        /// Handle the receive data callback
        /// </summary>
        /// <param name="result"></param>
        private void HandleDataReceive(IAsyncResult result)
        {
            var offset = 0;
            var readByteCount = _stream.EndRead(result);
            if (readByteCount <= 0)
            {
                CloseConnection();
            }

            var packetBytes = new byte[readByteCount];
            Buffer.BlockCopy(
                _readBuffer,
                offset,
                packetBytes,
                offset,
                readByteCount);

            var packet = _packetManager.Receive(packetBytes);
            _receiverPacketBus.Publish(Id, packet);
            BeginStreamRead(result.AsyncState as StateBuffer);
        }

        /// <summary>
        /// Begin a stream read with state object
        /// </summary>
        /// <param name="state">State object</param>
        private void BeginStreamRead(StateBuffer state)
        {
            var offset = 0;
            _stream.BeginRead(
                _readBuffer,
                offset,
                Constants.BUFFER_CLIENT_SIZE,
                new AsyncCallback(HandleDataReceive),
                state);
        }
    }
}
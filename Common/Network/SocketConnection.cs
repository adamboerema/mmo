﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Common.Network
{
    public abstract class SocketConnection: ISocketConnection
    {
        protected Socket socket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Dgram,
            ProtocolType.Udp);

        protected StateBuffer stateBuffer = new StateBuffer(8 * 1024);
        protected EndPoint endpointReceive;
        protected EndPoint endpointConnect;

        public SocketConnection(string ipAddress, int port)
        {
            var connectIpAddress = IPAddress.Parse(ipAddress);
            endpointReceive = new IPEndPoint(connectIpAddress, port);
            endpointConnect = new IPEndPoint(connectIpAddress, port);
        }

        public abstract void ReceiveDataHandler(StateBuffer state);

        public void Connect()
        {
            Console.WriteLine($"Starting Client");
            socket.Connect(endpointConnect);
            Receive(stateBuffer);
        }

        public void Listen()
        {
            Console.WriteLine($"Starting Server");
            var state = new StateBuffer(8 * 1024);
            socket.Bind(endpointReceive);
            Receive(state);
        }

        public void Send(string text)
        {
            var offset = 0;
            byte[] data = Encoding.UTF8.GetBytes(text);

            socket.BeginSend(
                data,
                offset,
                data.Length,
                SocketFlags.None,
                new AsyncCallback(SendDataCallback),
                stateBuffer);
        }

        public void Receive(StateBuffer stateBuffer)
        {
            var offset = 0;

            socket.BeginReceiveFrom(
                stateBuffer.Buffer,
                offset,
                stateBuffer.Buffer.Length,
                SocketFlags.None,
                ref endpointReceive,
                new AsyncCallback(ReceiveDataCallback),
                stateBuffer);
        }

        private void SendDataCallback(IAsyncResult result)
        {
            int bytes = socket.EndSend(result);
            Console.WriteLine($"Number of bytes sent: {bytes}");
        }

        private void ReceiveDataCallback(IAsyncResult result)
        {
            var stateBuffer = result.AsyncState as StateBuffer;
            int bytes = socket.EndReceiveFrom(result, ref endpointReceive);
            Console.WriteLine($"Number of bytes received: { bytes }");
            ReceiveDataHandler(stateBuffer);

            //Restart receiving
            Receive(stateBuffer);
        }
    }
}

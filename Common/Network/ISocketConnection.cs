using System;

namespace Common.Network
{
    public interface ISocketConnection
    {
        void Send(string text);

        void Receive(StateBuffer stateBuffer);

        void ReceiveDataHandler(StateBuffer result);
    }
}

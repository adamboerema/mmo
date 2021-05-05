using System;

namespace Common.Network.Shared
{
    public class StateBuffer
    {
        public byte[] Buffer;

        public StateBuffer(int bufferSize)
        {
            Buffer = new byte[bufferSize];
        }
    }
}

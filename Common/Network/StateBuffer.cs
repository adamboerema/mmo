using System;

namespace Common.Network
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

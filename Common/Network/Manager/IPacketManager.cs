using System;
using System.Collections.Generic;
using Common.Definitions;
using Common.Network.Definitions;

namespace Common.Network.Manager
{
    public interface IPacketManager
    {
        public byte[] Write(IPacket packet);

        public IEnumerable<IPacket> Receive(byte[] bytes);
    }
}

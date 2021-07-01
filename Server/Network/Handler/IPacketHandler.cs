using System;
using Common.Bus;
using Common.Network.Definitions;
using Server.Bus.Packet;

namespace Server.Network.Handler
{
    public interface IPacketHandler<T> where T : IPacket
    {
        public void Handle(string connectionId, T packet);
    }
}
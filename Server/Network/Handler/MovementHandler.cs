using System;
using Common.Network.Packets.Movement;

namespace Server.Network.Handler
{
    public class MovementHandler: IPacketHandler<MovementInputPacket>
    {
        public MovementHandler()
        {
        }

        public void Handle(string connectionId, MovementInputPacket packet)
        {
            Console.WriteLine($"Connection id: {connectionId}");
            Console.WriteLine($"Movement Packet { packet.Direction }");
        }
    }
}

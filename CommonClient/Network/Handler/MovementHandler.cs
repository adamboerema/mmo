using System;
using Common.Packets.ServerToClient.Movement;

namespace CommonClient.Network.Handler
{
    public class MovementHandler: IPacketHandler<MovementOutputPacket>
    {
        public MovementHandler()
        {
        }

        public void Handle(MovementOutputPacket packet)
        {
            Console.WriteLine($"Movement Packet: {packet.X} {packet.Y} {packet.Z} {packet.MovementType}");
        }
    }
}

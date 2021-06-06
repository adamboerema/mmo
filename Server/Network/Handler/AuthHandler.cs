using System;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Definitions.Schema.Auth;
using Server.Bus;
using Server.Bus.Packet;

namespace Server.Network.Handler
{
    public class AuthHandler: IServerHandler<LoginRequestPacket>
    {
        private readonly PacketBus packetBus;

        public AuthHandler(PacketBus eventBus)
        {
            packetBus = eventBus;
        }

        public void Handle(string connectionId, LoginRequestPacket packet)
        {
            Console.WriteLine($"Connection id: {connectionId}");
            Console.WriteLine($"Login attempt User: {packet.Username}, Password: {packet.Password}");

            // TODO: Handle login logic
            var response = new LoginResponsePacket
            {
                Success = true,
                UserId = Guid.NewGuid().ToString()
            };
            packetBus.Publish(connectionId, response);
        }

    }
}

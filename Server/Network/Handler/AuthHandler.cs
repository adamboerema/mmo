using System;
using Common.Network.Schema.Auth;
using Server.Bus.Packet;

namespace Server.Network.Handler
{
    public class AuthHandler: IServerHandler<LoginRequestPacket>
    {
        private readonly IDispatchPacketBus _packetBus;

        public AuthHandler(IDispatchPacketBus eventBus)
        {
            _packetBus = eventBus;
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
            _packetBus.Publish(connectionId, response);
        }

    }
}

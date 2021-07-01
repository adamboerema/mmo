using System;
using Common.Bus;
using Common.Network.Packets.Auth;
using Server.Bus.Packet;

namespace Server.Auth
{
    public class AuthManager: IAuthManager
    {
        private IDispatchPacketBus _dispatchPacketBus;

        public AuthManager(IDispatchPacketBus dispatchPacketBus)
        {
            _dispatchPacketBus = dispatchPacketBus;
        }

        public void HandleLoginResponse(string connectionId, LoginRequestPacket packet)
        {
            Console.WriteLine($"Connection id: {connectionId}");
            Console.WriteLine($"Login attempt User: {packet.Username}, Password: {packet.Password}");

            // TODO: Handle login logic
            var response = new LoginResponsePacket
            {
                Success = true,
                UserId = Guid.NewGuid().ToString()
            };
            _dispatchPacketBus.Publish(connectionId, response);
        }

    }
}

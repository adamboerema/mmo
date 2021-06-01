using System;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Definitions.Schema.Auth;

namespace Server.Network.Handler
{
    public class AuthHandler: IServerHandler<LoginRequestPacket>
    {
        public AuthHandler()
        {
        }

        public void Handle(string connectionId, LoginRequestPacket packet)
        {
            Console.WriteLine($"Connection id: {connectionId}");
            Console.WriteLine($"Login attempt User: {packet.Username}, Password: {packet.Password}");
        }

    }
}

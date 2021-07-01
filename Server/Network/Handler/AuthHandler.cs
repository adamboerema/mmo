using System;
using Common.Network.Packets.Auth;
using Server.Auth;

namespace Server.Network.Handler
{
    public class AuthHandler: IPacketHandler<LoginRequestPacket>
    {
        private readonly IAuthManager _authManager;

        public AuthHandler(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        public void Handle(string connectionId, LoginRequestPacket packet)
        {
            _authManager.HandleLoginResponse(connectionId, packet);
        }

    }
}

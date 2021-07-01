using System;
using Common.Packets.ClientToServer.Auth;

namespace Server.Auth
{
    public interface IAuthManager
    {
        public void HandleLoginResponse(string connectionId, LoginRequestPacket packet);
    }
}

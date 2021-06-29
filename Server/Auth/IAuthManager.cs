﻿using System;
using Common.Network.Schema.Auth;

namespace Server.Auth
{
    public interface IAuthManager
    {
        public void HandleLoginResponse(string connectionId, LoginRequestPacket packet);
    }
}

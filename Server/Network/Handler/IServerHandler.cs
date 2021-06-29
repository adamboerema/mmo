﻿using System;
using Common.Network.Definitions;

namespace Server.Network.Handler
{
    public interface IServerHandler<T> where T : IPacket
    {
        public void Handle(string connectionId, T packet);
    }
}
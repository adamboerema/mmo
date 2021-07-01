﻿using System;
using Common.Bus;
using Common.Definitions;

namespace Server.Bus.Packet
{
    public interface IDispatchPacketBus: IEventBus<DispatchPacketEvent>
    {
        /// <summary>
        /// Dispatch a packet with connection id information
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        public void Publish(string connectionId, IPacket packet);

        /// <summary>
        /// Publish all packet
        /// </summary>
        /// <param name="packet"></param>
        public void Publish(IPacket packet);

    }
}

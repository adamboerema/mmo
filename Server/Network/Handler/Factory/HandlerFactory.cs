using System;
using System.Collections.Generic;
using Common.Network.Definitions;

namespace Server.Network.Handler.Factory
{
    public class HandlerFactory: IHandlerFactory
    {
        private readonly Dictionary<Type, object> _handlers;

        public void RegisterHandler<T>(IPacketHandler<T> handler) where T: IPacket
        {
            _handlers[typeof(T)] = handler;
        }
     
        public IPacketHandler<T> GetHandler<T>() where T: IPacket
        {
            return _handlers[typeof(T)] as IPacketHandler<T>;
        }
    }
}

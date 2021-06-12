using System;
using Common.Bus;

namespace Server.Bus.Connection
{
    public interface IConnectionBus: IEventBus<ConnectionEvent>
    {
        /// <summary>
        /// Publish connection state
        /// </summary>
        /// <param name="state"></param>
        public void Publish(string connectionId, ConnectionState state);
    }
}

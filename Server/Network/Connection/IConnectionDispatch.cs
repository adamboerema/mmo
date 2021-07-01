using System;
using Common.Definitions;
using Common.Network.Definitions;

namespace Server.Network.Connection
{
    public interface IConnectionDispatch
    {
        /// <summary>
        /// Dispatch packet to the connection manager
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        public void Dispatch(string connectionId, IPacket packet);

        /// <summary>
        /// Close the Dispatcher
        /// </summary>
        public void Close();
    }
}

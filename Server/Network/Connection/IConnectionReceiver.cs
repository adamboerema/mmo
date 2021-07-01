using System;
using Common.Definitions;

namespace Server.Network.Connection
{
    public interface IConnectionReceiver
    {
        /// <summary>
        /// Re
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        public void Receive(string connectionId, IPacket packet);

        /// <summary>
        /// Close the Receiver
        /// </summary>
        public void Close();
    }
}

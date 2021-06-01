using System;
namespace Server.Configuration
{
    public interface IServerConfiguration
    {
        /// <summary>
        /// Max server connections
        /// </summary>
        public int MaxConnections { get; }

        /// <summary>
        /// Server port to listen
        /// </summary>
        public int Port { get; }
    }
}

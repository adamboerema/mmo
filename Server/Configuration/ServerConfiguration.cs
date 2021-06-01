using System;

namespace Server.Configuration
{
    public class ServerConfiguration : IServerConfiguration
    {
        public int Port => 7777;

        public int MaxConnections => 10;
    }
}

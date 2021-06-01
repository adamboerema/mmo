using System;
using Common.Client.Configuration;

namespace DesktopClient.Configuration
{
    public class ClientConfiguration : IGameConfiguration
    {
        public string IpAddress => "127.0.0.1";

        public int Port => 7777;
    }
}

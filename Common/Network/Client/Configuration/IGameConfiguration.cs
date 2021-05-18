using System;
namespace Common.Network.Client.Configuration
{
    public interface IGameConfiguration
    {
        public string IpAddress { get; }

        public int Port { get;  }

    }
}

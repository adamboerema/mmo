using System;
namespace CommonClient.Configuration
{
    public interface IGameConfiguration
    {
        public string IpAddress { get; }

        public int Port { get;  }

    }
}

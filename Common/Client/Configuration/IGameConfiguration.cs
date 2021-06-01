using System;
namespace Common.Client.Configuration
{
    public interface IGameConfiguration
    {
        public string IpAddress { get; }

        public int Port { get;  }

    }
}

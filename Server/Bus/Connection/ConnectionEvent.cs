using System;
namespace Server.Bus.Connection
{
    public enum ConnectionState
    {
        CONNECT,
        DISCONNECT,
    }

    public class ConnectionEvent
    {
        public string Id { get; set; }

        public ConnectionState State { get; set; }
    }
}

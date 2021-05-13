using System;
namespace Common.Network.Server.Socket
{
    public interface IServer
    {
        public void Start();

        public void CloseConnection();
    }
}

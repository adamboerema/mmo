using System;
namespace Server.Network.Server
{
    public interface IServer
    {
        public void Start();

        public void Close();
    }
}

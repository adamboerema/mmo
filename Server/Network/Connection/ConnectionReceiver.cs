using System;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Definitions.Schema.Auth;
using Server.Network.Handler;

namespace Server.Network.Connection
{
    public class ConnectionReceiver: IConnectionReceiver
    {
        public ConnectionReceiver()
        {
        }

        public void Receive(string connectionId, IPacket packet)
        {
            switch(packet.Id)
            {
                case Definitions.LOGIN_REQUEST:
                    var handler = new AuthHandler();
                    handler.Handle(connectionId, packet as LoginRequestPacket);
                    break;
            }
        }
    }
}

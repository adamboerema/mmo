using System;
using Common.Bus;
using Common.Network.Schema.Auth;
using Server.Bus.Packet;

namespace Server.Auth
{
    public class AuthManager: IAuthManager, IEventBusListener<ReceiverPacketEvent>
    {
        private IReceiverPacketBus _receiverPacketBus;
        private IDispatchPacketBus _dispatchPacketBus;

        public AuthManager(IReceiverPacketBus receiverPacketBus,
            IDispatchPacketBus dispatchPacketBus)
        {
            _dispatchPacketBus = dispatchPacketBus;
            _receiverPacketBus = receiverPacketBus;
            _receiverPacketBus.Subscribe(this);
        }

        public void HandleLoginResponse(string connectionId, LoginRequestPacket packet)
        {
            Console.WriteLine($"Connection id: {connectionId}");
            Console.WriteLine($"Login attempt User: {packet.Username}, Password: {packet.Password}");

            // TODO: Handle login logic
            var response = new LoginResponsePacket
            {
                Success = true,
                UserId = Guid.NewGuid().ToString()
            };
            _dispatchPacketBus.Publish(connectionId, response);
        }

        public void Handle(ReceiverPacketEvent eventObject)
        {
            switch(eventObject.Packet)
            {
                case LoginRequestPacket packet:
                    HandleLoginResponse(eventObject.ConnectionId, packet);
                    break;
            }
        }

    }
}

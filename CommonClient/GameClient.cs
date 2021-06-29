using System;
using System.Threading.Tasks;
using Common.Bus;
using Common.Network.Definitions;
using CommonClient.Bus.Packet;
using CommonClient.Configuration;
using CommonClient.Network.Socket;

namespace CommonClient
{
    public class GameClient: IGameClient
    {
        private readonly IGameConfiguration _configuration;
        private readonly IClient _client;
        private readonly ReceiverPacketBus _receiverPacketBus;
        private readonly DispatchPacketBus _dispatchPacketBus;

        public GameClient(IGameConfiguration gameConfiguration)
        {
            _receiverPacketBus = new ReceiverPacketBus();
            _dispatchPacketBus = new DispatchPacketBus();
            _configuration = gameConfiguration;
            _client = new TcpSocketClient(_receiverPacketBus, _dispatchPacketBus);
        }

        public void Start()
        {
            initializeConnection().Wait();
        }

        public void Close()
        {
            _client.Close();
        }

        public void Send(IPacket packet)
        {
            _dispatchPacketBus.Publish(packet);
        }

        public void RegisterListener(IEventBusListener<PacketEvent> eventListener)
        {
            _receiverPacketBus.Subscribe(eventListener);
        }

        public void UnregisterListener(IEventBusListener<PacketEvent> eventListener)
        {
            _receiverPacketBus.Unsubscribe(eventListener);
        }

        private async Task initializeConnection()
        {
            try
            {
                await _client.Start(_configuration.IpAddress, _configuration.Port);
            }
            catch(Exception error)
            {
                var delayMs = 5000;
                Console.WriteLine($"Failed connection with error: {error.Message}");
                Console.WriteLine("Retrying Connection attempt in 5 seconds");
                await Task.Delay(delayMs).ContinueWith(_ => initializeConnection());
            }
        }
    }
}

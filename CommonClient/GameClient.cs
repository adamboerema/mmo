using System;
using System.Threading.Tasks;
using Common.Client.Configuration;
using Common.Network.Client;
using Common.Network.Packet.Definitions.Schema.Auth;

namespace Common.Client
{
    public class GameClient
    {
        private readonly IGameConfiguration _configuration;
        private readonly TcpSocketClient _client;

        public GameClient(IGameConfiguration gameConfiguration)
        {
            _configuration = gameConfiguration;
            _client = new TcpSocketClient(gameConfiguration.IpAddress, gameConfiguration.Port);
        }

        public void Start()
        {
            InitializeConnection().Wait();
        }

        public async Task InitializeConnection()
        {
            try
            {
                await _client.Start();
            }
            catch(Exception error)
            {
                var delayMs = 5000;
                Console.WriteLine($"Failed connection with error: {error.Message}");
                Console.WriteLine("Retrying Connection attempt in 5 seconds");
                await Task.Delay(delayMs).ContinueWith(_ => InitializeConnection());
            }
        }

        public void Login(string username, string password)
        {
            var packet = new LoginRequestPacket
            {
                Username = username,
                Password = password
            };
            _client.Send(packet);
        }
    }
}

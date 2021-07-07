using System;
using System.Threading.Tasks;
using CommonClient.Configuration;
using CommonClient.Network.Receiver;
using CommonClient.Network.Socket;

namespace CommonClient
{
    public class GameClient: IGameClient
    {
        private readonly IGameConfiguration _configuration;
        private readonly IConnectionReceiver _connectionReceiver;
        private readonly IClient _client;

        public GameClient(IGameConfiguration gameConfiguration)
        {
            GameServices.Initialize(gameConfiguration);
            _configuration = gameConfiguration;
            _client = GameServices.GetService<IClient>();
            _connectionReceiver = GameServices.GetService<IConnectionReceiver>();
        }

        public void Start()
        {
            InitializeConnection().Wait();
        }

        public void Close()
        {
            _client.Close();
            _connectionReceiver.Close();
        }

        private async Task InitializeConnection()
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
                await Task.Delay(delayMs).ContinueWith(_ => InitializeConnection());
            }
        }
    }
}

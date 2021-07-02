using System;
using System.Threading.Tasks;
using CommonClient.Configuration;
using CommonClient.Network.Socket;
using Microsoft.Extensions.DependencyInjection;

namespace CommonClient
{
    public class GameClient: IGameClient
    {
        public readonly GameServices GameServices;

        private readonly IGameConfiguration _configuration;
        private readonly IClient _client;

        public GameClient(IGameConfiguration gameConfiguration)
        {
            GameServices = new GameServices(gameConfiguration);
            _configuration = gameConfiguration;
            _client = GameServices.ServiceProvider.GetService<IClient>();
        }

        public void Start()
        {
            InitializeConnection().Wait();
        }

        public void Close()
        {
            _client.Close();
        }

        public T GetService<T>()
        {
            return GameServices.ServiceProvider.GetService<T>();
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

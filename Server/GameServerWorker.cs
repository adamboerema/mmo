using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Sever;

namespace Server
{
    public class GameServerWorker: IHostedService
    {
        private readonly GameServer _server;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public GameServerWorker(
            GameServer server,
            IHostApplicationLifetime applicationLifetime)
        {
            _server = server;
            _applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(OnStarted);
            _applicationLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.StopApplication();

            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _server.Start();
        }

        private void OnStopped()
        {
            _server.Close();
        }
    }
}

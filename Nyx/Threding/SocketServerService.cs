using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Threding
{
    public class SocketServerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5);

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();


        public SocketServerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Security Service started");

            using var timer = new PeriodicTimer(_checkInterval);

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    //await PerformSecurityChecksAsync();
                }
            }
            catch (OperationCanceledException)
            {
                Log.Information("Security Service stopped gracefully");
            }
        }
    }
}

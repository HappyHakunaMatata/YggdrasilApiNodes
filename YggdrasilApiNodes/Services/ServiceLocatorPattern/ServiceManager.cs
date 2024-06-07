using System;
using YggdrasilApiNodes.Interfaces;

namespace YggdrasilApiNodes.Services
{
	public class ServiceManager
	{
		private IClient? linkService = null;
        private IPingService? pingService = null;
        TimerManager Timer;
        private readonly ILogger _logger;

        public ServiceManager()
		{
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            _logger = loggerFactory.CreateLogger<ServiceLocator>();

            Timer = new TimerManager();
        }

        public IClient LinkService
        {
            get
            {
                if (linkService == null)
                {
                    throw new ArgumentNullException();
                }
                return linkService;
            }
            set
            {
                linkService = value;
            }
        }

        public IPingService PingService
        {
            get
            {
                if (pingService == null)
                {
                    throw new ArgumentNullException();
                }
                return pingService;
            }
            set
            {
                pingService = value;
            }
        }

        public void RunLinkService()
        {
            try
            {
                Timer.GetPeersCompleted += OnGetPeersCompleted;
                Timer.PingCompleted += OnPingPeersCompleted;
                Timer.SetTimer();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        public async void OnGetPeersCompleted(object? sender, EventArgs e)
        {
            try
            {
                await LinkService.GetPeers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async void OnPingPeersCompleted(object? sender, EventArgs e)
        {
            try
            {
                await PingService.PingHosts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                Timer.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}


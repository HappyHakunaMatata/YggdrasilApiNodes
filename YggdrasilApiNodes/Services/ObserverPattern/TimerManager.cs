using System;
using System.Timers;

namespace YggdrasilApiNodes.Services
{
	public class TimerManager
	{

        private readonly System.Timers.Timer _timer;

        public event EventHandler? GetPeersCompleted;
        public event EventHandler? PingCompleted;


        public TimerManager()
		{
            try
            {
                _timer = new System.Timers.Timer(300000);
            }
            catch
            {
                throw;
            }
        }

        public void SetTimer()
        {
            ArgumentNullException.ThrowIfNull(_timer);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private bool controller = false;
        protected virtual void OnTimerElapsed(Object? source, ElapsedEventArgs e)
        {
            try
            {
                if (GetPeersCompleted != null && controller == false)
                {
                    GetPeersCompleted.Invoke(this, e);
                    controller = true;
                    return;
                }

                if (PingCompleted != null && controller == true)
                {
                    PingCompleted.Invoke(this, e);
                    controller = false;
                    return;
                }
            }
            catch
            {
                throw;
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
                _timer.Dispose();
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


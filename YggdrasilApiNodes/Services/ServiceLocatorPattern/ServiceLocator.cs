using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YggdrasilApiNodes.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace YggdrasilApiNodes.Services
{
	public class ServiceLocator
	{

		private static ServiceLocator? locator = null;

		private AppDbContext? ctx = null;

        private ServiceLocator()
        {
        }
        

        public static ServiceLocator Instance
		{
			get
			{
				if (locator == null)
				{
					locator = new ServiceLocator();
				}
				return locator;
			}
		}

		public void AddAppDbContext(string connection)
		{
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite(connection);
            ctx = new AppDbContext(optionsBuilder.Options);
        }

		private IClient? linkService = null;

		public IClient GetILinkService()
		{
            ArgumentNullException.ThrowIfNull(ctx);
            try
			{
				if (linkService == null)
				{
					linkService = new Client(ctx);
				}
				return linkService;
			}
			catch
			{
				throw;
			}
		}

        private IPingService? pingService = null;

        public IPingService GetIPingService()
		{
			ArgumentNullException.ThrowIfNull(ctx);
            try
            {
                if (pingService == null)
                {
                    pingService = new PingService(ctx);
                }
                return pingService;
            }
            catch
            {
                throw;
            }
        }

        

    }
}


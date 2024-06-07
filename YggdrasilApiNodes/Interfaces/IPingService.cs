using System;
namespace YggdrasilApiNodes.Interfaces
{
	public interface IPingService
	{
        public Task PingHosts();
        public bool IsIPv6Supported();
    }
}


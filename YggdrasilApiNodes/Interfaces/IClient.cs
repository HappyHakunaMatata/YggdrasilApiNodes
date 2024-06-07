using System;
using System.Timers;
using YggdrasilApiNodes.Services;

namespace YggdrasilApiNodes.Interfaces
{
	public interface IClient
    {
        public Task GetPeers();
        
    }
}


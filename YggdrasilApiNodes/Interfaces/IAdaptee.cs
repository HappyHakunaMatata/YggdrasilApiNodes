using System;
using Microsoft.AspNetCore.Mvc;
using YggdrasilApiNodes.Models;

namespace YggdrasilApiNodes.Interfaces
{
	public interface IAdaptee
	{
        public IActionResult GetCountries(List<Country>? countries);
        public IActionResult GetIpAddresses(List<Peer>? peers);
        public IActionResult GetPeers(List<Peer>? peers);
        public IActionResult GetLocations(List<string?>? locations);
        public IActionResult GetPeer(Peer? peers);
        public IActionResult Exception(ExceptionModel? exception);
    }
}


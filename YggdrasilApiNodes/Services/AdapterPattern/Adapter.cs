using System;
using Microsoft.AspNetCore.Mvc;
using YggdrasilApiNodes.Interfaces;
using YggdrasilApiNodes.Models;

namespace YggdrasilApiNodes.Services.Adapter
{
	public class Adapter
	{
        private readonly IAdaptee _adaptee;

        public Adapter(IAdaptee adaptee)
        {
            this._adaptee = adaptee;
        }

        public IActionResult GetCountries(List<Country>? countries)
        {
            try
            {
                return _adaptee.GetCountries(countries);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult GetIpAddresses(List<Peer>? peers)
        {
            try
            {
                return _adaptee.GetIpAddresses(peers);
            }
            catch
            {
                throw;
            }
        }


        public IActionResult GetPeers(List<Peer>? peers)
        {
            try
            {
                return _adaptee.GetPeers(peers);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult GetPeer(Peer? peers)
        {
            try
            {
                return _adaptee.GetPeer(peers);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult GetLocations(List<string?>? locations)
        {
            try
            {
                return _adaptee.GetLocations(locations);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult Exception(ExceptionModel? exception)
        {
            try
            {
                return _adaptee.Exception(exception);
            }
            catch
            {
                throw;
            }
        }
    }
}


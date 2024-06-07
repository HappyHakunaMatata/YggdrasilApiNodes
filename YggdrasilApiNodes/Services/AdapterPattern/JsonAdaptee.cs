using System;
using Microsoft.AspNetCore.Mvc;
using YggdrasilApiNodes.Interfaces;
using YggdrasilApiNodes.Models;
using YggdrasilApiNodes.Services.Adapter;

namespace YggdrasilApiNodes.Services
{
    public class JsonAdaptee : IAdaptee
    {
        public IActionResult GetCountries(List<Country>? countries)
        {
            try
            {
                if (countries == null)
                {
                    var error = new JsonResult(new List<string>());
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var result = countries.Select(country =>
                {
                    if (!string.IsNullOrEmpty(country.Name))
                    {
                        return new Dictionary<string, object>
                        {
                        { "Id", (int)country.Id },
                        { "Name", country.Name }
                        };
                    }
                    return null;
                }).Where(n => n != null).ToList();
                if (result == null || result.Count == 0)
                {
                    var error = new JsonResult(new List<string>());
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var json = new JsonResult(result);
                json.StatusCode = 200;
                json.ContentType = "application/json";
                return json;
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
                if (locations == null)
                {
                    var error = new JsonResult(new List<string>());
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var result = locations.Select((location, id) =>
                {
                    if (!string.IsNullOrEmpty(location))
                    {
                        return new Dictionary<string, object>
                        {
                            { "Id", id },
                            { "location", location },
                        };
                    }
                    return null;
                }).Where(n => n != null).ToList();
                if (result == null || result.Count == 0)
                {
                    var error = new JsonResult(new List<string>());
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var json = new JsonResult(result);
                json.StatusCode = 200;
                json.ContentType = "application/json";
                return json;
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
                if (peers == null)
                {
                    var error = new JsonResult(new List<string>());
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var result = peers.Select(peer =>
                {
                    if (peer.ipAddresses != null && peer.ipAddresses.Count > 0 && !string.IsNullOrEmpty(peer.hostAddress))
                    {
                        return new Dictionary<string, object?>
                        {
                        { "hostAddress", peer.hostAddress },
                        { "Domain", peer.DomainName },
                        { "IPs", peer.IPs }
                        };
                    }
                    return null;
                }).Where(n => n != null).ToList();
                if (result == null || result.Count == 0)
                {
                    var error = new JsonResult(new List<string>());
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var json = new JsonResult(result);
                json.StatusCode = 200;
                json.ContentType = "application/json";
                return json;
            }
            catch
            {
                throw;
            }
        }

        public IActionResult GetPeer(Peer? peer)
        {
            try
            {
                if (peer == null || string.IsNullOrEmpty(peer.hostAddress))
                {
                    var error = new JsonResult(null);
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }

                var result = new Dictionary<string, object?>();
                result.Add("Id", (int)peer.Id);
                result.Add("hostAddress", peer.hostAddress);
                result.Add("LastOnline", peer.LastOnline);
                result.Add("IPs", peer.ipAddresses?.Select(n => n.ipAddress?.ToString()).Where(n => !string.IsNullOrEmpty(n?.ToString())).ToList());
                var location = new Dictionary<string, object?>();
                location.Add("Location", peer.location?.location);
                location.Add("Country", peer.location?.country?.Name);
                location.Add("Region", peer.location?.country?.region);
                result.Add("Location", location);
                result.Add("Domain", peer.DomainName);
                result.Add("info", peer.info);
                result.Add("Online", peer.GetOnline() ? "online" : "offline");
                result.Add("Uptime", peer.GetUptime());
                if (result == null)
                {
                    var error = new JsonResult(null);
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var json = new JsonResult(result);
                json.StatusCode = 200;
                json.ContentType = "application/json";
                return json;
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
                if (peers == null)
                {
                    var error = new JsonResult(new List<Peer>());
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var result = peers.Select(peer =>
                {
                    var result = new Dictionary<string, object?>();
                    if (!string.IsNullOrEmpty(peer.hostAddress))
                    {
                        result.Add("Id", (int)peer.Id);
                        result.Add("hostAddress", peer.hostAddress);
                    }
                    else
                    {
                        return null;
                    }
                    result.Add("LastOnline", peer.LastOnline);
                    result.Add("IPs", peer.ipAddresses?.Select(n => n.ipAddress?.ToString()).Where(n => !string.IsNullOrEmpty(n?.ToString())).ToList());
                    var location = new Dictionary<string, object?>();
                    location.Add("Location", peer.location?.location);
                    location.Add("Country", peer.location?.country?.Name);
                    location.Add("Region", peer.location?.country?.region);
                    result.Add("Location", location);
                    result.Add("Domain", peer.DomainName);
                    result.Add("info", peer.info);
                    result.Add("Online", peer.GetOnline() ? "online" : "offline");
                    result.Add("Uptime", peer.GetUptime());
                    return result;
                }).Where(n => n != null).ToList();

                if (result == null || result.Count == 0)
                {
                    var error = new JsonResult(new List<Peer>());
                    error.StatusCode = 200;
                    error.ContentType = "application/json";
                    return error;
                }
                var json = new JsonResult(result);
                json.StatusCode = 200;
                json.ContentType = "application/json";
                return json;
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
                if (exception == null)
                {
                    var error = new JsonResult(null);
                    error.StatusCode = 400;
                    error.ContentType = "application/json";
                    return error;
                }
                var result = new Dictionary<string, object?>();
                result.Add("Location", exception.Location);
                result.Add("Domain", (int)exception.Domain);
                result.Add("Info", exception.Info);
                result.Add("Online", exception.Online);
                var json = new JsonResult(result);
                json.StatusCode = 400;
                json.ContentType = "application/json";
                return json;
            }
            catch
            {
                throw;
            }
        }
    }
}


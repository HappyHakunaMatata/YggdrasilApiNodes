using System;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using YggdrasilApiNodes.Interfaces;
using YggdrasilApiNodes.Models;

namespace YggdrasilApiNodes.Services
{
	public class XMLAdaptee: IAdaptee
	{
        public IActionResult GetCountries(List<Country>? countries)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Country>));
                using (StringWriter stringWriter = new StringWriter())
                {
                    if (countries == null)
                    {
                        serializer.Serialize(stringWriter, countries);
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    var obj = countries.Where(n => !string.IsNullOrEmpty(n.Name)).Select(n => new Country() { _ID = n.Id.ToString(), Name = n.Name }).ToList();
                    if (obj == null || obj.Count == 0)
                    {
                        serializer.Serialize(stringWriter, new List<Country>());
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    serializer.Serialize(stringWriter, obj);
                    return new ContentResult
                    {
                        ContentType = "application/xml",
                        Content = stringWriter.ToString(),
                        StatusCode = 200
                    };
                }
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Location>));
                using (StringWriter stringWriter = new StringWriter())
                {
                    if (locations == null)
                    {
                        serializer.Serialize(stringWriter, locations);
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    var obj = locations.Where(n => !string.IsNullOrEmpty(n)).Select((n, index) => new Location() { _ID = index.ToString(), location = n }).ToList();
                    if (obj == null || obj.Count == 0)
                    {
                        serializer.Serialize(stringWriter, new List<Location>());
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    serializer.Serialize(stringWriter, obj);
                    return new ContentResult
                    {
                        ContentType = "application/xml",
                        Content = stringWriter.ToString(),
                        StatusCode = 200
                    };
                }
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Peer>));
                using (StringWriter stringWriter = new StringWriter())
                {
                    if (peers == null)
                    {
                        serializer.Serialize(stringWriter, peers);
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    var obj = peers.Where(n => !string.IsNullOrEmpty(n.hostAddress) && n.IPs != null && n.IPs.Count != 0).
                        Select(n => new Peer()
                        {
                            hostAddress = n.hostAddress,
                            DomainName = n.DomainName,
                            ipAddresses = n.ipAddresses,
                        }).ToList();
                    if (obj == null || obj.Count == 0)
                    {
                        serializer.Serialize(stringWriter, new List<Peer>());
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    serializer.Serialize(stringWriter, obj);
                    return new ContentResult
                    {
                        ContentType = "application/xml",
                        Content = stringWriter.ToString(),
                        StatusCode = 200
                    };
                }
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
                XmlSerializer serializer = new XmlSerializer(typeof(Peer));
                using (StringWriter stringWriter = new StringWriter())
                {
                    if (peer == null || string.IsNullOrEmpty(peer.hostAddress))
                    {
                        serializer.Serialize(stringWriter, null);
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    if (peer.location != null)
                    {
                        peer.location.country = new Country()
                        {
                            region = peer.location?.country?.region,
                            Name = peer.location?.country?.Name,
                        };
                        peer._Online = peer.GetOnline() ? "online" : "offline";
                        peer._Uptime = peer.GetUptime();
                        peer._lastonline = peer.LastOnline.ToString();
                        peer._ID = peer.Id.ToString();
                    }
                    serializer.Serialize(stringWriter, peer);
                    return new ContentResult
                    {
                        ContentType = "application/xml",
                        Content = stringWriter.ToString(),
                        StatusCode = 200
                    };
                }
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Peer>));
                using (StringWriter stringWriter = new StringWriter())
                {
                    if (peers == null)
                    {
                        serializer.Serialize(stringWriter, peers);
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    var obj = peers.Where(n => !string.IsNullOrEmpty(n.hostAddress)).Select(n =>
                    {
                        if (n.location != null)
                        {
                            n.location.country = new Country()
                            {
                                region = n.location?.country?.region,
                                Name = n.location?.country?.Name,
                            };
                            n._lastonline = n.LastOnline.ToString();
                            n._Online = n.GetOnline() ? "online" : "offline";
                            n._Uptime = n.GetUptime();
                            n._ID = n.Id.ToString();
                        }
                        return n;
                    }).ToList();
                    if (obj == null || obj.Count == 0)
                    {
                        serializer.Serialize(stringWriter, new List<Peer>());
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 200
                        };
                    }
                    serializer.Serialize(stringWriter, obj);
                    return new ContentResult
                    {
                        ContentType = "application/xml",
                        Content = stringWriter.ToString(),
                        StatusCode = 200
                    };
                }
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
                XmlSerializer serializer = new XmlSerializer(typeof(ExceptionModel));
                using (StringWriter stringWriter = new StringWriter())
                {
                    if (exception == null)
                    {
                        serializer.Serialize(stringWriter, null);
                        return new ContentResult
                        {
                            ContentType = "application/xml",
                            Content = stringWriter.ToString(),
                            StatusCode = 400
                        };
                    }
                    serializer.Serialize(stringWriter, exception);
                    return new ContentResult
                    {
                        ContentType = "application/xml",
                        Content = stringWriter.ToString(),
                        StatusCode = 400
                    };
                }
            }
            catch
            {
                throw;
            }
        }
    }
}


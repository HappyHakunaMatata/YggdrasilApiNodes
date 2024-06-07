using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using YggdrasilApiNodes.Interfaces;
using YggdrasilApiNodes.Models;

namespace YggdrasilApiNodes.Services
{
	public class PingService: IPingService
	{

        private readonly AppDbContext _context;
        private readonly bool IPv6Supported;

        public PingService(AppDbContext context)
		{
            IPv6Supported = IsIPv6Supported();
            Console.WriteLine($"IPv6 is supported: {IPv6Supported}");
            _context = context;
        }


        public bool IsIPv6Supported()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    var reply = ping.Send("ipv6.google.com");
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (PingException)
            {
                return false;
            }
            catch
            {
                throw;
            }
        }

		public async Task PingHosts()
		{
            var pattern = new Regex("(?:localhost|\\.loki)");
            try
            {
                var peers = _context.peer.Include(p => p.ipAddresses).ToList();
                var peersWithoutlocal = peers.Select(h => h).Where(h => !pattern.Match(h.DomainName).Success).ToList();
                if (peers != null)
                {
                    await PingPeers(peersWithoutlocal);
                    await ClearEmptyPeers();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task ClearEmptyPeers()
        {
            try
            {
                var peersToRemove = _context.ipAdresses.Where(p => p.PeerId == null).ToList();
                _context.ipAdresses.RemoveRange(peersToRemove);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task PingPeers(List<Peer> peers)
        {
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 4
            };
            try
            {
                await Parallel.ForEachAsync(peers, options, async (host, ctx) =>
                {
                    var iPAddress = host.TryGetIP(IPv6Supported);
                    if (iPAddress != null)
                    {
                        try
                        {
                            using (Ping ping = new Ping())
                            {
                                var reply = await ping.SendPingAsync(iPAddress, 500);
                                if (reply.Status == IPStatus.Success)
                                {
                                    host.LastOnline = DateTime.Now;
                                    host.SetIPAdresses();
                                }
                            }
                        }
                        catch (PingException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine(iPAddress);
                        }
                    }
                });
                _context.peer.UpdateRange(peers);
                _context.SaveChanges();
                Peer.PingCounter += 1;
            }
            catch
            {
                throw;
            }
        }
    }
}


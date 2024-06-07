using System;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using YamlDotNet.Serialization;
using YggdrasilApiNodes.Interfaces;
using YggdrasilApiNodes.Models;
using static System.Net.WebRequestMethods;

namespace YggdrasilApiNodes.Services
{
	public class Client : IClient
	{
        private LinkModel? linkModel;
        private readonly AppDbContext _context;


        public Client(AppDbContext context)
		{
            try
            {
                _context = context;
                string contents = System.IO.File.ReadAllText("links.json");
                var deserializer = new DeserializerBuilder().Build();
                linkModel = JsonSerializer.Deserialize<LinkModel>(contents);
            }
            catch
            {
                throw;
            }
        }

       

        public async Task GetPeers()
        {
            ArgumentNullException.ThrowIfNull(linkModel);
            try
            {
                await linkModel.GetGithubPeerLinks();
                var component = linkModel.GetGithubLinks();
                var peers = await component.GetPeers();
                var countries = _context.countries.Select(c => c);
                foreach (var peer in peers)
                {
                    var entity = await _context.peer.Select(p => p).Where(p => p.hostAddress == peer.hostAddress).FirstOrDefaultAsync();
                    if (entity != null)
                    {
                        entity.info = peer.info;
                        if (entity.location != null && entity.location.country != null)
                        {
                            entity.location.location = peer.location?.location;
                            entity.location.country.region = peer.location?.country?.region;
                        }
                        _context.Update(entity);
                    }
                    else
                    {
                        if (peer.location != null && peer.location.country != null)
                        {
                            var country = await countries.Select(n => n).Where(n => n.Name == peer.location.country.Name && n.region == peer.location.country.region).FirstOrDefaultAsync();
                            if (country != null)
                            {
                                peer.location.country = country;
                            }
                        }
                        _context.Add(peer);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }


    }
}


using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using YamlDotNet.Serialization;
using YggdrasilApiNodes.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YggdrasilApiNodes.Services
{
	public class Leath : Component
    {
        
		public Leath(string uri):base(uri)
		{
            
        }


        public List<string> GetAdresses(string html)
        {
            ArgumentNullException.ThrowIfNull(html);
            try
            {
                string pattern = "(?:<code>)(.*?)(?:<\\/code>)|(?:<td id=\"address\">)(.*?)(?:<\\/td>)";
                Regex rg = new Regex(pattern);
                MatchCollection links = rg.Matches(html);
                return links.Select(n => n.Groups[2].Value).ToList();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// This method suitable for https://publicpeers.neilalexander.dev/v0.5 links
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public List<Peer> GetPeers(string html)
        {
            ArgumentNullException.ThrowIfNull(html);
            try
            {

                Regex addressRegex = new Regex("(?:<td id=\"address\">)(.*?)(?:<\\/td>)");
                Regex countryRegex = new Regex("(?:<thead><tr><th id=\"country\">)(.*?)(?:<\\/th>)");
                Regex peerRegex = new Regex("(?:<\\/thead>)((.|\\n)*?)(?:<thead>|<\\/table>)");

                MatchCollection countries = countryRegex.Matches(html);
                MatchCollection peers = peerRegex.Matches(html);
                return peers.SelectMany((n, index) =>
                {
                    var value = addressRegex.Matches(n.Value).Select(n => n.Groups[1].Value);
                    return value.Select(n => 
                    new Peer()
                    {
                        hostAddress = n,
                        location = new Location()
                        {
                            country = new Country()
                            {
                                Name = countries.Select(n => n.Groups[1].Value).ToArray()[index].Replace("-", "").ToLower(),
                            },
                        }
                    });
                }).ToList();
            }
            catch
            {
                throw;
            }
        }

        public override async Task<List<Peer>> GetPeers()
        {
            try
            {
                var html = await GetHTMLContent();
                if (string.IsNullOrEmpty(html))
                {
                    return new List<Peer>();
                }
                return GetPeers(html);
            }
            catch
            {
                throw;
            }
        }

        public override bool IsComposite()
        {
            return false;
        }
    }
}


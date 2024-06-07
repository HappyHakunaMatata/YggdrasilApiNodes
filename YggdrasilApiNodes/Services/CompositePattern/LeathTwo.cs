using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using YggdrasilApiNodes.Models;
using YggdrasilApiNodes.Models.Github;

namespace YggdrasilApiNodes.Services
{
	public class LeathTwo : Component
    {
        public string? location = null;

        public LeathTwo(string uri) : base(uri)
        {

        }

        public string? GetPayload(string html)
        {
            ArgumentNullException.ThrowIfNull(html);
            try
            {
                var jsonMatches = new Regex("(?:<script type=\"application/json\" data-target=\"react-app.embeddedData\">)(.*?)(?:)(<\\/script>)").Matches(html);
                var json = jsonMatches.FirstOrDefault()?.Groups[1].Value;
                return json;
            }
            catch
            {
                throw;
            }
        }

        public Root? GetModel(string html)
        {
            ArgumentNullException.ThrowIfNull(html);
            try
            {
                var jsonMatches = new Regex("(?:<script type=\"application\\/json\" data-target=\".*?\">)(.*?)(?:<\\/script>)").Matches(html);
                var json = jsonMatches.Reverse().FirstOrDefault()?.Groups[1].Value;
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                return JsonSerializer.Deserialize<Root>(json);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// This method suitable for github links
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public List<Peer> GetPeers(string html)
        {
            
            ArgumentNullException.ThrowIfNull(html);
            try
            {
                var jsonMatches = new Regex("(?:<script type=\"application/json\" data-target=\"react-app.embeddedData\">)(.*?)(?:)(<\\/script>)").Matches(html);
                var json = jsonMatches.FirstOrDefault()?.Groups[1].Value;
                if (string.IsNullOrEmpty(json))
                {
                    return new List<Peer>();
                }
                var model = JsonSerializer.Deserialize<Root>(json);
                if (model == null)
                {
                    return new List<Peer>();
                }
                var mdhtml = model.payload?.blob?.richText;
                if (string.IsNullOrEmpty(mdhtml))
                {
                    return new List<Peer>();
                }
                Regex regex = new Regex("(?:<h1 tabindex=\"-1\" class=\"heading-element\" dir=\"auto\">)(.*?)(?: (peers|Peers)<\\/h1>)");
                var countries = regex.Matches(mdhtml);
                regex = new Regex("(?:<h3 tabindex=\"-1\" class=\"heading-element\" dir=\"auto\">)(.*?)(?:<\\/h3>)");
                var region = regex.Matches(mdhtml);
                var content = new Regex("(?:<\\/(div|p)>\\n<ul dir=\"auto\">)((.|\\n)*?)(?:<\\/ul>\\n(<div class=\"markdown-heading\" dir=\"auto\">|<\\/article>))").Matches(mdhtml);
                var result = content.SelectMany((content, index) =>
                {
                    var info = new Regex("(?:<li>)((.|\\n)*?)(?:<\\/ul>)").Matches(content.Value);
                    return info.SelectMany(n =>
                    {
                        var peers = new Regex("(?:<li><code>)((.|\\n)*?)(?:<\\/code>(<\\/li>| <em>))").Matches(n.Value);
                        return peers.Select(peer =>
                        {
                            var information = new Regex("(?:<li>)((.|\\n)*?)(?:<ul dir=\"auto\">)").Matches(n.Value).FirstOrDefault()?.Groups[1].Value;
                            information = information?.Replace("\n", string.Empty);
                            if (!string.IsNullOrEmpty(information))
                            {
                                information = new Regex("((?:<.*?>)|(<\\/.*?>))").Replace(information, string.Empty);
                            }
                            return new Peer()
                            {
                                hostAddress = peer.Groups[1].Value,
                                info = information,
                                location = new Location()
                                {
                                    location = this.location,
                                    country = new Country()
                                    {
                                        Name = countries.FirstOrDefault()?.Groups[1].Value.ToLower(),
                                        region = (region.Count == 0) ? null : region[index]?.Groups[1].Value,
                                    }
                                }
                            };
                        });
                    }).ToList();
                }).ToList();
                return result;
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


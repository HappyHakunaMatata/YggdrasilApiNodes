using System;
using System.Xml.Serialization;
using YggdrasilApiNodes.Models;

namespace YggdrasilApiNodesUnitTest.Model
{
    public class Peer
    {
        public int Id { get; set; }
        public string? hostAddress { get; set; }
        public List<string>? IPs { get; set; }
        public string? info { get; set; }
        public string? Uptime { get; set; }

    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;

namespace YggdrasilApiNodes.Models
{
	public class Peer
	{
        public static int PingCounter = 0;
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlIgnore]
        public int Id { get; set; }


        /// <summary>
        /// This field is used just for xml serialization
        /// We use _ before name to prevent colides in docker
        /// </summary>
        [NotMapped]
        [XmlElement(ElementName = "Id")]
        public string? _ID
        {
            get; set;
        }


        [Required]
        public string? hostAddress { get; set; }

        [XmlIgnore]
        public DateTime LastOnline { get; set; }

        /// <summary>
        /// This field is used just for xml serialization
        /// We use _ before name to prevent colides in docker
        /// </summary>
        [NotMapped]
        [XmlElement(ElementName = "LastOnline")]
        public string? _lastonline
        {
            get; set;
        }

        [XmlIgnore]
        public virtual List<IPAddressEntity>? ipAddresses { get; set; }

        [NotMapped]
        public List<string?>? IPs
        {
            get
            {
                try
                {
                    return ipAddresses?.Select(n => n._IPAddress).Where(n => !string.IsNullOrEmpty(n?.ToString())).ToList();
                }
                catch
                {
                    throw;
                }
            }
            set
            {

            }
        }

        public virtual Location? location { get; set; }

        public string? info { get; set; }


        /// <summary>
        /// This field is used just for xml serialization
        /// We use _ before name to prevent colides in docker
        /// </summary>
        [NotMapped]
        [XmlElement(ElementName = "Uptime")]
        public string? _Uptime
        {
            get; set;
        }

        [NotMapped]
        public string DomainName
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(hostAddress))
                    {
                        return "";
                    }
                    string pattern = "(?:\\/\\/)(.*?)(?::)";
                    if (hostAddress.Contains("["))
                    {
                        pattern = "(?:\\[)(.*?)(?:\\])";
                    }
                    Regex rg = new Regex(pattern);
                    return rg.Matches(hostAddress).First().Groups[1].Value;
                }
                catch
                {
                    throw;
                }
            }
            set
            {

            }
        }


        public void SetIPAdresses()
        {
            try
            {
                var hostAdresses = Dns.GetHostAddresses(DomainName);
                ipAddresses = hostAdresses.Select(n => new IPAddressEntity() { ipAddress = n }).ToList();
            }
            catch
            {
                throw;
            }
        }


        public IPAddress? TryGetIP(bool ipV6Supported)
        {
            try
            {
                var hostAdresses = Dns.GetHostAddresses(DomainName);
                var ipv4 = hostAdresses.Select(n => n).Where(n => n.AddressFamily == AddressFamily.InterNetwork).ToList().FirstOrDefault();
                if (ipV6Supported)
                {
                    var ipv6 = hostAdresses.Select(n => n).Where(n => n.AddressFamily == AddressFamily.InterNetworkV6).ToList().FirstOrDefault();
                    return ipv4 ?? ipv6;
                }
                return ipv4;
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// This field is used just for xml serialization
        /// We use _ before name to prevent colides in docker
        /// </summary>
        [NotMapped]
        [XmlElement(ElementName = "Online")]
        public string? _Online
        {
            get;
            set;
        }


        public bool GetOnline()
        {
            try
            {
                var now = DateTime.Now;
                var minutesDifference = (int)(now - LastOnline).TotalMinutes;
                return minutesDifference <= 30;
            }
            catch
            {
                throw;
            }
        }


        public string GetUptime()
        {
            try
            {
                double PossibleOfflineMinutes = Peer.PingCounter * 10;
                if (PossibleOfflineMinutes == 0 || LastOnline == DateTime.MinValue)
                {
                    return "unidentified";
                }
                double OnlineMinutes = (double)(DateTime.Now - LastOnline).TotalMinutes;
                var status = OnlineMinutes * 100 / PossibleOfflineMinutes;
                switch (status)
                {
                    case (< 20):
                        return "good";
                    case (< 40):
                        return "average";
                    case (< 60):
                        return "bad";
                    case (< 80):
                        return "very bad";
                }
                return "unidentified";
            }
            catch
            {
                throw;
            }
        }



    }
}


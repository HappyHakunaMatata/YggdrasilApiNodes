using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml.Serialization;

namespace YggdrasilApiNodes.Models
{
	public class IPAddressEntity
	{
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlIgnore]
        public int Id { get; set; }

        [Required]
        [XmlIgnore]
        public IPAddress? ipAddress { get; set; }

        [NotMapped]
        [XmlElement(ElementName = "IPAddress")]
        public string? _IPAddress
        {
            get
            {
                try
                {
                    return ipAddress?.ToString();
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

        public int? PeerId { get; set; }
    }
}


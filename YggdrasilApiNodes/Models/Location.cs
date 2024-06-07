using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace YggdrasilApiNodes.Models
{
    public class Location
	{
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlIgnore]
        public int Id { get; set; }

        public string? location { get; set; }

        [Required]
        public Country? country { get; set; }

        [NotMapped]
        public string? _ID { get; set; }
    }
}


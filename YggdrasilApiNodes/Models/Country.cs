using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace YggdrasilApiNodes.Models
{
	public class Country
	{
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlIgnore]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? region { get; set; }

        /// <summary>
        /// This field is used just for xml serialization 
        /// </summary>
        [NotMapped]
        public string? _ID { get; set; }
    }
}


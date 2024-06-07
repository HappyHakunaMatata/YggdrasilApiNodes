using System;
namespace YggdrasilApiNodes.Models
{
	public class ExceptionModel
	{
		public string? Location { get; set; }
        public int Domain { get; set; }
        public string? Info { get; set; }
        public DateTime Online { get; set; }
    }
}


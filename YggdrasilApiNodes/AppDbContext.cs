using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using YggdrasilApiNodes.Controllers;
using YggdrasilApiNodes.Models;

namespace YggdrasilApiNodes
{
	public class AppDbContext : DbContext
    {
        public virtual DbSet<Peer> peer { get; set; }
        public virtual DbSet<Country> countries { get; set; }
        public virtual DbSet<IPAddressEntity> ipAdresses { get; set; }
        public virtual DbSet<Location> locations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            var NotCreated = Database.EnsureCreated();
            if (NotCreated)
            {
                Console.WriteLine("Database is not created");
            }
        }

        /// <summary>
        /// Required for mock testing
        /// </summary>
        public AppDbContext()
        {

        }
    }
}


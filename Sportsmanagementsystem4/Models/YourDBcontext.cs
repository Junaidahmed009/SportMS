using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;



namespace Sportsmanagementsystem4.Models
{
    public class YourDbContext : DbContext
    {
        public YourDbContext() : base("name=SportsManagementDBEntities")
        {
        }

        // DbSet property for Player entity
        public DbSet<Player> Players { get; set; }

        // DbSet property for User entity
        public DbSet<User> Users { get; set; }
        public DbSet<Sport> Sports { get; set; }

        public Playerteaminfo Playerteaminfos { get; set; }

        public Team Teams { get; set; }
        // DbSet property for PlayerentryDTO class
        public DbSet<PlayerentryDTO> PlayerentryDTOs { get; set; }
    }
}

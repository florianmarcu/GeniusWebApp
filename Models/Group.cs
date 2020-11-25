using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GeniusWebApp.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<GeniusUser> GeniusUsers { get; set; }
    }
    public class GroupDbContext : DbContext
    {
        public GroupDbContext() : base("DBConnectionString") { }
        public DbSet<GeniusUser> GeniusUsers { get; set; }
    }
}
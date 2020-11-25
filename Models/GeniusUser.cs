using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GeniusWebApp.Models
{
    /// <summary>
    ///  Model that represents the app's user
    /// </summary>
    public class GeniusUser
    {
        [Key]
        public int GeniusUserId { get; set; }
        [Required]
        [StringLength(26)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(26)]
        public string LastName { get; set; }
        [Required]
        public virtual GeniusUserProfile GeniusUserProfile { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
    public class GeniusUserDbContext : DbContext
    {
        public GeniusUserDbContext() : base("DBConnectionString") { }
        public DbSet<Group> Groups { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeniusWebApp.Models
{
    public class Group
    {

        [Key]
        public int GroupId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [InverseProperty("Groups")]
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<UserProfile> Administrators { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace GeniusWebApp.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        //public int MembersCount { get; set; }

        public virtual ICollection<GeniusUser> GeniusUsers { get; set; }
    }
}
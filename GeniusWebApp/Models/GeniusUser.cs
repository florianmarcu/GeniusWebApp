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
        public int Id { get; set; }
        [Required]
        [StringLength(26)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(26)]
        public string LastName { get; set; }
        [Required]
        public virtual GeniusUserProfile GeniusUserProfile { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<FriendRequest> FriendRequests { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
    
}
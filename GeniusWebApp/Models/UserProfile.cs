using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeniusWebApp.Models
{
    public class UserProfile
    {
        [Key]
        public int GeniusUserProfileId { get; set; }

        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }

        [RegularExpression(@"^(private|public)$", ErrorMessage = "Visibility not allowed")]
        public string Visibility { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<FriendRequest> FriendRequests { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; } // relationship with AspNetUsers table
    }

}
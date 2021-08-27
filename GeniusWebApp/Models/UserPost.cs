using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeniusWebApp.Models
{
    public class UserPost
    {
     
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; } // the user that makes the post
        public virtual ICollection<Comment> Comments { get; set; } // the comments that appear in the post

        public bool IsGroupPost { get; set; }
    }
}
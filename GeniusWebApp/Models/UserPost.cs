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

        public string Image { get; set; } // I guess the path to the image, we'll change it later if it doesn't work

        [Required]
        public virtual UserProfile Profile { get; set; } // the user that makes the post
        public virtual ICollection<Comment> Comments { get; set; } // the comments that appear in the post
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeniusWebApp.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }
        public string Image { get; set; } // same as UserPost
        public int UserPostId { get; set; }
        [Required]
        public virtual UserPost UserPost { get; set; } // the post it appears in
    }
}
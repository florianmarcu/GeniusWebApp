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

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }    // the name of the person that posted the comment
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        public string UserId { get; set; } // the id of the person that posted the comment

        [Required]
        public virtual UserPost Post { get; set; } // the post it appears in
    }
}
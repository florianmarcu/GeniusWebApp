using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeniusWebApp.Models
{
    public class GeniusUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(26)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(26)]
        public string LastName { get; set; }
        [Key]
        public GeniusUserProfile Profile { get; set; }
    }
}
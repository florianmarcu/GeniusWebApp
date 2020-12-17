using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeniusWebApp.Models
{
    public class GeniusUserProfile
    {
        [Key]
        public int GeniusUserProfileId { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }

        [RegularExpression(@"^(private|public)$", ErrorMessage = "Visibility not allowed")]
        public string Visibility { get; set; }

        public int GeniusUserId { get; set; }

        public GeniusUser GeniusUser { get; set; }
    }

}
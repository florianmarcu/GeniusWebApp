using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeniusWebApp.Models
{
    public class AdminMessage
    {
        [Key]
        public int AdminMessageId { get; set; }
        public string message { get; set; }
        public string UserId { get; set; }
    }
}
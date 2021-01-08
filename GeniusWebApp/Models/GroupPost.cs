using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeniusWebApp.Models
{
    public class GroupPost : UserPost
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
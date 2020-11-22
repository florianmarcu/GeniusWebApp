using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeniusWebApp.Models
{
    public class Action
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPrivate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeniusWebApp.Models
{
    public class FriendRequest
    {

        /*
         *  A composed primary key wouldn't work here since the users could send a friend request to one another
        */


        [Key]
        public int Id { get; set; } // implemented the Id so I can avoid a composed primary key

        [Required]
        public string LastName { get; set; } // the last name of the user the friend request was sent to
        [Required]
        public string FirstName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }  // the date the friend request was created
        [MaxLength(1000)]
        public string Message { get; set; }

        // Foreign key
        [Required]
        public virtual GeniusUser User { get; set; } // the user that sent the friend request
    }
}
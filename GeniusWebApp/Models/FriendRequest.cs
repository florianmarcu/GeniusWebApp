using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int SenderUserProfileId { get; set; } // the user to whom the FR belongs to
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; } // The User Profile to whom the Friend Request was sent to

        [Required]
        public DateTime CreateDate { get; set; }  // the date the friend request was created
        [MaxLength(1000)]
        public string Message { get; set; }
        
        public bool? Accepted { get; set; } // whether the FR was accepted or not (null by default)
        public DateTime ReviewDate { get; set; } // when it was either accepted or deleted by the user

        //// Foreign key
        //[Required] 
        //public virtual ICollection<UserProfile> Profile { get; set; } // the user that sent the friend request
    }
}
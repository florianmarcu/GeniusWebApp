using GeniusWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeniusWebApp.Controllers
{
    public class UserProfileController : Controller
    { 
        ApplicationDbContext _db = new ApplicationDbContext();
    
        // GET: GeniusUserProfile
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult FindUserByName(string firstName, string lastName)
        {

            if (firstName == "" || lastName == "")
                return RedirectToAction("Index");
            else
                return RedirectToAction("ShowAll", "UserProfile", new { firstName = firstName, lastName = lastName});
        }


        public ActionResult ShowAll(string firstName, string lastName)
        {
            System.Diagnostics.Debug.WriteLine(firstName);
            System.Diagnostics.Debug.WriteLine(lastName);


            var matchProfiles = from profile in _db.UserProfiles
                                where profile.FirstName.ToLower() == firstName.ToLower() || profile.LastName.ToLower() == lastName.ToLower()
                                select profile;

            System.Diagnostics.Debug.WriteLine(matchProfiles.ToList<UserProfile>().Count);

            return View(matchProfiles.ToList<UserProfile>());
        }
    }
}
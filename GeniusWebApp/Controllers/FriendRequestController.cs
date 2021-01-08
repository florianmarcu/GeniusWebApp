using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
namespace GeniusWebApp.Controllers
{
    public class FriendRequestController : Controller
    {
        ApplicationDbContext _db;

        public FriendRequestController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: FriendRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Accept(int frId, int upId)
        {
            string loggedUserId = User.Identity.GetUserId();
            UserProfile loggedUserProfile = _db.UserProfiles.Where(profile => profile.UserId == loggedUserId).First();
            UserProfile senderUserProfile = _db.UserProfiles.Find(upId);
            FriendRequest _friendRequest = _db.FriendRequests.Find(frId);
            _friendRequest.Accepted = true;
            _friendRequest.ReviewDate = DateTime.Now;
            loggedUserProfile.Friends.Add(senderUserProfile);
            senderUserProfile.Friends.Add(loggedUserProfile);
            _db.SaveChanges();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        public ActionResult Delete(int frId, int upId)
        {
            string loggedUserId = User.Identity.GetUserId();
            UserProfile loggedUserProfile = _db.UserProfiles.Where(profile => profile.UserId == loggedUserId).First();
            UserProfile senderUserProfile = _db.UserProfiles.Find(upId);
            FriendRequest _friendRequest = _db.FriendRequests.Find(frId);
            _friendRequest.Accepted = false;
            _friendRequest.ReviewDate = DateTime.Now;
            _db.SaveChanges();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        // Creates a friend request(It belongs to the 
        public ActionResult Create(int userProfileId)
        {
            var profile = _db.UserProfiles.Find(userProfileId);
            string senderUserId = User.Identity.GetUserId();
            var senderUserProfile = _db.UserProfiles.Where(prf => prf.UserId == senderUserId).First();
            string senderUserProfileId = senderUserProfile.UserId;
            if (!profile.Friends.Contains(senderUserProfile))
            {
                DateTime _now = DateTime.Now;
                FriendRequest fr = new FriendRequest
                {
                    CreateDate = _now,
                    UserProfileId = userProfileId,
                    UserProfile = profile,
                    SenderUserProfileId = _db.UserProfiles.Where(userProfile => userProfile.UserId == senderUserProfileId).First().GeniusUserProfileId,
                    ReviewDate = _now /// ReviewDate set to Now because EF doesn't accept null values
                };
                profile.FriendRequests.Add(fr);
                _db.SaveChanges();
                //return HttpNotFound();
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            ModelState.AddModelError("Error", "Already friends");
            ContentResult contentResult = new ContentResult();
            contentResult.Content = "Already friends";
            return contentResult;
        }
    }
}
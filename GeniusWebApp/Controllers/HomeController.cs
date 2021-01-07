using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
namespace GeniusWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        string currentUserId;
        private UserProfile _loggedUserProfile;
        public HomeController()
        {
            try
            {
                currentUserId = User.Identity.GetUserId();
                _loggedUserProfile = _db.UserProfiles.Where(profile => profile.UserId == currentUserId).First();
            }
            catch(Exception exc)
            {
                return;
            }
            // _userDb = new GeniusUserDbContext();
        }
        protected override void Dispose(bool disposable)
        {
            //_applicationDb.Dispose();
            // _userDb.Dispose();
        }

        public ActionResult Index()
        {
            /// Queryies all groups belonging to the group
            //var groups = from @group in _db.Groups
            //             orderby @group.Name
            //             select @group;
            List<Group> groups = new List<Group>();

            if (User.Identity.IsAuthenticated)
            { 
                if(_loggedUserProfile == null || currentUserId == null)
                {
                    currentUserId = User.Identity.GetUserId();
                    _loggedUserProfile = _db.UserProfiles.Where(profile => profile.UserId == currentUserId).First();
                }
                groups = _loggedUserProfile.Groups.ToList();

                var friends = _loggedUserProfile.Friends.ToList();
                ViewBag.friends = friends;

                var friendRequests = _loggedUserProfile.FriendRequests.Where(fr => fr.Accepted == null);
                if(friendRequests == null || friendRequests.Count() != 0 )
                {
                    var frUserProfiles = friendRequests.Join(
                        _db.UserProfiles,
                        frs => frs.SenderUserProfileId,
                        ups => ups.GeniusUserProfileId,
                        (frs,ups) => new Tuple<FriendRequest,UserProfile>(frs,ups)
                        );
                    ViewBag.friendRequestsUserProfiles = frUserProfiles.ToList();
                }
                ViewBag.friendRequests = friendRequests.ToList();
            }
            return View(groups);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NewGroup()
        {
            Group group = new Group();
            return View(group);
        }
        [HttpPost]
        public ActionResult NewGroup(string name, string description)
        {
            string _currentUserId = User.Identity.GetUserId();
            UserProfile _currentUserProfile = _db.UserProfiles.Where(profile => profile.UserId == _currentUserId).First();
            try
            {
                Group group = new Group
                {
                    Name = name,
                    Description = description
                };
                group.UserProfiles.Add(_currentUserProfile);
                group.Administrators.Add(_currentUserProfile);
                _db.Groups.Add(
                    group
                );
                _db.SaveChanges();
                //return Redirect("Group/Index/"+group.GroupId);
                return Redirect("Index");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Redirect("Index");
            }

        }

        
    }
}
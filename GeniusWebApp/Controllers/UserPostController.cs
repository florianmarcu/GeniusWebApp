using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeniusWebApp.Controllers
{
    public class UserPostController : Controller
    {
        private ApplicationDbContext _db;

        public UserPostController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: UserPost
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateGroupPost(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            GroupPost groupPost = new GroupPost();
            return View(groupPost);
        }
        [HttpPost]
        public ActionResult CreateGroupPost(string Title, string Content)
        {
            string _currentUserId = User.Identity.GetUserId();
            UserProfile _currentUserProfile = _db.UserProfiles.Where(profile => profile.UserId == _currentUserId).First();
            try
            {
                Group group = _db.Groups.Find((int)TempData["GroupId"]);
                GroupPost newPost = new GroupPost
                {
                    Title = Title,
                    Content = Content,
                    Group = group,
                    GroupId = (int)TempData["GroupId"],
                    UserProfileId = _currentUserProfile.GeniusUserProfileId,
                    UserProfile = _currentUserProfile
                };
                group.UserPosts.Add(newPost);
                _db.SaveChanges();
                return RedirectToAction(
                    actionName: "Index", 
                    controllerName: "Group", 
                    routeValues: new { GroupId = (int)TempData["GroupId"] }
                );
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return RedirectToAction(actionName:"New", controllerName:"Group");
        }
    }
}
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        // GET
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(UserPost post)
        {


            var userId = User.Identity.GetUserId();
            var userProfile = (from userprofile in _db.UserProfiles
                               where userprofile.User.Id == userId
                               select userprofile).First<UserProfile>();

            post.UserProfile = userProfile;

            _db.UserPosts.Add(post);
            _db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }
        public ActionResult Show(int UserPostId)
        {
            var posts = _db.UserPosts.Find(UserPostId);


            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var userpost = (from post in _db.UserPosts
                            where post.Id == id
                            select post).First();

            _db.UserPosts.Remove(userpost);

            _db.SaveChanges();

            var userId = User.Identity.GetUserId();

            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            if (adminId == userId)
                return RedirectToAction("IndexAdmin", "Home");

            return RedirectToAction("Index", "Manage");
        }

        [HttpPut]
        public ActionResult Edit(UserPost post)
        {
            var userpost = (from p in _db.UserPosts
                            where p.Id == post.Id
                            select p).First();

            userpost.Title = post.Title;
            userpost.Content = post.Content;

            _db.SaveChanges();

            var userId = User.Identity.GetUserId();

            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            if (adminId == userId)
                return RedirectToAction("IndexAdmin", "Home");

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
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
                UserPost newPost = new UserPost
                {
                    Title = Title,
                    Content = Content,
                    UserProfileId = _currentUserProfile.GeniusUserProfileId,
                    UserProfile = _currentUserProfile,
                    IsGroupPost = true
                };
                group.UserPosts.Add(newPost);
                _db.SaveChanges();
                return RedirectToAction(
                    actionName: "Index",
                    controllerName: "Group",
                    routeValues: new { GroupId = (int)TempData["GroupId"] }
                );
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return RedirectToAction(actionName: "New", controllerName: "Group");
        }
    }
}
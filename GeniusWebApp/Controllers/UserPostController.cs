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
            // check if admin
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            var userId = User.Identity.GetUserId();

            bool isAdmin = (userId == adminId);
            if (userId == adminId)
            {
                string uId = (string)TempData["userId"];

                userId = uId;
            }
            var userProfile = (from userprofile in _db.UserProfiles
                                where userprofile.User.Id == userId
                                select userprofile).First<UserProfile>();

            post.UserProfile = userProfile;

            if(isAdmin)
            {
                post.FirstName = "admin";
                post.LastName = "admin";
            }
            else
            {
                post.FirstName = userProfile.FirstName;
                post.LastName = userProfile.LastName;
            }
            

            _db.UserPosts.Add(post);
            _db.SaveChanges();

            userId = User.Identity.GetUserId();

            if (userId == adminId)
            {
                return RedirectToAction("Show", "UserProfile", new { id = userProfile.GeniusUserProfileId });
            }

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

            if (TempData["GroupId"] != null)
            {
                return RedirectToAction("Index", "Group", new { GroupId = TempData["GroupId"] });
            }

            if (adminId == userId)
            {
                
                if(TempData["UserProfileId"] != null)
                {
                    return RedirectToAction("Show", "UserProfile", new { id = TempData["UserProfileId"] });
                }
                return RedirectToAction("IndexAdmin", "Home");
            }

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

            var groupId = TempData["GroupId"];

            if(groupId != null)
            {
                return RedirectToAction("Index", "Group", new { GroupId = groupId });
            }

            if (adminId == userId)
            {
                var userProfileId = TempData["UserProfileId"];
                if(userProfileId != null)
                {
                    return RedirectToAction("Show", "UserProfile", new { id = userProfileId });
                }
                return RedirectToAction("IndexAdmin", "Home");
            }

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Edit(int id)
        {
            var groupId = TempData["GroupId"];
            var userProfileId = TempData["UserProfileId"];

            TempData.Clear();

            TempData["GroupId"] = groupId;
            TempData["UserProfileId"] = userProfileId;

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
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            string _currentUserId = User.Identity.GetUserId();


            if (adminId == _currentUserId)
            {
                try
                {
                    Group group = _db.Groups.Find((int)TempData["GroupId"]);
                    UserPost newPost = new UserPost
                    {
                        Title = Title,
                        Content = Content,
                        FirstName = "admin",
                        LastName = "admin",
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
                return RedirectToAction("Index", "Group", new { id = (int)TempData["GroupId"] });
            }
            else
            {
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
                        FirstName = _currentUserProfile.FirstName,
                        LastName = _currentUserProfile.LastName,
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
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
        }
    }
}
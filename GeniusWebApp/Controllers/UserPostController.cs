using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GeniusWebApp.Controllers
{
    public class UserPostController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

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

            post.Profile = userProfile;

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
                return RedirectToAction("Index", "Home");

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
            userpost.Image = userpost.Image;

            _db.SaveChanges();

            var userId = User.Identity.GetUserId();

            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            if (adminId == userId)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
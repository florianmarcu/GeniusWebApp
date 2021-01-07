using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;

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
        public ActionResult Delete(int GeniusUserProfileId)
        {
            return View();
        }

        [HttpPut]
        public ActionResult Edit()
        {
            return View();
        }
    }
}
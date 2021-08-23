using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GeniusWebApp.Controllers
{
    public class CommentController : Controller
    {

        ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(string Id)
        {
            var postId = Int32.Parse(Id);

            var comments = (from comment in _db.Comments
                            where comment.Post.Id == postId
                            select comment);

            ViewBag.comments = comments.ToList<Comment>();
            ViewBag.postId = Id;

            var userId = User.Identity.GetUserId();

            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;
            ViewBag.isAdmin = (userId == adminId);

            if (userId != adminId)
            {

                ViewBag.currentUserId = userId;
            }

            return View();
        }
        
        public ActionResult New(int Id)
        {
            ViewBag.postId = Id;
            //var UserPost = (from post in _db.UserPosts
            //                where post.Id == Int32.Parse(Id)
            //                select post).First();

            //var userId = User.Identity.GetUserId();
            //ViewBag.isValidUser = (userId == UserPost.Profile.User.Id);

            //ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            //var adminId = UserManager.FindByEmail("admin@gmail.com").Id;
            //ViewBag.isAdmin = (userId == adminId);

            return View();
        }

        [HttpPost]
        public ActionResult New(string _postId, string Text, string Image)
        {
            System.Diagnostics.Debug.WriteLine(_postId == "");
            int postId = Int32.Parse(_postId);

            var userPost = (from post in _db.UserPosts
                            where post.Id == postId
                            select post).First();

            Comment comment = new Comment();
            comment.Text = Text;
            comment.Post = userPost;

            var userId = User.Identity.GetUserId();
            var userProfile = (from profile in _db.UserProfiles
                               where profile.User.Id == userId
                               select profile).First();

            comment.LastName = userProfile.LastName;
            comment.FirstName = userProfile.FirstName;
            comment.UserId = userId;

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Show", new { Id = Id });
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(Comment comment)
        {
            System.Diagnostics.Debug.WriteLine(comment.Id);
            System.Diagnostics.Debug.WriteLine(comment.Text);

            var db_comm = (from comm in _db.Comments
                           where comm.Id == comment.Id
                           select comm).First();

            db_comm.Text = comment.Text;
            db_comm.UserId = User.Identity.GetUserId();

            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var comment = (from comm in _db.Comments
                           where comm.Id == id
                           select comm).First();

            _db.Comments.Remove(comment);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}
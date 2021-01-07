using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;

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

            return View();
        }

        public ActionResult New(string Id)
        {
            ViewBag.postId = Id;
            return View();
        }

        [HttpPost]
        public ActionResult New(string Id, string Text, string Image)
        {
            int postId = Int32.Parse(Id);

            var userPost = (from post in _db.UserPosts
                            where post.Id == postId
                            select post).First();

            Comment comment = new Comment();
            comment.Text = Text;
            comment.Image = Image;
            comment.Post = userPost;

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("Show", new { Id = Id });
        }
    }
}
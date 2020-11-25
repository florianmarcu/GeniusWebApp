using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;

namespace GeniusWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _applicationDb;
        private GeniusUserDbContext _userDb;

        public HomeController()
        {
            _applicationDb = new ApplicationDbContext();
            _userDb = new GeniusUserDbContext();
        }
        protected override void Dispose(bool disposable)
        {
            _applicationDb.Dispose();
            _userDb.Dispose();
        }

        public ActionResult Index()
        {
            /// Queryies all groups belonging to the student
            var groups = from student in _userDb.Groups
                           orderby student.Name
                           select student;
            return View();
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
    }
}
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
        private ApplicationDbContext appDbContext = new ApplicationDbContext();

        public HomeController()
        {
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
            var groups = from @group in appDbContext.Groups
                         orderby @group.Name
                         select @group;
            return View(groups.ToList());
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
            try
            {
                Group group = new Group
                {
                    Name = name,
                    Description = description
                };
                appDbContext.Groups.Add(
                    group
                );
                appDbContext.SaveChanges();
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
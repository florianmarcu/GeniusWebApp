using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;

namespace GeniusWebApp.Controllers
{
    public class GroupController : Controller
    {
        ApplicationDbContext _db;

        public GroupController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: Group
        public ActionResult Index(int? Id)
        {
            var group = _db.Groups.Find(Id);
            return View(group);
        }
    }
}
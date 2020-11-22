using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeniusWebApp.Controllers
{
    public class ActionController : Controller
    {
        [HttpPost]
        public ActionResult Create()
        {
            return null;
        }
        // GET: Action
        public ActionResult Index()
        {
            return View();
        }
    }
}
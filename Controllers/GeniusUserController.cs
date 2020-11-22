using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using System.Data.Entity;


namespace GeniusWebApp.Controllers
{
    public class GeniusUserController : Controller
    {
        private ApplicationDbContext _context;

        /// <summary>
        ///  Class Constructor that initializes the:
        ///  - DbContext
        /// </summary>
        public GeniusUserController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            return View();
        }

        // GET: User
        public ActionResult Index(int? Id)
        {
            //if (!pageIndex.HasValue)
            //    pageIndex = 1;
            //if (string.IsNullOrWhiteSpace(sortBy))
            //    sortBy = "FirstName";

            var user = _context.GeniusUsers.Include(c => c.Profile).SingleOrDefault(c => c.Id == Id);
            if (!Id.HasValue)
                return View(user);
            return View(user);
        }
        // GET: User/Random
        public ActionResult Random()
        {
            var user = new GeniusUser() { Id = 1 , FirstName = "Florian" , LastName = "Marcu"};
            var actions = new List<Models.Action>
            {
                new Models.Action {Id = 1, Title = "Post"},
                new Models.Action {Id = 2, Title = "FriendRequest"}
            };

            //ActionViewModel viewModel = new ActionViewModel
            //{
            //    User = user,
            //    Actions = actions
            //};
            //ViewData["User"] = user;
            //ViewData["Actions"] = actions;
            return View(user);
        }

        // GET: User/Edit
        //public ActionResult Edit(int id)
        //{
        //    return Content("id= " + id);
        //}
        //[Route("user/dateRegistered/{year}/{month:regex(\\d{4}):range(1,12)}")]
        //public ActionResult ByDateRegistered(int? year, int? month)
        //{
        //    if (!year.HasValue)
        //        year = 2020;
        //    if (!month.HasValue)
        //        month = 1;
        //    return Content(year + "/" + month);
        //}


    }
}
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
        /// <summary>
        /// Database context used for C.R.U.D operation
        /// </summary>
        //private GeniusUserDbContext _db;
        private ApplicationDbContext _applicationDb;

        /// <summary>
        ///  Class Constructor that initializes the:
        ///  - DbContext
        /// </summary>
        public GeniusUserController()
        {
            //_db = new GeniusUserDbContext();
            _applicationDb = new ApplicationDbContext();
        }
        /// <summary>
        /// Overrides default method for disposing the user-specific Database context
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            //_db.Dispose();
            _applicationDb.Dispose();
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

            //var user = _applicationDb.GeniusUsers.Include(c => c.GeniusUserProfile).SingleOrDefault(c => c.GeniusUserId == Id);
            //if (!Id.HasValue)
            //    return View(user);
            //return View(user);
            return View();
        }
        // GET: User/Random
        public ActionResult Random()
        {
            var user = new GeniusUser() { GeniusUserId = 1 , FirstName = "Florian" , LastName = "Marcu"};
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
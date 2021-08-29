using GeniusWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeniusWebApp.Controllers
{
    public class AdminMessagesController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(string message, string userId)
        {
            AdminMessage adminMessage = new AdminMessage();
            adminMessage.message = message;
            adminMessage.UserId = userId;

            _db.AdminMessages.Add(adminMessage);
            _db.SaveChanges();

            ViewBag.firstName = TempData["firstName"].ToString();
            ViewBag.lastName = TempData["lastName"].ToString();

            return RedirectToAction("ShowAll", "UserProfile", new { firstName = TempData["firstName"].ToString(), lastName = TempData["lastName"].ToString() });
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            System.Diagnostics.Debug.WriteLine(Id);

            var adminMessage = _db.AdminMessages.Find(Id);
            _db.AdminMessages.Remove(adminMessage);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
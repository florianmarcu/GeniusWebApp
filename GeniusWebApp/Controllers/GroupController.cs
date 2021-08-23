
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;

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
        public ActionResult Index(int? GroupId)
        {
            //ViewBag.Name = ;
            if (GroupId != null) /// Prints only the desired group in the route
            {
                var group = _db.Groups.Find(GroupId);
                if (group == null)
                    return HttpNotFound();
                return View(group); /// Should contain only one group
            }
            else /// Prints out all groups registered
            {
                var groups = from gr in _db.Groups
                             select gr;
                return View(groups.ToList()); /// Should contain all the groups registered
            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var group = _db.Groups.Find(Id);

            List<string> userProfileIds = new List<string>(group.UserProfiles.Select(profile => profile.UserId));
            //_db.UserProfiles.Select(profile => userProfileIds.Contains(profile.UserId))

            _db.Groups.Remove(group);
            _db.SaveChanges();
            return RedirectToAction("Index", "Group");
        }
        public ActionResult ShowAll()
        {
            string _currentUserId = User.Identity.GetUserId();
            UserProfile _currentUserProfile = _db.UserProfiles.Where(profile => profile.UserId == _currentUserId).First();
            var groupsQuery = _db.Groups.ToList();
            var groups = groupsQuery.Select(
                group => new Tuple<Group, bool>(group, _currentUserProfile.Groups.Contains(group))
            );
            return View(groups.ToList());
        }
        public ActionResult New()
        {
            Group group = new Group();
            return View(group);
        }
        [HttpPost]
        public ActionResult New(string name, string description)
        {
            string _currentUserId = User.Identity.GetUserId();
            UserProfile _currentUserProfile = _db.UserProfiles.Where(profile => profile.UserId == _currentUserId).First();
            try
            {
                Group group = new Group
                {
                    Name = name,
                    Description = description
                };
                group.UserProfiles.Add(_currentUserProfile);
                group.Administrators.Add(_currentUserProfile);
                _db.Groups.Add(
                    group
                );
                _currentUserProfile.Groups.Add(group);
                _db.SaveChanges();
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


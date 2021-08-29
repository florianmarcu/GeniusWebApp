﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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

            List<int> userPostIds = new List<int>(group.UserPosts.Select(post => post.Id));
            List<UserPost> posts = new List<UserPost>(_db.UserPosts.Where(post => userPostIds.Contains(post.Id)));
            foreach(var post in posts)
            {
                _db.UserPosts.Remove(post);
            }

            _db.Groups.Remove(group);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
        public ActionResult New(Group group)
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;
            string _currentUserId = User.Identity.GetUserId();

            //if(adminId == _currentUserId)
            //{
            //    Group group
            //}
            System.Diagnostics.Debug.WriteLine("Im here10!!!!");

            try
            {
                if (ModelState.IsValid)
                {
                    UserProfile _currentUserProfile = _db.UserProfiles.Where(profile => profile.UserId == _currentUserId).First();
                    group.UserProfiles.Add(_currentUserProfile);
                    group.AdministratorId = _currentUserProfile.UserId;
                    _db.Groups.Add(
                        group
                    );
                    _currentUserProfile.Groups.Add(group);
                    _db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Im here3!!!!");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Im here4!!!!");
                    return View("New");
                }
                System.Diagnostics.Debug.WriteLine("Im here!!!!");
                int id = _db.Groups.OrderByDescending(g => g.GroupId).First().GroupId;
                //return Redirect("Group/Index/"+group.GroupId);
                return RedirectToAction("Index", "Group", new { GroupId = id });
            }
            catch (Exception exception)
            {

                System.Diagnostics.Debug.WriteLine("Im here2!!!!");
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return Redirect("Index");
            }

        }
        
        public ActionResult Edit(int id)
        {
            ViewBag.groupId = id;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(Group g)
        {
            if (ModelState.IsValid)
            {
                Group group = _db.Groups.Find(g.GroupId);
                group.Name = g.Name;
                group.Description = g.Description;
                _db.SaveChanges();
            }
            else
            {
                ViewBag.groupId = g.GroupId;
                return View("Edit");
            }
            return RedirectToAction("Show", "Group", new { id = g.GroupId });
        }
    }
}


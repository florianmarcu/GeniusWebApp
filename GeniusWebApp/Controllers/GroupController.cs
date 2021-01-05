﻿
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
        public ActionResult Index(string groupName)
        {
            ViewBag.Name = groupName;
            if (groupName != null) /// Prints only the desired group in the route
            {
                var group = _db.Groups.Where(gr => gr.Name.ToLower() == groupName.ToLower());
                if (group == null || group.Count() == 0)
                    return HttpNotFound();
                if (group.Count() == 1)
                {
                    ViewBag.single = true;
                }
                else
                {
                    ViewBag.single = false;
                }
                return View(group.ToList()); /// Should contain only one group
            }
            else /// Prints out all groups registered
            {
                var groups = from gr in _db.Groups
                             select gr;
                return View(groups.ToList()); /// Should contain all the groups registered
            }
        }
        public ActionResult Delete(int id)
        {
            var group = _db.Groups.Find(id);
            _db.Groups.Remove(group);
            _db.SaveChanges();
            return RedirectToAction("Index", "Group");
        }
    }
}


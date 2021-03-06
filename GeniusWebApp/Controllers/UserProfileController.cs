﻿using GeniusWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data;


namespace GeniusWebApp.Controllers
{
    public class UserProfileController : Controller
    { 
        ApplicationDbContext _db = new ApplicationDbContext();

        // GET: GeniusUserProfile
        public ActionResult Index(int UserProfileId)
        {
            UserProfile profile = _db.UserProfiles.Find(UserProfileId);
            return View(profile);
        }


        public ActionResult FindUserByName(string firstName, string lastName)
        {

            if (firstName == "" || lastName == "")
                return RedirectToAction("Index","Home");
            else
                return RedirectToAction("ShowAll", "UserProfile", new { firstName = firstName, lastName = lastName});
        }


        public ActionResult ShowAll(string firstName, string lastName)
        {
            var matchProfiles = from profile in _db.UserProfiles
                                where profile.FirstName.ToLower() == firstName.ToLower() || profile.LastName.ToLower() == lastName.ToLower()
                                select profile;

            var userId = User.Identity.GetUserId();

            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;
            ViewBag.isAdmin = (userId == adminId);

            if (userId != adminId)
            {
                var profileId = (from profile in _db.UserProfiles
                                 where profile.User.Id == userId
                                 select profile).FirstOrDefault().GeniusUserProfileId;
                ViewBag.validProfile = profileId;
            }
            
            

            return View(matchProfiles.ToList<UserProfile>());
        }
        public ActionResult UserNotFound(string firstName, string lastName)
        {
            List<string> model = new List<string>();
            model.Add(firstName);
            model.Add(lastName);
            return View(model);
        }
        public ActionResult UpdateProfileImage(string ProfileImage)
        {
            //if(ProfileImage )
           //string ProfileImage = TempData["ProfileImage"].ToString();
           if(ProfileImage == "") // if unsuccesful, do nothing
            {
                return RedirectToAction("Index","Manage");
            }
           else // if succesful, return to Index
            {
                var userId = User.Identity.GetUserId();
                var userProfile = _db.UserProfiles.Where(user => user.UserId == userId).Single();
                userProfile.ProfileImage = ProfileImage;
                if(TryUpdateModel(userProfile,"",new string[] { "ProfileImage" }))
                {
                    try
                    {
                        _db.SaveChanges();
                    }
                    catch(DataException exc)
                    {
                        return RedirectToAction("Index", "Manage");
                    }
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Manage");
            }
        }

        public ActionResult Show(int id)
        {
            var userprofile = (from profile in _db.UserProfiles
                               where profile.GeniusUserProfileId == id
                               select profile).First();
            ViewBag.UserProfile = userprofile;

            var userId = User.Identity.GetUserId();

            ViewBag.isValidUser = (userprofile.User.Id == userId);

            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();

            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;
            ViewBag.isAdmin = (userId == adminId);

            return View();
        }

        public ActionResult JoinGroup(int GroupId)
        {
            string _currentUserId = User.Identity.GetUserId();
            UserProfile _currentUserProfile = _db.UserProfiles.Where(profile => profile.UserId == _currentUserId).First();
            Group group = _db.Groups.Find(GroupId);
            group.UserProfiles.Add(_currentUserProfile);
            _currentUserProfile.Groups.Add(group);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LeaveGroup(int GroupId)
        {
            string _currentUserId = User.Identity.GetUserId();
            UserProfile _currentUserProfile = _db.UserProfiles.Where(profile => profile.UserId == _currentUserId).First();
            Group group = _db.Groups.Find(GroupId);
            group.UserProfiles.Remove(_currentUserProfile);
            _currentUserProfile.Groups.Remove(group);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
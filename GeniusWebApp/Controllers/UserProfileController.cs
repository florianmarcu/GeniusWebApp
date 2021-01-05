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
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult FindUserByName(string firstName, string lastName)
        {

            if (firstName == "" || lastName == "")
                return RedirectToAction("UserNotFound","UserProfile", new { firstName = firstName, lastName = lastName });
            else
                return RedirectToAction("ShowAll", "UserProfile", new { firstName = firstName, lastName = lastName});
        }

        public ActionResult UserNotFound(string firstName, string lastName)
        {
            List<string> model = new List<string>();
            model.Add(firstName);
            model.Add(lastName);
            return View(model);
        }

        public ActionResult ShowAll(string firstName, string lastName)
        {
            System.Diagnostics.Debug.WriteLine(firstName);
            System.Diagnostics.Debug.WriteLine(lastName);


            var matchProfiles = from profile in _db.UserProfiles
                                where profile.FirstName.ToLower() == firstName.ToLower() || profile.LastName.ToLower() == lastName.ToLower()
                                select profile;

            System.Diagnostics.Debug.WriteLine(matchProfiles.ToList<UserProfile>().Count);

            return View(matchProfiles.ToList<UserProfile>());
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
    }
}
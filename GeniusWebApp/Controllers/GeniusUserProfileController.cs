﻿using GeniusWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeniusWebApp.Controllers
{
    public class GeniusUserProfileController : Controller
    { 
        ApplicationDbContext _db = new ApplicationDbContext();
    
        // GET: GeniusUserProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowAll(string Input)
        {
            String delimiters = " ,./<>?;':[]{}_=+`~\t1234567890!@#$%^&*()|";

            var splitString = Input.Split(delimiters.ToCharArray());

            if (splitString.Length > 2)
            {
                // some error
            }

            if (splitString.Length == 1)
            {
                var name = splitString[0].ToLower();

                var matchProfiles = from profile in _db.GeniusUserProfiles
                                    where profile.GeniusUser.LastName.ToLower() == name || profile.GeniusUser.FirstName.ToLower() == name
                                    select profile;

                return View(matchProfiles.ToList<GeniusUserProfile>());
            }
            else
            {

            }

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Globalization;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace GeniusWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult IndexAdmin()
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            string currentUserId = User.Identity.GetUserId();

            if (currentUserId == adminId)
                return View();
            else return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            /// Queryies all groups belonging to the group
            //var groups = from @group in _db.Groups
            //             orderby @group.Name
            //             select @group;
            List<Group> groups = new List<Group>();

            if (User.Identity.IsAuthenticated)
            {
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

                string currentUserId = User.Identity.GetUserId();

                if(currentUserId == adminId)
                {
                    return RedirectToAction("IndexAdmin");
                }

                UserProfile _loggedUserProfile = _db.UserProfiles.Where(profile => profile.UserId == currentUserId).First();
                if (_loggedUserProfile == null || currentUserId == null)
                {
                    currentUserId = User.Identity.GetUserId();
                    _loggedUserProfile = _db.UserProfiles.Where(profile => profile.UserId == currentUserId).First();
                }
                groups = _loggedUserProfile.Groups.ToList();

                var friends = _loggedUserProfile.Friends.ToList();
                ViewBag.friends = friends;

                var friendRequests = _loggedUserProfile.FriendRequests.Where(fr => fr.Accepted == null);
                if (friendRequests == null || friendRequests.Count() != 0)
                {
                    var frUserProfiles = friendRequests.Join(
                        _db.UserProfiles,
                        frs => frs.SenderUserProfileId,
                        ups => ups.GeniusUserProfileId,
                        (frs, ups) => new Tuple<FriendRequest, UserProfile>(frs, ups)
                        );
                    ViewBag.friendRequestsUserProfiles = frUserProfiles.ToList();
                }
                ViewBag.friendRequests = friendRequests.ToList();
                ViewBag.adminMessages = _db.AdminMessages.Where(m => m.UserId == _loggedUserProfile.UserId).ToList();
            }
            return View(groups);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NewGroup()
        {
            Group group = new Group();
            return View(group);
        }
        [HttpPost]
        public ActionResult NewGroup(Group group)
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            string _currentUserId = User.Identity.GetUserId();

            if(_currentUserId == adminId)
            {
                if (ModelState.IsValid)
                {
                    group.UserProfiles = new List<UserProfile>();
                    group.UserPosts = new List<UserPost>();

                    group.AdministratorId = adminId;
                    _db.Groups.Add(group);
                    _db.SaveChanges();
                }
                else
                {
                    return View("~/Views/Group/New.cshtml");
                }

                return RedirectToAction("ShowGroups", "Home");
            }

            UserProfile _currentUserProfile = _db.UserProfiles.Where(profile => profile.UserId == _currentUserId).First();
            
            try
            {
                if (ModelState.IsValid)
                {
                    group.UserProfiles = new List<UserProfile>();
                    group.UserPosts = new List<UserPost>();

                    group.UserProfiles.Add(_currentUserProfile);
                    group.AdministratorId = _currentUserProfile.UserId;

                    _db.Groups.Add(
                        group
                    );
                    _db.SaveChanges();
                }
                else
                {
                    return View("~/Views/Group/New.cshtml");
                }

                int id = _db.Groups.OrderByDescending(g => g.GroupId).First().GroupId;

                return RedirectToAction("Index", "Group", new { GroupId = id });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return Redirect("Index");
            }

        }

        public ActionResult ShowGroups()
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            string currentUserId = User.Identity.GetUserId();

            if (currentUserId != adminId)
                return RedirectToAction("Index");

            List<Group> groups = new List<Group>(_db.Groups);
            ViewBag.groups = groups;

            return View();
        }

        public ActionResult ShowUsers()
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            string currentUserId = User.Identity.GetUserId();

            if (currentUserId != adminId)
                return RedirectToAction("Index");

            List<UserProfile> profiles = new List<UserProfile>(_db.UserProfiles);
            ViewBag.profiles = profiles;

            return View();
        }

        public ActionResult NewUser()
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var adminId = UserManager.FindByEmail("admin@gmail.com").Id;

            string currentUserId = User.Identity.GetUserId();

            if (currentUserId != adminId)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewUser(RegisterViewModel model)
        {
            ApplicationUserManager _userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            ApplicationSignInManager _signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    _userManager.AddToRole(user.Id, "LoggedUser");

                    var profiles = _db.UserProfiles.OrderByDescending(u => u.GeniusUserProfileId);
                    var nextId = (profiles.ToList<UserProfile>().Count == 0 ? 0 : profiles.ToList<UserProfile>().First().GeniusUserProfileId) + 1;

                    var profile = new UserProfile
                    {
                        GeniusUserProfileId = nextId,
                        Visibility = "public",
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserId = user.Id,
                        //User = user
                    };

                    _db.UserProfiles.Add(profile);
                    try
                    {
                        // Your code...
                        // Could also be before try if you know the exception occurs in SaveChanges

                        _db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {

                    }

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("ShowUsers", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string Id)
        {
            var profile = _db.UserProfiles.Where(prof => prof.UserId == Id).First();
            
            foreach(var friend in profile.Friends)
            {
                friend.Friends.Remove(profile);
            }
            profile.Friends.Clear();
            profile.Groups.Clear();
            profile.UserPosts.Clear();
            profile.FriendRequests.Clear();
            _db.SaveChanges();
            _db.UserProfiles.Remove(profile);
            _db.SaveChanges();

            ApplicationUserManager _userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Id);
                var logins = user.Logins;
                var rolesForUser = await _userManager.GetRolesAsync(Id);

                using (var transaction = _db.Database.BeginTransaction())
                {
                    foreach (var login in logins.ToList())
                    {
                        await _userManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }

                    if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            var result = await _userManager.RemoveFromRoleAsync(user.Id, item);
                        }
                    }
                    

                    await _userManager.DeleteAsync(user);
                    transaction.Commit();
                }
            }

            return RedirectToAction("ShowUsers");
        }

    }
}
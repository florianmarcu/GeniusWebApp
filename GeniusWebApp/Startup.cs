using GeniusWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeniusWebApp.Startup))]
namespace GeniusWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            /*
              This method automatically creates the Administrator role with the admin's account and the other roles this app will have
            */
            CreateAdminUserAndApplicationRoles();
        }

        private void CreateAdminUserAndApplicationRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Start adding roles

            if (!roleManager.RoleExists("Admin")) // admin role with admin's account
            {

                // add the role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                // create the account
                var user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";
                var adminCreated = UserManager.Create(user, "!1Admin");
                if (adminCreated.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin"); // give the created user admin role
                }
            }
  
            if (!roleManager.RoleExists("LoggedUser"))
            {
                var role = new IdentityRole();
                role.Name = "LoggedUser";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("AnonymousUser"))
            {
                var role = new IdentityRole();
                role.Name = "AnonymousUser";
                roleManager.Create(role);
            }
        }
    }
}

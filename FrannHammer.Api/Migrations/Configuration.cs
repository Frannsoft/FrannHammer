using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FrannHammer.Api.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var getUserName = ConfigurationManager.AppSettings["basicusername"];
            var adminUserName = ConfigurationManager.AppSettings["adminusername"];

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager =
                new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var getUserFound = manager.FindByName(getUserName);
            var adminUserFound = manager.FindByName(adminUserName);

            if (getUserFound == null)
            {
                var newGetUser = new ApplicationUser()
                {
                    UserName = getUserName,
                    Email = "getuser@mymail.com",
                    EmailConfirmed = true
                };
                manager.Create(newGetUser, ConfigurationManager.AppSettings["basicpassword"]);
            }

            if (adminUserFound == null)
            {
                var newAdminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = "adminuser@myemail.com",
                    EmailConfirmed = true
                };
                manager.Create(newAdminUser, ConfigurationManager.AppSettings["adminpassword"]);
            }

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Basic" }); //Just GETs
            }

            var adminUser = manager.FindByName("KuroUser");
            manager.AddToRoles(adminUser.Id, "Admin");
            manager.AddToRoles(adminUser.Id, "Basic");

            var getUser = manager.FindByName("GETuser");
            manager.AddToRoles(getUser.Id, "Basic");
        }
    }
}

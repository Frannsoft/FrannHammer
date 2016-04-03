using KuroganeHammer.Data.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KuroganeHammer.Data.Api.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "KuroUser",
                Email = "testemail@mymail.com",
                EmailConfirmed = true,
            };

            manager.Create(user, "***REMOVED***");

            var getuser = new ApplicationUser()
            {
                UserName = "GETuser",
                Email = "getuser@mymail.com",
                EmailConfirmed = true,
            };

            manager.Create(getuser, "***REMOVED***");

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

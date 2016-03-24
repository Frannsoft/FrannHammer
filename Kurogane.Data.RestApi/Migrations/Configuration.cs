namespace Kurogane.Data.RestApi.Migrations
{
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Linq;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuthContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AuthContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AuthContext()));

            var user = new ApplicationUser()
            {
                UserName = "KuroUser",
                Email = "Frannsoftdev@outlook.com",
                EmailConfirmed = true,
                FirstName = "Kurogane",
                LastName = "Hammer",
                Level = 1,
                JoinDate = DateTime.Now,
                
            };
            
            manager.Create(user, "MySuperP@ssword!");

            if(!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" }); 
                roleManager.Create(new IdentityRole {Name = "Basic"}); //Just GETs
            }

            var adminUser = manager.FindByName("KuroUser");
            manager.AddToRoles(adminUser.Id, "Admin");
        }
    }
}

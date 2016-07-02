using System;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using FrannHammer.Api.Models;
using FrannHammer.Models;
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

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "Basic" }); //Just GETs
            //}

            //var adminUser = manager.FindByName("KuroUser");
            //manager.AddToRoles(adminUser.Id, "Admin");
            //manager.AddToRoles(adminUser.Id, "Basic");

            //var getUser = manager.FindByName("GETuser");
            //manager.AddToRoles(getUser.Id, "Basic");

            ////seed notations
            //if (!context.Notations.Any())
            //{
            //    var floatNotation = new Notation
            //    {
            //        Name = "FLOAT",
            //        NotationType = NotationTypes.Float,
            //        LastModified = DateTime.Now
            //    };

            //    var framesNotation = new Notation
            //    {
            //        Name = "FRAMES",
            //        NotationType = NotationTypes.Frames,
            //        LastModified = DateTime.Now
            //    };

            //    var booleanNotation = new Notation
            //    {
            //        Name = "BOOLEAN",
            //        NotationType = NotationTypes.Boolean,
            //        LastModified = DateTime.Now
            //    };

            //    context.Notations.Add(floatNotation);
            //    context.Notations.Add(framesNotation);
            //    context.Notations.Add(booleanNotation);
            //}

            //if (context.CharacterAttributes.Any() && !context.CharacterAttributeTypes.Any())
            //{
            //    var charAttributeNames = context.CharacterAttributes.Select(c => c.Name).Distinct();//get names

            //    //add char attribute type ids
            //    foreach (var name in charAttributeNames)
            //    {
            //        var charAttributeType = new CharacterAttributeType
            //        {
            //            Name = name
            //        };
            //        context.CharacterAttributeTypes.Add(charAttributeType);
            //    }
            //    context.SaveChanges();

            //    //now set the characterattributes
            //    foreach (var characterAttribute in context.CharacterAttributes.ToList())
            //    {
            //        characterAttribute.CharacterAttributeTypeId = context.CharacterAttributeTypes.Single(c => c.Name.Equals(characterAttribute.Name)).Id;
            //        context.CharacterAttributes.AddOrUpdate(characterAttribute);
            //    }
            //}


        }
    }
}

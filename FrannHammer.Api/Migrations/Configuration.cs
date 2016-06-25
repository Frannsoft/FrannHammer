using System;
using System.Data.Entity.Migrations;
using System.Linq;
using FrannHammer.Api.Models;
using FrannHammer.Core.Models;
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
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

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

            //seed notations
            if (!context.Notations.Any())
            {
                var floatNotation = new Notation
                {
                    Name = "FLOAT",
                    NotationType = NotationTypes.Float,
                    LastModified = DateTime.Now
                };

                var framesNotation = new Notation
                {
                    Name = "FRAMES",
                    NotationType = NotationTypes.Frames,
                    LastModified = DateTime.Now
                };

                var booleanNotation = new Notation
                {
                    Name = "BOOLEAN",
                    NotationType = NotationTypes.Boolean,
                    LastModified = DateTime.Now
                };

                context.Notations.Add(floatNotation);
                context.Notations.Add(framesNotation);
                context.Notations.Add(booleanNotation);
            }

            if (context.CharacterAttributes.Any() && !context.CharacterAttributeTypes.Any())
            {
                var charAttributeNames = context.CharacterAttributes.Select(c => c.Name).Distinct();//get names

                //add char attribute type ids
                foreach (var name in charAttributeNames)
                {
                    var charAttributeType = new CharacterAttributeType
                    {
                        Name = name
                    };
                    context.CharacterAttributeTypes.Add(charAttributeType);
                }
                context.SaveChanges();

                //now set the characterattributes
                foreach (var characterAttribute in context.CharacterAttributes.ToList())
                {
                    characterAttribute.CharacterAttributeTypeId = context.CharacterAttributeTypes.Single(c => c.Name.Equals(characterAttribute.Name)).Id;
                    context.CharacterAttributes.AddOrUpdate(characterAttribute);
                }
            }

            
        }
    }
}

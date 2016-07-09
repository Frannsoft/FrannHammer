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

            //seed notations
            const string notationFloat = "FLOAT";
            const string notationFrames = "FRAMES";
            const string notationBool = "BOOLEAN";

            if (!context.Notations.Any())
            {
                var floatNotation = new Notation
                {
                    Name = notationFloat,
                    LastModified = DateTime.Now
                };

                var framesNotation = new Notation
                {
                    Name = notationFrames,
                    LastModified = DateTime.Now
                };

                var booleanNotation = new Notation
                {
                    Name = notationBool,
                    LastModified = DateTime.Now
                };

                context.Notations.Add(floatNotation);
                context.Notations.Add(framesNotation);
                context.Notations.Add(booleanNotation);
            }

            if (context.CharacterAttributes.Any() && !context.CharacterAttributeTypes.Any())
            {
                var charAttributeNames = context.CharacterAttributes.Select(c => c.Name).Distinct().ToList();//get names

                //add char attribute type ids
                foreach (var name in charAttributeNames)
                {
                    int notationId;

                    if (name.Equals("FAST FALL SPEED") ||
                        name.Equals("MAX AIR SPEED VALUE") ||
                        name.Equals("MAX FALL SPEED VALUE") ||
                        name.Equals("MAX FALL SPEED") ||
                        name.Equals("MAX WALK SPEED VALUE") ||
                        name.Equals("SPEED INCREASE") ||
                        name.Equals("VALUE") ||
                        name.Equals("WEIGHT VALUE"))
                    {
                        notationId = context.Notations.First(n => n.Name.Equals(notationFloat)).Id;
                    }
                    else if (name.Equals("INTANGIBILITY") ||
                             name.Equals("INTANGIBLE") ||
                             name.Equals("FAF"))
                    {
                        notationId = context.Notations.First(n => n.Name.Equals(notationFrames)).Id;
                    }
                    else
                    {
                        notationId = context.Notations.First(n => n.Name.Equals(notationBool)).Id;
                    }


                    var charAttributeType = new CharacterAttributeType
                    {
                        NotationId = notationId,
                        LastModified = DateTime.Now,
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

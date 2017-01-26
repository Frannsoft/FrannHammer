using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Models;
using FrannHammer.WebScraper;
using FrannHammer.WebScraper.Stats;
using KuroganeHammer.TransferDBTool.Seeding;
using NUnit.Framework;

namespace KuroganeHammer.TransferDBTool
{
    //order to load:
    //0) generate cfm migration (add-migration) and update-database


    public class Loader : BaseTest
    {
        [Test]
        [Explicit("Data loader.  High intensity")]
        public async Task LoadAndSeedAndCleanDataFull()
        {
            Console.WriteLine("characters...");
            await ReloadCharacters();

            Console.WriteLine("smash attribute values...");
            await ReloadSmashAttributeTypes();

            Console.WriteLine("movements...");
            await ReloadMovements();

            Console.WriteLine("moves...");
            await ReloadMoves();

            Console.WriteLine("char attr values...");
            await ReloadSmash4CharacterAttributeValues();

            Console.WriteLine("seeding and cleaning all data...");
            using (var context = new AppDbContext())
            {
                var seeder = new Seeder(context);
                seeder.SeedAll();
            }

            Console.WriteLine("Done.");
        }

        [Test]
        [Explicit("Only Seed data.  Useful for testing only.")]
        public void SeedMoveDetailsOnly()
        {
            Console.WriteLine("seeding and cleaning all data...");
            using (var context = new AppDbContext())
            {
                var seeder = new Seeder(context);
                seeder.SeedMoveDetails();
            }

            Console.WriteLine("Done.");
        }

        [Test]
        [Explicit("Reloads character data")]
        public async Task ReloadCharacters()
        {
            var thumbnails = new HomePage("http://kuroganehammer.com/Smash4/")
                .GetThumbnailData();

            var idCounter = 0;

            var chars = Enum.GetNames(typeof(Characters)).ToList();
            chars.Sort();

            foreach (var character in chars.Select(c => new WebCharacter((Characters)Enum.Parse(typeof(Characters), c), ++idCounter)))
            {
                string val;
                if (character.Name.Contains("Mii") || character.Name.Contains("MII"))
                {
                    val = "MIIFIGHTERS";
                }
                else
                {
                    val = character.Name;
                }

                var thumbnail = thumbnails.FirstOrDefault(t => t.Key.Equals(val, StringComparison.OrdinalIgnoreCase));

                //load character
                var charStat = new CharacterStat(character.Id, character.Name, character.DisplayName,
                    character.Style, character.FullUrl, character.MainImageUrl, thumbnail.Url, character.ColorHex,
                    character.Description, DateTime.Now);

                var result = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "/Characters", charStat);
                Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            }
        }

        [Test]
        [Explicit("Reloads smash attribute types")]
        public async Task ReloadSmashAttributeTypes()
        {
            var thumbnails = new HomePage("http://kuroganehammer.com/Smash4/Attributes")
               .GetThumbnailData();

            foreach (var attributeType in thumbnails
                .Select(thumbnail => new SmashAttributeType
                { Name = thumbnail.Key }))
            {
                var result = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "/SmashAttributeTypes", attributeType);
                Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            }
        }

        [Test]
        [Explicit("Reloads movement data")]
        public async Task ReloadMovements()
        {
            var characters = LoggedInBasicClient.GetAsync(Baseuri + "/Characters")
                .Result.Content.ReadAsAsync<List<FrannHammer.Models.Character>>().Result;

            foreach (var character in characters.Select(c => new WebCharacter(c)))
            {
                var movements = from movement in character.FrameData.OfType<MovementStat>()
                                select movement;

                foreach (var movement in movements)
                {
                    movement.LastModified = DateTime.Now;
                    var movementResult = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "/movements", movement);
                    Assert.AreEqual(HttpStatusCode.Created, movementResult.StatusCode);
                }
            }
        }

        [Test]
        [Explicit("Reloads move data - LARGE")]
        public async Task ReloadMoves()
        {
            var characters = LoggedInBasicClient.GetAsync(Baseuri + "/Characters")
                .Result.Content.ReadAsAsync<List<Character>>().Result;

            foreach (var character in characters.Select(c => new WebCharacter(c)))
            {
                //load moves
                var moves = from move in character.FrameData.OfType<MoveStat>()
                            select move;

                foreach (var move in moves)
                {
                    move.LastModified = DateTime.Now;
                    var moveResult = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "/moves", move);
                    Assert.AreEqual(HttpStatusCode.Created, moveResult.StatusCode);
                }
            }
        }

        [Test]
        [Explicit("Reloads sm4sh character attribute values")]
        public async Task ReloadSmash4CharacterAttributeValues()
        {
            //get all attributeTypes in db
            var attributeTypes = LoggedInAdminClient.GetAsync(Baseuri + "/smashattributetypes")
                .Result.Content.ReadAsAsync<List<SmashAttributeType>>().Result;

            var baseUrl = "http://kuroganehammer.com/Smash4/";

            foreach (var attributeType in attributeTypes)
            {
                if (attributeType.Name.Contains("ITEMTOSS") ||
                    attributeType.Name.Equals("LEDGEJUMP") ||
                    attributeType.Name.Equals("TECH") ||
                    attributeType.Name.Equals("AIRDECELERATION") ||
                    attributeType.Name.Equals("AIRFRICTION") ||
                    attributeType.Name.Equals("DASHLENGTH") ||
                    attributeType.Name.Equals("COUNTERS") ||
                    attributeType.Name.Equals("ELEMENTS") ||
                    attributeType.Name.Equals("GETUPOPTIONS") ||
                    attributeType.Name.Equals("REFLECTORS") ||
                    attributeType.Name.Equals("SHIELDS") ||
                    attributeType.Name.Equals("SMASHCHARGERELEASE") ||
                    attributeType.Name.Equals("TRIP") ||
                    attributeType.Name.Equals("JABLOCK") ||
                    attributeType.Name.Equals("RUNSPEED"))
                { continue; } //skip these for now since the tables are problematic

                var page = new Page(baseUrl + attributeType.Name);
                Console.WriteLine(attributeType.Name);
                var attributesFromPage = page.GetAttributes();

                var fullAtts = new List<CharacterAttribute>();
                var existingCharacters = LoggedInBasicClient.GetAsync(Baseuri + "/Characters")
                 .Result.Content.ReadAsAsync<List<FrannHammer.Models.Character>>().Result;

                foreach (var attributeRow in attributesFromPage.AttributeValues)
                {
                    var rank = attributeRow.Values.First(a => a.Name.ToLower() == "rank").Value;
                    var characterName = MapCharacterName(attributeRow.Values.First(a => a.Name.ToLower() == "character").Value).Trim();

                    if (string.IsNullOrEmpty(characterName))
                    { continue; } //if no characterName was found there is a high probability this table row was a decorator rather than a real value

                    var specificValues = (from attr in attributeRow.Values.Where(a => a.Name.ToLower() != "rank" &&
                                                                                     a.Name.ToLower() != "character")
                                          select attr).ToList();

                    for (var i = 0; i < specificValues.Count(); i++)
                    {
                        var attributeName = specificValues[i].Name;
                        var dbAttributeType = attributeTypes.Find(a => a.Name.Equals(specificValues[i].AttributeFlag)); //   (CharacterAttributes)Enum.Parse(typeof(CharacterAttributes), specificValues[i].AttributeFlag, true);
                        var value = specificValues[i].Value;
                        var characterAttribute = new CharacterAttribute
                        {
                            Rank = rank,
                            LastModified = DateTime.Now,
                            Name = attributeName,
                            OwnerId = existingCharacters.First(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase)).Id,
                            SmashAttributeTypeId = dbAttributeType.Id,
                            Value = value
                        };
                        fullAtts.Add(characterAttribute);

                        var postResult = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "/characterattributes", characterAttribute);
                        Assert.AreEqual(HttpStatusCode.Created, postResult.StatusCode);
                    }
                    Assert.That(fullAtts.Count > 0);
                }
            }
        }



        /// <summary>
        /// Not all values in the character field match values in enum.  Manually map those that don't.
        /// </summary>
        /// <param name="rawName"></param>
        /// <returns></returns>
        private string MapCharacterName(string rawName)
        {
            string retVal;

            rawName = rawName.Replace(".", string.Empty)
                .Replace("(Default size)", string.Empty)
                .Replace(" ", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace("-", string.Empty)
                .Replace("&", string.Empty);

            if (rawName.Equals("Dedede"))
            {
                retVal = Characters.Kingdedede.ToString();
            }
            else if (rawName.Equals("MrGameWatch") || rawName.Equals("GameWatch"))
            {
                retVal = Characters.Mrgamewatch.ToString();
            }
            else if (rawName.Equals("Rosalina"))
            {
                retVal = Characters.Rosalinaluma.ToString();
            }
            else if (rawName.Equals("DuckHuntDog") || rawName.Equals("DuckHuntDuo"))
            {
                retVal = Characters.Duckhunt.ToString();
            }
            else if (rawName.Equals("Diddy"))
            {
                retVal = Characters.Diddykong.ToString();
            }
            else if (rawName.Equals("GreninjaForward") || rawName.Equals("GreninjaBack"))
            {
                retVal = Characters.Greninja.ToString();
            }
            else if (rawName.Equals("Lucina'sworthlessgrandfather"))
            {
                retVal = Characters.Marth.ToString();
            }
            else if (rawName.Equals("Marth'sworthlessgranddaughter"))
            {
                retVal = Characters.Lucina.ToString();
            }
            else if (rawName.Equals("EbolaBackThrow"))
            {
                retVal = Characters.Ness.ToString();
            }
            else if (rawName.Equals("MiiSwordspider"))
            {
                retVal = Characters.Miiswordfighter.ToString();
            }
            else
            {
                retVal = rawName;
            }

            return retVal;
        }
    }
}

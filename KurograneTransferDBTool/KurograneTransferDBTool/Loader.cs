using KuroganeHammer.WebScraper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Api.Models;
using KuroganeHammer.Data.Core;
using Character = KuroganeHammer.WebScraper.Character;

namespace KurograneTransferDBTool
{
    //order to load:
    //1) characters
    //2) smash attribute types
    //3) movements
    //4) moves
    //5) attribute values

    public class Loader : BaseTest
    {
        //[Test]
        //[Explicit("Updates character data")]
        //public async Task UpdateCharacterData()
        //{
        //    var charIds = (int[])Enum.GetValues(typeof(Characters));

        //    var response = await LoggedInAdminClient.GetAsync(Baseuri + "characters");
        //    var characters = await response.Content.ReadAsAsync<CharacterDTO[]>();

        //    foreach (var i in charIds)
        //    {
        //        var character = new Character((Characters)i);

        //        //var characterStatFromDB = characters.FirstOrDefault(c => c.Id == character.i;
        //        if (characterStatFromDB == null)
        //        { throw new InvalidOperationException("Character from page data could not be found in db"); }

        //        var cachedHashCode = characterStatFromDB.GetHashCode();
        //        PropertyUpdateHelper.UpdateProperty(character, characterStatFromDB);

        //        var newHashCode = characterStatFromDB.GetHashCode();

        //        //if updated version has differences, push update to server
        //        if (cachedHashCode != newHashCode)
        //        {
        //            //submit update post
        //            var updateResponse = await LoggedInAdminClient.PutAsJsonAsync(Baseuri + "characters/" + characterStatFromDB.Id, characterStatFromDB);

        //            //check if OK - 200 //if not, stop updating
        //            Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
        //        }

        //    }
        //}

        //[Test]
        //[Explicit("Updates movement data")]
        //public async Task UpdateMovementData()
        //{
        //    var charIds = (int[])Enum.GetValues(typeof(Characters));

        //    var response = await LoggedInAdminClient.GetAsync(Baseuri + "movements");
        //    var movementsFromDB = await response.Content.ReadAsAsync<MovementStatDTO[]>();

        //    foreach (var i in charIds)
        //    {
        //        var character = new Character((Characters)i);

        //        var movements = from charMoves in character.FrameData.Values.OfType<MovementStat>()
        //                        select charMoves;

        //        foreach (var move in movements)
        //        {
        //            //get a single move

        //            //find it in the returned array of existing movements by id

        //            //TODO: ID doesn't exist on kuro's page so we have nothing to go on here.
        //            //probably going to have to create a hash for each movement and move and check it against the page values to determine if there's a difference
        //            //shouldn't be a big deal.
        //            var moveFromDB = movementsFromDB.FirstOrDefault(m => m.Id == move.Id);

        //            if (moveFromDB == null)
        //            { throw new InvalidOperationException("Unable to find move in DB."); }

        //            var cachedHashCode = moveFromDB.GetHashCode();
        //            PropertyUpdateHelper.UpdateProperty(move, moveFromDB);
        //            var newHashCode = moveFromDB.GetHashCode();

        //            //if updated version has hash differences, push update to server
        //            if (cachedHashCode != newHashCode)
        //            {
        //                //submit update post
        //                var updateResponse = await LoggedInAdminClient.PutAsJsonAsync(Baseuri + "movement/" + moveFromDB.Id, moveFromDB);

        //                //check if OK - 200 //if not, stop updating
        //                Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
        //            }
        //        }
        //    }
        //}

        //[Test]
        //[Explicit("Actually reloads Character Attributes in the DB")]
        ////[TestCase("http://kuroganehammer.com/Smash4/ItemTossBack")] - abnormal tables
        ////[TestCase("http://kuroganehammer.com/Smash4/ItemTossDash")] - includes 'everyone else' which I'm not handling
        ////[TestCase("http://kuroganehammer.com/Smash4/ItemTossDown")] - this url doesn't exist anymore
        ////[TestCase("http://kuroganehammer.com/Smash4/ItemTossForward")] - abnormal tables
        ////[TestCase("http://kuroganehammer.com/Smash4/ItemTossUp")] - abnormal tables
        ////[TestCase("http://kuroganehammer.com/Smash4/LedgeJump")] - abnormal table values
        ////[TestCase("http://kuroganehammer.com/Smash4/Tech")] - abnormal tables
        //[TestCase("http://kuroganehammer.com/Smash4/AirAcceleration")]
        //[TestCase("http://kuroganehammer.com/Smash4/Airdodge")]
        //[TestCase("http://kuroganehammer.com/Smash4/AirSpeed")]
        //[TestCase("http://kuroganehammer.com/Smash4/FallSpeed")]
        //[TestCase("http://kuroganehammer.com/Smash4/Gravity")]
        //[TestCase("http://kuroganehammer.com/Smash4/ItemToss")]
        //[TestCase("http://kuroganehammer.com/Smash4/Jumpsquat")]
        //[TestCase("http://kuroganehammer.com/Smash4/LedgeAttack")]
        //[TestCase("http://kuroganehammer.com/Smash4/LedgeGetup")]
        //[TestCase("http://kuroganehammer.com/Smash4/LedgeRoll")]
        //[TestCase("http://kuroganehammer.com/Smash4/Rolls")]
        //[TestCase("http://kuroganehammer.com/Smash4/DashSpeed")]
        //[TestCase("http://kuroganehammer.com/Smash4/Spotdodge")]
        //[TestCase("http://kuroganehammer.com/Smash4/WalkSpeed")]
        //[TestCase("http://kuroganehammer.com/Smash4/Weight")]
        //public async Task ReloadCharacterAttribute(string url)
        //{
        //    var page = new Page(url);
        //    var attributesFromPage = page.GetAttributes();

        //    var fullAtts = new List<CharacterAttribute>();
        //    foreach (var attributeRow in attributesFromPage.AttributeValues)
        //    {
        //        var rank = attributeRow.Values.First(a => a.Name.ToLower() == "rank").Value;
        //        var characterName = MapCharacterName(attributeRow.Values.First(a => a.Name.ToLower() == "character").Value);

        //        if (string.IsNullOrEmpty(characterName))
        //        { continue; } //if no characterName was found there is a high probability this table row was a decorator rather than a real value

        //        var specificValues = (from attr in attributeRow.Values.Where(a => a.Name.ToLower() != "rank" &&
        //                                                                         a.Name.ToLower() != "character")
        //                              select attr).ToList();

        //        for (var i = 0; i < specificValues.Count(); i++)
        //        {
        //            var attributeName = specificValues[i].Name;
        //            var attributeType = (CharacterAttributes)Enum.Parse(typeof(CharacterAttributes), specificValues[i].AttributeFlag, true);
        //            var value = specificValues[i].Value;

        //            var characterAttribute = new CharacterAttribute(rank, characterName, attributeName, value,
        //                attributeType);
        //            fullAtts.Add(characterAttribute);

        //            var postResult = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "attributes", characterAttribute);
        //            Assert.AreEqual(HttpStatusCode.OK, postResult.StatusCode);
        //        }
        //        Assert.That(fullAtts.Count > 0);
        //    }
        //}

        [Test]
        [Explicit("Reloads character data")]
        public async Task ReloadCharacters()
        {
            var charIds = (int[])Enum.GetValues(typeof(Characters));

            var thumbnails = new HomePage("http://kuroganehammer.com/Smash4/")
                .GetThumbnailData();

            foreach (var character in charIds.Select(i => new Character((Characters)i)))
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
                var charStat = new CharacterStat(character.Id, character.Name,
                    character.Style, character.MainImageUrl, thumbnail.Url, character.ColorHex,
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
                .Result.Content.ReadAsAsync<List<CharacterStat>>().Result;

            foreach (var character in characters.Select(i => new Character(i.Name)))
            {
                var movements = from movement in character.FrameData.Values.OfType<MovementStat>()
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
                .Result.Content.ReadAsAsync<List<CharacterStat>>().Result;

            foreach (var character in characters.Select(i => new Character(i.Name)))
            {
                //load moves
                var moves = from move in character.FrameData.Values.OfType<MoveStat>()
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
                if(attributeType.Name.Contains("ITEMTOSS") ||
                    attributeType.Name.Equals("LEDGEJUMP") ||
                    attributeType.Name.Equals("TECH") ||
                    attributeType.Name.Equals("RUNSPEED"))
                { continue; } //skip these for now since the tables are problematic

                var page = new Page(baseUrl + attributeType.Name);
                Console.WriteLine(attributeType.Name);
                var attributesFromPage = page.GetAttributes();

                var fullAtts = new List<CharacterAttribute>();
                foreach (var attributeRow in attributesFromPage.AttributeValues)
                {
                    var rank = attributeRow.Values.First(a => a.Name.ToLower() == "rank").Value;
                    var characterName = MapCharacterName(attributeRow.Values.First(a => a.Name.ToLower() == "character").Value);

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
                            OwnerId = CharacterUtility.GetCharacterIdFromName(characterName),
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

        //[Test]
        //[Explicit("Actually reloads all data")]
        //public async Task ReloadAllCharacterData()
        //{
        //    var charIds = (int[])Enum.GetValues(typeof(Characters));

        //    var thumbnails = new HomePage("http://kuroganehammer.com/Smash4/")
        //        .GetThumbnailData();

        //    foreach (var character in charIds.Select(i => new Character((Characters)i)))
        //    {
        //        string val;
        //        if (character.Name.Contains("Mii") || character.Name.Contains("MII"))
        //        {
        //            val = "MIIFIGHTERS";
        //        }
        //        else
        //        {
        //            val = character.Name;
        //        }

        //        var thumbnail = thumbnails.FirstOrDefault(t => t.Key.Equals(val, StringComparison.OrdinalIgnoreCase));

        //        //load character
        //        var charStat = new CharacterStat(character.Id, character.Name,
        //            character.Style, character.MainImageUrl, thumbnail.Url,
        //            character.ColorHex, character.Description, DateTime.Now);

        //        var result = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "characters", charStat);
        //        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        //        //load moves
        //        var moves = from move in character.FrameData.Values.OfType<MoveStat>()
        //                    select move;

        //        foreach (var move in moves)
        //        {
        //            var moveResult = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "moves", move);
        //            Assert.AreEqual(HttpStatusCode.OK, moveResult.StatusCode);
        //        }

        //        var movements = from movement in character.FrameData.Values.OfType<MovementStat>()
        //                        select movement;

        //        foreach (var movement in movements)
        //        {
        //            var movementResult = await LoggedInAdminClient.PostAsJsonAsync(Baseuri + "movement", movement);
        //            Assert.AreEqual(HttpStatusCode.OK, movementResult.StatusCode);
        //        }
        //    }
        //}

        [Test]
        [TestCase("35-39", 4)]
        [TestCase("2-15", 13)]
        [TestCase("2,4,6,8-12", 7)]
        [TestCase("18-19, 21-22, 24-25, 27-28, 30-31", 5)]
        public void GetHitboxLength(string hitboxActive, int expectedTotal)
        {
            var ints = new List<int>();
            string[] vals;
            if (hitboxActive.Contains(','))
            {
                vals = hitboxActive.Split(new char[] { ',' });

                foreach (var commaVal in vals)
                {
                    AddHitboxLength(ints, commaVal.Split(new char[] { '-' }, 2));
                }
            }
            else
            {
                AddHitboxLength(ints, hitboxActive.Split(new char[] { '-' }, 2));
            }

            var total = ints.Aggregate(0, (i, j) => i + j);
            Assert.AreEqual(expectedTotal, total);
        }

        private void AddHitboxLength(List<int> ints, string[] vals)
        {
            var result = GetDifference(vals);
            if (result > 0)
            {
                ints.Add(result);
            }
        }

        private int GetDifference(string[] vals)
        {
            int one, two, result;

            if (vals.Length > 1)
            {
                one = Convert.ToInt32(vals[0]);
                two = Convert.ToInt32(vals[1]);
                result = two - one;
            }
            else
            {
                result = 1; //add one
            }


            return result;
        }

        //[Test]
        //public async Task ReloadThumbnailData()
        //{
        //    var homePage = new HomePage("http://kuroganehammer.com/Smash4/");
        //    var images = homePage.GetThumbnailData();

        //    foreach (var image in images)
        //    {

        //        //these three need to be updated manually for now
        //        if (image.Key != "MIIFIGHTERS")
        //        {
        //            var character = (Characters)Enum.Parse(typeof(Characters), image.Key);

        //            var get = await LoggedInAdminClient.GetAsync(Baseuri + "characters/" + (int)character);

        //            var dto = JsonConvert.DeserializeObject<Character>(await get.Content.ReadAsStringAsync());

        //            dto.ThumbnailUrl = image.Url;
        //            var putResult = await LoggedInAdminClient.PutAsJsonAsync(Baseuri + "characters/" + (int)character, dto);
        //            Assert.AreEqual(HttpStatusCode.OK, putResult.StatusCode);
        //        }
        //    }
        //}

        //[Test]
        //[Ignore("permanent")]
        //public async Task UpdateCharacter()
        //{
        //    var get = await LoggedInAdminClient.GetAsync(Baseuri + +(int)Characters.Bowser + "/character");

        //    var bowser = JsonConvert.DeserializeObject<CharacterDTO>(await get.Content.ReadAsStringAsync());

        //    bowser.Name = "NEWNAME";

        //    var result = await LoggedInAdminClient.PutAsJsonAsync(Baseuri + +(int)Characters.Bowser + "/character", bowser);
        //}

    }
}

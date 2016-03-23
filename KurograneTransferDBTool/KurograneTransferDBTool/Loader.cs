using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core;
using KuroganeHammer.WebScraper;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KurograneTransferDBTool
{
    public class Loader : BaseTest
    {
        [Test]
        [Explicit("Updates character data")]
        public async Task UpdateCharacterData()
        {
            var charIds = (int[])Enum.GetValues(typeof(Characters));

            var response = await LoggedInClient.GetAsync(Baseuri + "characters");
            var characters = await response.Content.ReadAsAsync<CharacterDTO[]>();

            foreach (var i in charIds)
            {
                var character = new Character((Characters)i);

                var characterStatFromDB = characters.FirstOrDefault(c => c.OwnerId == character.OwnerId);
                if (characterStatFromDB == null)
                { throw new InvalidOperationException("Character from page data could not be found in db"); }

                var cachedHashCode = characterStatFromDB.GetHashCode();
                PropertyUpdateHelper.UpdateProperty(character, characterStatFromDB);

                var newHashCode = characterStatFromDB.GetHashCode();

                //if updated version has differences, push update to server
                if (cachedHashCode != newHashCode)
                {
                    //submit update post
                    var updateResponse = await LoggedInClient.PutAsJsonAsync(Baseuri + "characters/" + characterStatFromDB.Id, characterStatFromDB);

                    //check if OK - 200 //if not, stop updating
                    Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
                }

            }
        }

        [Test]
        [Explicit("Updates movement data")]
        public async Task UpdateMovementData()
        {
            var charIds = (int[])Enum.GetValues(typeof(Characters));

            var response = await LoggedInClient.GetAsync(Baseuri + "movements");
            var movementsFromDB = await response.Content.ReadAsAsync<MovementStatDTO[]>();

            foreach (var i in charIds)
            {
                var character = new Character((Characters)i);

                var movements = from charMoves in character.FrameData.Values.OfType<MovementStat>()
                                select charMoves;

                foreach (var move in movements)
                {
                    //get a single move

                    //find it in the returned array of existing movements by id

                    //TODO: ID doesn't exist on kuro's page so we have nothing to go on here.
                    //probably going to have to create a hash for each movement and move and check it against the page values to determine if there's a difference
                    //shouldn't be a big deal.
                    var moveFromDB = movementsFromDB.FirstOrDefault(m => m.Id == move.Id);

                    if (moveFromDB == null)
                    { throw new InvalidOperationException("Unable to find move in DB."); }

                    var cachedHashCode = moveFromDB.GetHashCode();
                    PropertyUpdateHelper.UpdateProperty(move, moveFromDB);
                    var newHashCode = moveFromDB.GetHashCode();

                    //if updated version has hash differences, push update to server
                    if (cachedHashCode != newHashCode)
                    {
                        //submit update post
                        var updateResponse = await LoggedInClient.PutAsJsonAsync(Baseuri + "movement/" + moveFromDB.Id, moveFromDB);

                        //check if OK - 200 //if not, stop updating
                        Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
                    }
                }
            }
        }

        [Test]
        [Explicit("Actually reloads all data")]
        public async Task ReloadAllCharacterData()
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
                var charStat = new CharacterStat(character.Name,
                    character.OwnerId, character.Style, character.MainImageUrl, thumbnail.Url, character.Description);

                var result = await LoggedInClient.PostAsJsonAsync(Baseuri + "characters", charStat);
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

                //load moves
                var moves = from move in character.FrameData.Values.OfType<MoveStat>()
                    select move;

                foreach (var move in moves)
                {
                    var moveResult = await LoggedInClient.PostAsJsonAsync(Baseuri + "moves", move);
                    Assert.AreEqual(HttpStatusCode.OK, moveResult.StatusCode);
                }

                var movements = from movement in character.FrameData.Values.OfType<MovementStat>()
                    select movement;

                foreach (var movement in movements)
                {
                    var movementResult = await LoggedInClient.PostAsJsonAsync(Baseuri + "movement", movement);
                    Assert.AreEqual(HttpStatusCode.OK, movementResult.StatusCode);
                }
            }
        }

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

        [Test]
        public async Task ReloadThumbnailData()
        {
            var homePage = new HomePage("http://kuroganehammer.com/Smash4/");
            var images = homePage.GetThumbnailData();

            foreach (var image in images)
            {

                //these three need to be updated manually for now
                if (image.Key != "MIIFIGHTERS")
                {
                    var character = (Characters)Enum.Parse(typeof(Characters), image.Key);

                    var get = await LoggedInClient.GetAsync(Baseuri + "characters/" + (int)character);

                    var dto = JsonConvert.DeserializeObject<CharacterDTO>(await get.Content.ReadAsStringAsync());

                    dto.ThumbnailUrl = image.Url;
                    var putResult = await LoggedInClient.PutAsJsonAsync(Baseuri + "characters/" + (int)character, dto);
                    Assert.AreEqual(HttpStatusCode.OK, putResult.StatusCode);
                }
            }
        }

        [Test]
        [Ignore("permanent")]
        public async Task UpdateCharacter()
        {
            var get = await LoggedInClient.GetAsync(Baseuri + +(int)Characters.Bowser + "/character");

            var bowser = JsonConvert.DeserializeObject<CharacterDTO>(await get.Content.ReadAsStringAsync());

            bowser.Name = "NEWNAME";

            var result = await LoggedInClient.PutAsJsonAsync(Baseuri + +(int)Characters.Bowser + "/character", bowser);
        }

    }
}

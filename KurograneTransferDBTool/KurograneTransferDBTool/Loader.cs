using Kurogane.Data.RestApi.DTOs;
using Kurogane.Data.RestApi.Models;
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
            int[] charIds = (int[])Enum.GetValues(typeof(Characters));

            var response = await client.GetAsync(BASEURL + "characters");
            CharacterDTO[] characters = await response.Content.ReadAsAsync<CharacterDTO[]>();

            foreach(int i in charIds)
            {
                Character character = new Character((Characters)i);

                var characterStatFromDB = characters.FirstOrDefault(c => c.OwnerId == character.OwnerId);
                if(characterStatFromDB == null)
                { throw new InvalidOperationException("Character from page data could not be found in db"); }

                var cachedHashCode = characterStatFromDB.GetHashCode();
                PropertyUpdateHelper.UpdateProperty(character, characterStatFromDB);

                var newHashCode = characterStatFromDB.GetHashCode();

                //if updated version has differences, push update to server
                if(cachedHashCode != newHashCode)
                { 
                    //submit update post
                    var updateResponse = await client.PutAsJsonAsync(BASEURL + "characters/" + characterStatFromDB.Id, characterStatFromDB);

                    //check if OK - 200 //if not, stop updating
                    Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
                }

            }
        }

        [Test]
        [Explicit("Actually reloads all data")]
        public async Task ReloadAllCharacterData()
        {
            int[] charIds = (int[])Enum.GetValues(typeof(Characters));

            List<Thumbnail> thumbnails = new HomePage("http://kuroganehammer.com/Smash4/")
                .GetThumbnailData();

            foreach (int i in charIds)
            {
                Character character = new Character((Characters)i);

                string val = string.Empty;
                if(character.Name.Contains("Mii") || character.Name.Contains("MII"))
                {
                    val = "MIIFIGHTERS";
                }
                else
                {
                    val = character.Name;
                }

                Thumbnail thumbnail = thumbnails.FirstOrDefault(t => t.Key.Equals(val, StringComparison.OrdinalIgnoreCase));

                //load character
                CharacterStat charStat = new CharacterStat(character.Name,
                    character.OwnerId, character.Style, character.MainImageUrl, thumbnail.Url, character.Description);

                var result = await client.PostAsJsonAsync(BASEURL + "characters", charStat);
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

                //load moves
                var moves = from move in character.FrameData.Values.OfType<MoveStat>()
                            select move;

                foreach (var move in moves)
                {
                    var moveResult = await client.PostAsJsonAsync(BASEURL + "moves", move);
                    Assert.AreEqual(HttpStatusCode.OK, moveResult.StatusCode);
                }

                var movements = from movement in character.FrameData.Values.OfType<MovementStat>()
                                select movement;

                foreach (var movement in movements)
                {
                    var movementResult = await client.PostAsJsonAsync(BASEURL + "movement", movement);
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
            List<int> ints = new List<int>();
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

            int total = ints.Aggregate(0, (i, j) => i + j);
            Assert.AreEqual(expectedTotal, total);
        }

        private void AddHitboxLength(List<int> ints, string[] vals)
        {
            int result = GetDifference(vals);
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
            HomePage homePage = new HomePage("http://kuroganehammer.com/Smash4/");
            List<Thumbnail> images = homePage.GetThumbnailData();

            foreach (var image in images)
            {

                //these three need to be updated manually for now
                if (image.Key != "MIIFIGHTERS")
                {
                    Characters character = (Characters)Enum.Parse(typeof(Characters), image.Key);

                    var get = await client.GetAsync(BASEURL + "characters/" + (int)character);

                    CharacterDTO dto = JsonConvert.DeserializeObject<CharacterDTO>(await get.Content.ReadAsStringAsync());

                    dto.ThumbnailUrl = image.Url;
                    var putResult = await client.PutAsJsonAsync(BASEURL + "characters/" + (int)character, dto);
                    Assert.AreEqual(HttpStatusCode.OK, putResult.StatusCode);
                }
            }
        }

        [Test]
        public async Task ReloadMovement()
        {
            int[] charIds = (int[])Enum.GetValues(typeof(Characters));

            foreach (int i in charIds)
            {
                Character character = new Character((Characters)i);


                //var movementMoves = from move in character.FrameData.Values.OfType<MovementStat>()
                //                    select new MovementStatDTO()
                //                    {
                //                        Name = move.Name,
                //                        OwnerId = move.OwnerId,
                //                        Value = move.Value
                //                    };

                //foreach (var movementMove in movementMoves)
                //{
                //    var movementResult = await client.PostAsJsonAsync(BASEURL + "movement", movementMove);
                //    Assert.AreEqual(HttpStatusCode.OK, movementResult.StatusCode);
                //}
            }
        }

        [Test]
        [Ignore("permanent")]
        public async Task UpdateCharacter()
        {
            var get = await client.GetAsync(BASEURL + +(int)Characters.BOWSER + "/character");

            CharacterDTO bowser = JsonConvert.DeserializeObject<CharacterDTO>(await get.Content.ReadAsStringAsync());

            bowser.Name = "NEWNAME";

            var result = await client.PutAsJsonAsync(BASEURL + +(int)Characters.BOWSER + "/character", bowser);
        }

    }
}

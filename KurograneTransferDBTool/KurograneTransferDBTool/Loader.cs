using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Characters;
using KuroganeHammer.Data.Core.Model.Stats;
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
        public async Task ReloadAllCharacterData()
        {
            int[] charIds = (int[])Enum.GetValues(typeof(Characters));

            foreach (int i in charIds)
            {
                Character character = new Character((Characters)i);

                //load character
                CharacterDTO charDTO = new CharacterDTO()
                {
                    Description = character.Description,
                    MainImageUrl = character.MainImageUrl,
                    ThumbnailUrl = character.ThumbnailUrl,
                    Name = character.Name,
                    OwnerId = character.OwnerId,
                    Style = character.Style
                };

                var result = await client.PostAsJsonAsync(BASEURL + "character", charDTO);
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

                //load moves
                var groundMoves = from move in character.FrameData.Values.OfType<GroundStat>()
                                  select new MoveDTO()
                                  {
                                      Angle = move.Angle,
                                      BaseDamage = move.BaseDamage,
                                      BaseKnockBackSetKnockback = move.BaseKnockBackSetKnockback,
                                      FirstActionableFrame = move.FirstActionableFrame,
                                      HitboxActive = move.HitboxActive,
                                      KnockbackGrowth = move.KnockbackGrowth,
                                      Name = move.Name,
                                      OwnerId = move.OwnerId,
                                      Type = MoveType.Ground
                                  };

                var aerialMoves = from move in character.FrameData.Values.OfType<AerialStat>()
                                  select new MoveDTO()
                                  {
                                      Angle = move.Angle,
                                      AutoCancel = move.AutoCancel,
                                      BaseDamage = move.BaseDamage,
                                      BaseKnockBackSetKnockback = move.BaseKnockBackSetKnockback,
                                      FirstActionableFrame = move.FirstActionableFrame,
                                      HitboxActive = move.HitboxActive,
                                      KnockbackGrowth = move.KnockbackGrowth,
                                      LandingLag = move.LandingLag,
                                      Name = move.Name,
                                      OwnerId = move.OwnerId,
                                      Type = MoveType.Aerial
                                  };

                var specialMoves = from move in character.FrameData.Values.OfType<SpecialStat>()
                                   select new MoveDTO()
                                   {
                                       Angle = move.Angle,
                                       BaseDamage = move.BaseDamage,
                                       BaseKnockBackSetKnockback = move.BaseKnockBackSetKnockback,
                                       FirstActionableFrame = move.FirstActionableFrame,
                                       HitboxActive = move.HitboxActive,
                                       KnockbackGrowth = move.KnockbackGrowth,
                                       Name = move.Name,
                                       OwnerId = move.OwnerId,
                                       Type = MoveType.Special
                                   };

                var movementMoves = from move in character.FrameData.Values.OfType<MovementStat>()
                                    select new MovementStatDTO()
                                    {
                                        Name = move.Name,
                                        OwnerId = move.OwnerId,
                                        Value = move.Value
                                    };

                foreach (var groundMove in groundMoves)
                {
                    var groundResult = await client.PostAsJsonAsync(BASEURL + "move", groundMove);
                    Assert.AreEqual(HttpStatusCode.OK, groundResult.StatusCode);
                }

                foreach (var aerialMove in aerialMoves)
                {
                    var aerialResult = await client.PostAsJsonAsync(BASEURL + "move", aerialMove);
                    Assert.AreEqual(HttpStatusCode.OK, aerialResult.StatusCode);
                }

                foreach (var specialMove in specialMoves)
                {
                    var specialResult = await client.PostAsJsonAsync(BASEURL + "move", specialMove);
                    Assert.AreEqual(HttpStatusCode.OK, specialResult.StatusCode);
                }

                foreach (var movementMove in movementMoves)
                {
                    var movementResult = await client.PostAsJsonAsync(BASEURL + "movement", movementMove);
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


                var movementMoves = from move in character.FrameData.Values.OfType<MovementStat>()
                                    select new MovementStatDTO()
                                    {
                                        Name = move.Name,
                                        OwnerId = move.OwnerId,
                                        Value = move.Value
                                    };

                foreach (var movementMove in movementMoves)
                {
                    var movementResult = await client.PostAsJsonAsync(BASEURL + "movement", movementMove);
                    Assert.AreEqual(HttpStatusCode.OK, movementResult.StatusCode);
                }
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

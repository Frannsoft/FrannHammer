using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Data.Core.Model.Characters;
using KuroganeHammer.Data.Core.Model.Stats;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KurograneTransferDBTool
{
    public class Loader : BaseTest
    {
        [Test]
        [Ignore("permanent")]
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
                    ImageUrl = character.ImageUrl,
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
                                      BaseKnockBackSetKnockback = move.BaseKnockbackSetKnockback,
                                      FirstActionableFrame = move.FirstActionableFrame,
                                      HitboxActive = move.HitBoxActive,
                                      KnockbackGrowth = move.KnockbackGrowth,
                                      Name = move.Name,
                                      OwnerId = move.OwnerId,
                                      Type = MoveType.Ground
                                  };

                var aerialMoves = from move in character.FrameData.Values.OfType<AerialStat>()
                                  select new MoveDTO()
                                  {
                                      Angle = move.Angle,
                                      AutoCancel = move.Autocancel,
                                      BaseDamage = move.BaseDamage,
                                      BaseKnockBackSetKnockback = move.BaseKnockbackSetKnockback,
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
                                       BaseKnockBackSetKnockback = move.BaseKnockbackSetKnockback,
                                       FirstActionableFrame = move.FirstActionableFrame,
                                       HitboxActive = move.HitboxActive,
                                       KnockbackGrowth = move.KnockbackGrowth,
                                       Name = move.Name,
                                       OwnerId = move.OwnerId,
                                       Type = MoveType.Special
                                   };

                foreach (var groundMove in groundMoves)
                {
                    var groundResult = await client.PostAsJsonAsync("http://localhost:53410/api/move", groundMove);
                }

                foreach (var aerialMove in aerialMoves)
                {
                    var aerialResult = await client.PostAsJsonAsync("http://localhost:53410/api/move", aerialMove);
                }

                foreach (var specialMove in specialMoves)
                {
                    var specialResult = await client.PostAsJsonAsync("http://localhost:53410/api/move", specialMove);
                }
            }
        }

        [Test]
        [Ignore("permanent")]
        public async Task UpdateCharacter()
        {
            var get = await client.GetAsync("http://localhost:53410/api/" + (int)Characters.BOWSER + "/character");

            CharacterDTO bowser = JsonConvert.DeserializeObject<CharacterDTO>(await get.Content.ReadAsStringAsync());

            bowser.Name = "NEWNAME";

            var result = await client.PutAsJsonAsync("http://localhost:53410/api/" + (int)Characters.BOWSER + "/character", bowser);
        }

    }
}

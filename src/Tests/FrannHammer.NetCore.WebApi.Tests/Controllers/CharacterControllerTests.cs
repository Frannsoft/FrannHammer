using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using FrannHammer.NetCore.WebApi.Models;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.Domain.Contracts;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static FrannHammer.Tests.Utility.Categories;

namespace FrannHammer.NetCore.WebApi.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTests : BaseControllerTests
    {
        private static IEnumerable<string> CharacterNamesWithSpacesRemoved()
        {
            yield return "rosalinaluma";
            yield return "littlemac";
            yield return "metaknight";
            yield return "captainfalcon";
            yield return "kingdedede";
            yield return "diddykong";
            yield return "donkeykong";
            yield return "wiifittrainer";
            yield return "toonlink";
            yield return "zerosuitsamus";
            yield return "mrgamewatch";
            yield return "miibrawler";
            yield return "miigunner";
            yield return "miiswordfighter";
            yield return "megaman";
            yield return "bowserjr";
            yield return "darkpit";
            yield return "duckhunt";
            yield return "drmario";
        }


        [Test]
        public void ThrowsArgumentNullExceptionForNullCharacterServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CharacterController(null, null);
            });
        }

        [Test]
        public async Task InvalidUrlThrowsResourceNotFoundException()
        {
            var response = await TestServer.GetAsync($"api/characters/ryu");

            Assert.That(response.StatusCode == HttpStatusCode.NotFound);

            string responseMessage = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).Message.ToString();
            Assert.That(responseMessage, Is.EqualTo($"Resource of type 'ICharacter' not found."));
        }

        [Test]
        [TestCase("api/characters/name/wolf")]
        [TestCase("api/characters/68")]
        public async Task CharacterInUltimateOnlyIsFoundWhenUltimateGameIsNotSpecifiedInQueryParameters(string endpoint)
        {
            var response = await TestServer.GetAsync(endpoint);

            Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());

            var character = JsonConvert.DeserializeObject<CharacterResource>(await response.Content.ReadAsStringAsync());

            Assert.That(character, Is.Not.Null);
            Assert.That(character.OwnerId, Is.EqualTo(CharacterIds.Wolf));
            Assert.DoesNotThrow(() => { var x = character.Related.Ultimate; });
            Assert.DoesNotThrow(() => { var x = character.Related.Ultimate.Moves; });
            Assert.That(character.Related.Ultimate.Moves.ToString(), Is.Not.Null);
        }

        [Test]
        [TestCase("api/characters/name/daisy/characterattributes")]
        [TestCase("api/characters/66/characterattributes")]
        public async Task MovementsForUltimateCharacterFoundWhenUltimateGameIsNotSpecifiedInQueryParameters(string endpoint)
        {
            var response = await TestServer.GetAsync(endpoint);

            Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());

            var moves = JsonConvert.DeserializeObject<List<MovementResource>>(await response.Content.ReadAsStringAsync());

            Assert.That(moves, Is.Not.Null);
            Assert.That(moves.Count, Is.GreaterThan(0));
            Assert.That(moves.TrueForAll(m => m.Game == Games.Ultimate));
        }

        [Test]
        [TestCase("api/characters/66/moves?expand=true")]
        [TestCase("api/characters/name/wolf/moves?expand=true")]
        public async Task ExpandedMovesForUltimateCharacterFoundWhenUltimateGameIsNotSpecifiedInQueryParameters(string endpoint)
        {
            var response = await TestServer.GetAsync(endpoint);

            Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());

            var moves = JsonConvert.DeserializeObject<List<dynamic>>(await response.Content.ReadAsStringAsync());

            Assert.That(moves, Is.Not.Null);
            Assert.That(moves.Count, Is.GreaterThan(0));
            Assert.That(moves.TrueForAll(m => m.Game == Games.Ultimate));
            Assert.DoesNotThrow(() => { var x = moves.First().HitboxActive.Frames; });
            Assert.DoesNotThrow(() => { var x = moves.First().HitboxActive.Adv; });
            Assert.DoesNotThrow(() => { var x = moves.First().HitboxActive.SD; });
            Assert.DoesNotThrow(() => { var x = moves.First().HitboxActive.ShieldstunMultiplier; });
            Assert.DoesNotThrow(() => { var x = moves.First().HitboxActive.RehitRate; });
            Assert.DoesNotThrow(() => { var x = moves.First().HitboxActive.FacingRestrict; });
            Assert.DoesNotThrow(() => { var x = moves.First().HitboxActive.SuperArmor; });

            Assert.DoesNotThrow(() => { var x = moves.First().BaseDamage.Normal; });
        }

        [Test]
        [TestCase("api/characters/66/moves")]
        [TestCase("api/characters/name/wolf/moves")]
        public async Task MovesForUltimateCharacterFoundWhenUltimateGameIsNotSpecifiedInQueryParameters(string endpoint)
        {
            var response = await TestServer.GetAsync(endpoint);

            Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());

            var moves = JsonConvert.DeserializeObject<List<MoveResource>>(await response.Content.ReadAsStringAsync());

            Assert.That(moves, Is.Not.Null);
            Assert.That(moves.Count, Is.GreaterThan(0));
            Assert.That(moves.TrueForAll(m => m.Game == Games.Ultimate));
        }

        [Test]
        [TestCase("api/characters/name/invalid")]
        [TestCase("api/characters/1235")]
        [TestCase("api/characters/name/invalid/moves")]
        public async Task InvalidUrlReturns404EvenAfterCheckingUltimateWhenGameIsNotSpecifiedInQueryParameters(string endpoint)
        {
            var response = await TestServer.GetAsync(endpoint);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            string responseMessage = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).Message.ToString();
            Assert.That(responseMessage, Is.EqualTo($"Resource of type 'ICharacter' not found."));
        }

        [Test]
        [Category(BackwardsCompatibility)]
        [TestCaseSource(nameof(CharacterNamesWithSpacesRemoved))]
        public void CharacterByName_CharacterWithSpacesRemovedInRouteValue_ReturnsCharacterData(string characterNameUnderTest)
        {
            var response = TestServer.GetAsync($"api/characters/name/{characterNameUnderTest}").Result;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"character: {characterNameUnderTest}");
        }

        [Test]
        [Category(BackwardsCompatibility)]
        [TestCaseSource(nameof(CharacterNamesWithSpacesRemoved))]
        public void CharacterByName_IsBackwardsCompatibleWithProduction_WhenCharacterWithSpacesIsCalled_ReturnsExpectedCharacterData(string characterNameUnderTest)
        {
            var betaResponse = TestServer.GetAsync($"api/characters/name/{characterNameUnderTest}").Result;

            Assert.That(betaResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            Character betaCharacter = betaResponse.Content.ReadAsAsync<Character>().Result;

            using (var productionClient = new HttpClient())
            {
                var prodResponse =
                    productionClient.GetAsync($"http://beta-api-kuroganehammer.azurewebsites.net/api/characters/name/{characterNameUnderTest}")
                        .Result;

                var kvpResponse = prodResponse.Content.ReadAsAsync<dynamic>().Result;

                Assert.That(betaCharacter.DisplayName.ToLower(), Is.EqualTo(kvpResponse.DisplayName.ToString().ToLower()));
                Assert.That(betaCharacter.Name.ToLower(), Is.EqualTo(kvpResponse.Name.ToString().ToLower()));
            }
        }
    }
}

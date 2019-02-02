using FrannHammer.Domain;
using FrannHammer.NetCore.WebApi.Controllers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
                // ReSharper disable once ObjectCreationAsStatement
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

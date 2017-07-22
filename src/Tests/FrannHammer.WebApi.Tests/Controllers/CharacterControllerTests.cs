using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using FrannHammer.Domain;
using FrannHammer.WebApi.Controllers;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using static FrannHammer.Tests.Utility.Categories;

namespace FrannHammer.WebApi.Tests.Controllers
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
                new CharacterController(null);
            });
        }

        [Test]
        [Category(BackwardsCompatibility)]
        [TestCaseSource(nameof(CharacterNamesWithSpacesRemoved))]
        public void CharacterByName_CharacterWithSpacesRemovedInRouteValue_ReturnsCharacterData(string characterNameUnderTest)
        {
            using (var testServer = TestServer.Create<Startup>())
            {
                var response = testServer.HttpClient.GetAsync($"api/characters/name/{characterNameUnderTest}").Result;

                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"character: {characterNameUnderTest}");
            }
        }

        [Test]
        [Category(BackwardsCompatibility)]
        [TestCaseSource(nameof(CharacterNamesWithSpacesRemoved))]
        public void CharacterByName_IsBackwardsCompatibleWithProduction_WhenCharacterWithSpacesIsCalled_ReturnsExpectedCharacterData(string characterNameUnderTest)
        {
            using (var betaServer = TestServer.Create<Startup>())
            {
                var betaResponse = betaServer.HttpClient.GetAsync($"api/characters/name/{characterNameUnderTest}").Result;

                Assert.That(betaResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                Character betaCharacter = betaResponse.Content.ReadAsAsync<Character>().Result;

                using (var productionClient = new HttpClient())
                {
                    var prodResponse =
                        productionClient.GetAsync($"http://api.kuroganehammer.com/api/characters/name/{characterNameUnderTest}")
                            .Result;

                    var kvpResponse = prodResponse.Content.ReadAsAsync<dynamic>().Result;

                    Assert.That(betaCharacter.DisplayName.ToLower(), Is.EqualTo(kvpResponse.displayName.ToString().ToLower()));
                    Assert.That(betaCharacter.Name.ToLower(), Is.EqualTo(kvpResponse.name.ToString().ToLower()));
                }
            }
        }
    }
}

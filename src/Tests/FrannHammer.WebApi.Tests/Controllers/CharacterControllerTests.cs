using FrannHammer.Domain;
using FrannHammer.NetCore.WebApi.Controllers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using static FrannHammer.Tests.Utility.Categories;

namespace FrannHammer.NetCore.WebApi.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTests : BaseControllerTests
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

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

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var _testServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>().UseEnvironment("development"));
            _httpClient = _testServer.CreateClient();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
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
        [Category(BackwardsCompatibility)]
        [TestCaseSource(nameof(CharacterNamesWithSpacesRemoved))]
        public void CharacterByName_CharacterWithSpacesRemovedInRouteValue_ReturnsCharacterData(string characterNameUnderTest)
        {
            var response = _httpClient.GetAsync($"api/characters/name/{characterNameUnderTest}").Result;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"character: {characterNameUnderTest}");
        }

        [Test]
        [Category(BackwardsCompatibility)]
        [TestCaseSource(nameof(CharacterNamesWithSpacesRemoved))]
        public void CharacterByName_IsBackwardsCompatibleWithProduction_WhenCharacterWithSpacesIsCalled_ReturnsExpectedCharacterData(string characterNameUnderTest)
        {
            var betaResponse = _httpClient.GetAsync($"api/characters/name/{characterNameUnderTest}").Result;

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

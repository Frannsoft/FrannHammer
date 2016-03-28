using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq;
using KuroganeHammer.Data.Core;
using KuroganeHammer.WebScraper;
using Kurogane.Data.RestApi.DTOs;
using Kurogane.Data.RestApi.Models;

namespace KurograneTransferDBTool
{
    public class Smoke : BaseTest
    {
        [Test]
        public void ShouldBeAbleToLoginWithBasicUser()
        {
            Assert.IsTrue(LoggedInBasicClient != null);
            Assert.That(LoggedInBasicClient.DefaultRequestHeaders.Authorization.Scheme.Length > 0);
        }

        [Test]
        public void ShouldBeAbleToLoginWithAdminUser()
        {
            Assert.That(LoggedInAdminClient != null);
            Assert.That(LoggedInAdminClient.DefaultRequestHeaders.Authorization.Scheme.Length > 0);
        }

        [Test]
        public void GetThumbnailDataFromKurogane()
        {
            var images = new HomePage("http://kuroganehammer.com/Smash4/")
            .GetThumbnailData();

            Assert.IsTrue(images.Count == 56);
        }

        [Test]
        public async Task GetAllSingleCharacters()
        {
            foreach (var character in Enum.GetValues(typeof(Characters)))
            {
                var getResult = await LoggedInBasicClient.GetAsync(Baseuri + "characters/" + (int)character);
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundCharacter = await getResult.Content.ReadAsAsync<CharacterDTO>();
                Assert.IsTrue(foundCharacter != null);
            }
        }

        [Test]
        public async Task GetAllCharacters()
        {
            var getResult = await LoggedInBasicClient.GetAsync(Baseuri + "characters");
            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Test]
        public async Task GetAllCharacterMovement()
        {
            foreach (var character in Enum.GetValues(typeof(Characters)))
            {
                var getResult = await LoggedInBasicClient.GetAsync(Baseuri + "characters/" + (int)character + "/movement");
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundMovement = await getResult.Content.ReadAsAsync<List<MovementStatDTO>>();
                Assert.IsTrue(foundMovement != null);
            }
        }

        [Test]
        public async Task GetAllCharacterMoves()
        {
            foreach (var character in Enum.GetValues(typeof(Characters)))
            {
                var getResult = await LoggedInBasicClient.GetAsync(Baseuri + "characters/" + (int)character + "/moves");
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundMoves = await getResult.Content.ReadAsAsync<List<MoveDTO>>();
                Assert.IsTrue(foundMoves != null);
            }
        }

        [Test]
        public async Task GetAllMovesOfType()
        {
            foreach (var moveType in Enum.GetValues(typeof(MoveType)))
            {
                var getResult = await LoggedInBasicClient.GetAsync(Baseuri + "movesoftype/" + (int)moveType);
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundMoves = await getResult.Content.ReadAsAsync<List<MoveDTO>>();
                Assert.IsTrue(foundMoves != null);
            }
        }

        [Test]
        public async Task GetMovesOfName()
        {
            var uri = Baseuri + "moves?name=" + WebUtility.HtmlEncode("Jab+1");
            var getResult = await LoggedInBasicClient.GetAsync(uri);
            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

            var foundMoves = await getResult.Content.ReadAsAsync<List<MoveDTO>>();
            Assert.IsTrue(foundMoves != null);
        }

        [Test]
        [Ignore("This test shouldn't rely on a hardcoded ID")]
        public async Task GetMoveById()
        {
            var getResult = await LoggedInBasicClient.GetAsync(Baseuri + "moves/" + 9562);
            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

            var move = await getResult.Content.ReadAsAsync<MoveDTO>();
            Assert.IsTrue(move != null);
        }

        [Test]
        [TestCase("http://kuroganehammer.com/Smash4/AirAcceleration")]
        [TestCase("http://kuroganehammer.com/Smash4/Airdodge")]
        [TestCase("http://kuroganehammer.com/Smash4/AirSpeed")]
        [TestCase("http://kuroganehammer.com/Smash4/FallSpeed")]
        [TestCase("http://kuroganehammer.com/Smash4/Gravity")]
        [TestCase("http://kuroganehammer.com/Smash4/ItemToss")]
        [TestCase("http://kuroganehammer.com/Smash4/ItemTossBack")]
        [TestCase("http://kuroganehammer.com/Smash4/ItemTossDash")]
        //[TestCase("http://kuroganehammer.com/Smash4/ItemTossDown")] - this url doesn't exist anymore
        [TestCase("http://kuroganehammer.com/Smash4/ItemTossForward")]
        [TestCase("http://kuroganehammer.com/Smash4/ItemTossUp")]
        [TestCase("http://kuroganehammer.com/Smash4/Jumpsquat")]
        [TestCase("http://kuroganehammer.com/Smash4/LedgeAttack")]
        [TestCase("http://kuroganehammer.com/Smash4/LedgeGetup")]
        [TestCase("http://kuroganehammer.com/Smash4/LedgeJump")]
        [TestCase("http://kuroganehammer.com/Smash4/LedgeRoll")]
        [TestCase("http://kuroganehammer.com/Smash4/Rolls")]
        [TestCase("http://kuroganehammer.com/Smash4/DashSpeed")]
        [TestCase("http://kuroganehammer.com/Smash4/Spotdodge")]
        [TestCase("http://kuroganehammer.com/Smash4/Tech")]
        [TestCase("http://kuroganehammer.com/Smash4/WalkSpeed")]
        [TestCase("http://kuroganehammer.com/Smash4/Weight")]
        public void CanIGetAttributesFromPage(string url)
        {
            var page = new Page(url);
            var attributes = page.GetAttributes();

            Assert.That(attributes != null);
            Assert.That(attributes.AttributeValues.Count > 0);
        }

        [Test]
        [TestCase(CharacterAttributes.AirAcceleration)]
        [TestCase(CharacterAttributes.Airdodge)]
        [TestCase(CharacterAttributes.AirSpeed)]
        [TestCase(CharacterAttributes.DashSpeed)]
        [TestCase(CharacterAttributes.FallSpeed)]
        [TestCase(CharacterAttributes.Gravity)]
        [TestCase(CharacterAttributes.ItemToss)]
        [TestCase(CharacterAttributes.Jumpsquat)]
        [TestCase(CharacterAttributes.LedgeAttack)]
        [TestCase(CharacterAttributes.LedgeGetup)]
        [TestCase(CharacterAttributes.LedgeRoll)]
        [TestCase(CharacterAttributes.Rolls)]
        [TestCase(CharacterAttributes.Spotdodge)]
        [TestCase(CharacterAttributes.WalkSpeed)]
        [TestCase(CharacterAttributes.Weight)]
        public async Task CanGetAllCharacterAttributesOfType(CharacterAttributes attributeType)
        {
            var url = Baseuri + "attributes?attributeType=" + attributeType;
            var getResponse = await LoggedInBasicClient.GetAsync(url);

            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);

            var attributes = await getResponse.Content.ReadAsAsync<List<CharacterAttributeDTO>>();

            Assert.That(attributes != null, "attributes is null");
            Assert.That(attributes.Count > 0, "There were no attributes found");
        }

        [Test]
        [TestCase(Characters.Bayonetta)]
        [TestCase(Characters.Bowser)]
        [TestCase(Characters.Bowserjr)]
        [TestCase(Characters.Captainfalcon)]
        [TestCase(Characters.Charizard)]
        [TestCase(Characters.Cloud)]
        [TestCase(Characters.Corrin)]
        [TestCase(Characters.Darkpit)]
        [TestCase(Characters.Diddykong)]
        [TestCase(Characters.Donkeykong)]
        [TestCase(Characters.Drmario)]
        [TestCase(Characters.Duckhunt)]
        [TestCase(Characters.Falco)]
        [TestCase(Characters.Fox)]
        [TestCase(Characters.Ganondorf)]
        [TestCase(Characters.Greninja)]
        [TestCase(Characters.Ike)]
        [TestCase(Characters.Jigglypuff)]
        [TestCase(Characters.Kingdedede)]
        [TestCase(Characters.Kirby)]
        [TestCase(Characters.Link)]
        [TestCase(Characters.Littlemac)]
        [TestCase(Characters.Lucario)]
        [TestCase(Characters.Lucas)]
        [TestCase(Characters.Lucina)]
        [TestCase(Characters.Luigi)]
        [TestCase(Characters.Mario)]
        [TestCase(Characters.Marth)]
        [TestCase(Characters.Megaman)]
        [TestCase(Characters.Metaknight)]
        [TestCase(Characters.Mewtwo)]
        [TestCase(Characters.Miibrawler)]
        [TestCase(Characters.Miigunner)]
        [TestCase(Characters.Miiswordfighter)]
        [TestCase(Characters.Mrgamewatch)]
        [TestCase(Characters.Ness)]
        [TestCase(Characters.Olimar)]
        [TestCase(Characters.Pacman)]
        [TestCase(Characters.Palutena)]
        [TestCase(Characters.Peach)]
        [TestCase(Characters.Pikachu)]
        [TestCase(Characters.Pit)]
        [TestCase(Characters.Rob)]
        [TestCase(Characters.Robin)]
        [TestCase(Characters.Rosalinaluma)]
        [TestCase(Characters.Roy)]
        [TestCase(Characters.Ryu)]
        [TestCase(Characters.Samus)]
        [TestCase(Characters.Sheik)]
        [TestCase(Characters.Shulk)]
        [TestCase(Characters.Sonic)]
        [TestCase(Characters.Toonlink)]
        [TestCase(Characters.Villager)]
        [TestCase(Characters.Wario)]
        [TestCase(Characters.Wiifittrainer)]
        [TestCase(Characters.Yoshi)]
        [TestCase(Characters.Zelda)]
        [TestCase(Characters.Zerosuitsamus)]
        public async Task CanGetAllCharacterAttributesOfCharacter(Characters characterType)
        {
            var url = Baseuri + "characters/" + (int)characterType + "/attributes";
            var getResponse = await LoggedInBasicClient.GetAsync(url);

            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);

            var attributes = await getResponse.Content.ReadAsAsync<List<CharacterAttributeDTO>>();

            Assert.That(attributes != null, "attributes is null");
            Assert.That(attributes.Count > 0, "There were no attributes found");
        }
    }
}

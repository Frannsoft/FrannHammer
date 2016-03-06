using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using KuroganeHammer.Data.Core;
using KuroganeHammer.WebScraper;
using Kurogane.Data.RestApi.DTOs;
using Kurogane.Data.RestApi.Models;

namespace KurograneTransferDBTool
{
    public class Smoke : BaseTest
    {
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
                var getResult = await AnonymousClient.GetAsync(Baseuri + "characters/" + (int)character);
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundCharacter = await getResult.Content.ReadAsAsync<CharacterDTO>();
                Assert.IsTrue(foundCharacter != null);
            }
        }

        [Test]
        public async Task GetAllCharacters()
        {
            var getResult = await AnonymousClient.GetAsync(Baseuri + "characters");
            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Test]
        public async Task GetAllCharacterMovement()
        {
            foreach (var character in Enum.GetValues(typeof(Characters)))
            {
                var getResult = await AnonymousClient.GetAsync(Baseuri + "characters/" + (int)character + "/movement");
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
                var getResult = await AnonymousClient.GetAsync(Baseuri + "characters/" + (int)character + "/moves");
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
                var getResult = await AnonymousClient.GetAsync(Baseuri + "movesoftype/" + (int)moveType);
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundMoves = await getResult.Content.ReadAsAsync<List<MoveDTO>>();
                Assert.IsTrue(foundMoves != null);
            }
        }

        [Test]
        public async Task GetMovesOfName()
        {
            var uri = Baseuri + "moves?name=" + WebUtility.HtmlEncode("Jab+1");
            var getResult = await AnonymousClient.GetAsync(uri);
            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

            var foundMoves = await getResult.Content.ReadAsAsync<List<MoveDTO>>();
            Assert.IsTrue(foundMoves != null);
        }

        [Test]
        public async Task GetMoveById()
        {
            var getResult = await AnonymousClient.GetAsync(Baseuri + "moves/" + 9575);
            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

            var move = await getResult.Content.ReadAsAsync<MoveDTO>();
            Assert.IsTrue(move != null);
        }
    }
}

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Smoke
{
    public class CharacterSmokeTest : BaseSmokeTest
    {
        [Test]
        public async Task GetAllSingleCharactersById()
        {
            var characters = await LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute).Result.Content.ReadAsAsync<List<Character>>();

            foreach (var character in characters)
            {
                var getResult = await LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute + "/" + character.Id);
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundCharacter = await getResult.Content.ReadAsAsync<Character>();
                Assert.IsTrue(foundCharacter != null);
            }
        }

        [Test]
        public async Task GetAllSingleCharactersByName()
        {
            var characters =
                await
                    LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute)
                        .Result.Content.ReadAsAsync<List<Character>>();

            characters.ForEach(async c =>
            {
                var getResult = await LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute + "/name/" + c.Name);
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundCharacter = await getResult.Content.ReadAsAsync<Character>();
                Assert.IsTrue(foundCharacter != null);
            });
        }

        [Test]
        public async Task GetAllCharacters()
        {
            var getResult = await LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute);

            var characters = getResult.Content.ReadAsAsync<List<Character>>().Result;
            CollectionAssert.AllItemsAreUnique(characters);
            CollectionAssert.AllItemsAreNotNull(characters);
        }

        [Test]
        public async Task GetAllCharacterMovement()
        {
            var characters = await LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute).Result.Content.ReadAsAsync<List<Character>>();

            foreach (var character in characters)
            {
                var getResult = await LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute + "/" + character.Id + "/" + MovementsRoute);
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundMovement = await getResult.Content.ReadAsAsync<List<Movement>>();
                Assert.IsTrue(foundMovement != null);
            }
        }

        [Test]
        public async Task GetAllCharacterMoves()
        {
            var characters = await LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute).Result.Content.ReadAsAsync<List<Character>>();

            foreach (var character in characters)
            {
                var getResult = await LoggedInBasicClient.GetAsync(Baseuri + CharactersRoute + "/" + character.Id + "/" + MovesRoute);
                Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);

                var foundMoves = await getResult.Content.ReadAsAsync<List<Move>>();
                CollectionAssert.AllItemsAreUnique(foundMoves);
            }
        }
    }
}

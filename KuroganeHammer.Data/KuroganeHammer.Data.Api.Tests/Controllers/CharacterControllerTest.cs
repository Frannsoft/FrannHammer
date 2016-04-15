using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using NUnit.Framework;
using KuroganeHammer.Data.Api.Models;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTest : BaseControllerTest
    {
        private const string BaseUri = "/api/characters";

        [Test]
        public void ShouldGetCharacter()
        {
            var character = TestObjects.Character();
            CharactersController.PostCharacter(character);

            var result = CharactersController.GetCharacter(character.Id) as OkNegotiatedContentResult<Character>;

            Assert.That(result?.Content, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllCharacters()
        {
            var newCharacter = TestObjects.Character();
            CharactersController.PostCharacter(newCharacter);

            var results = CharactersController.GetCharacters();

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetAllMovementsForCharacter()
        {
            var newCharacter = TestObjects.Character();
            CharactersController.PostCharacter(newCharacter);

            var movement = TestObjects.Movement();
            MovementsController.PostMovement(movement);

            var results = CharactersController.GetMovementsForCharacter(newCharacter.Id);

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().Name, Is.EqualTo("jab 1"));
        }

        [Test]
        public void ShouldGetAllMovesForCharacter()
        {
            var newCharacter = TestObjects.Character();

            var move = TestObjects.Move();
            MovesController.PostMove(move);

            CharactersController.PostCharacter(newCharacter);

            var results = CharactersController.GetMovesForCharacter(newCharacter.Id);

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().Name, Is.EqualTo("falco phantasm"));
        }

        [Test]
        public void ShouldAddCharacter()
        {
            var newCharacter = TestObjects.Character();
            var result = CharactersController.PostCharacter(newCharacter) as CreatedAtRouteNegotiatedContentResult<Character>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Content, Is.Not.Null);
            Assert.AreEqual(newCharacter, result?.Content);
        }

        [Test]
        public void ShouldUpdateCharacter()
        {
            const string expectedName = "mewtwo";
            var character = TestObjects.Character();

            var dateTime = DateTime.Now;

            //arrange
            var returnedCharacter =
                CharactersController.PostCharacter(character) as CreatedAtRouteNegotiatedContentResult<Character>;
            //act
            if (returnedCharacter != null)
            {
                returnedCharacter.Content.Name = expectedName;
                CharactersController.PutCharacter(returnedCharacter.Content.Id, returnedCharacter.Content);
            }

            var updatedCharacter = CharactersController.GetCharacter(character.Id) as OkNegotiatedContentResult<Character>;

            //assert
            Assert.That(updatedCharacter?.Content.Name, Is.EqualTo(expectedName));
            Assert.That(updatedCharacter?.Content.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteCharacter()
        {
            var character = TestObjects.Character();
            CharactersController.PostCharacter(character);

            CharactersController.DeleteCharacter(character.Id);

            var result = CharactersController.GetCharacter(character.Id) as NotFoundResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        [TestCase(BaseUri)]
        [TestCase(BaseUri + "/2")]
        [TestCase(BaseUri + "/4/movements")]
        [TestCase(BaseUri + "/10/moves")]
        public async Task ShouldGetUnauthorizedWithoutLogin_GET(string uri)
        {
            var response = await GetAsync(uri);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri)]
        public async Task ShouldGetUnauthorizedWithoutLogin_POST(string uri)
        {
            var character = TestObjects.Character();

            var response = await PostAsync(uri, character);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri + "/4")]
        public async Task ShouldGetUnauthorizedWithoutLogin_PUT(string uri)
        {
            var character = TestObjects.Character();

            var response = await PutAsync(uri, character);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri + "/1")]
        public async Task ShouldGetUnauthorizedWithoutLogin_DELETE(string uri)
        {
            var response = await DeleteAsync(uri);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}

using System;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.Characters
{
    /// <summary>
    /// TODO: These tests will soon become obsolete as they are built on an empty Effort DB.  The tests in CharacterTests will replace them
    /// TODO: as those tests are built with the prepopulated Effort db.
    /// </summary>
    [TestFixture]
    [Obsolete("Replaced with CharacterTests tests")]
    public class CharacterControllerTest : BaseControllerTest
    {

        [Test]
        public void ShouldGetCharacterById()
        {
            var character = TestObjects.Character();
            CharactersController.PostCharacter(character);

            var result = CharactersController.GetCharacter(character.Id) as OkNegotiatedContentResult<Character>;

            Assert.That(result?.Content, Is.Not.Null);
        }

        [Test]
        public void ShouldGetCharacterByName()
        {
            const string expected = "falco";

            var character = TestObjects.Character();
            CharactersController.PostCharacter(character);

            var result = CharactersController.GetCharacterByName(expected) as OkNegotiatedContentResult<Character>;
            Assert.That(result?.Content, Is.Not.Null);
            Assert.That(result?.Content.Name, Is.EqualTo(expected));
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

    }
}

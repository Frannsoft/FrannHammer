using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTests : EffortBaseTest
    {
        private CharactersController _controller;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new CharactersController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void CanGetCharacterById()
        {
            var character = ExecuteAndReturnContent<CharacterDto>(() => _controller.GetCharacter(1));
            Assert.That(character, Is.Not.Null);
        }

        [Test]
        public void CanGetCharacterByName()
        {
            const string expectedName = "Pikachu";
            var character = ExecuteAndReturnContent<CharacterDto>(() => _controller.GetCharacterByName(expectedName));

            Assert.That(character, Is.Not.Null);
            Assert.That(character.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void NotFoundResultWhenNoCharacterFoundById()
        {
            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacter(0));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void NotFoundResultWhenNoCharacterFoundByName()
        {
            const string expectedName = "dummyvalue";
            var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacterByName(expectedName));

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void BadRequestReturned_WhenEmptyNameForFoundByName()
        {
            ExecuteAndReturn<BadRequestErrorMessageResult>(() => _controller.GetCharacterByName(string.Empty));
        }

        [Test]
        public void BadRequestReturned_WhenNullNameForFoundByName()
        {
            ExecuteAndReturn<BadRequestErrorMessageResult>(() => _controller.GetCharacterByName(null));
        }

        [Test]
        public void ShouldGetAllCharacterThrows()
        {
            var throws = _controller.GetThrowsForCharacter(1);

            Assert.That(throws, Is.Not.Empty);
            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(ThrowDto));
        }

        [Test]
        public void ShouldGetAllCharacters()
        {
            var characters = _controller.GetCharacters();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.AllItemsAreUnique(characters);
            CollectionAssert.AllItemsAreInstancesOfType(characters, typeof(CharacterDto));
        }

        [Test]
        public void ShouldGetAllMovementsForCharacter()
        {
            var movements = _controller.GetMovementsForCharacter(1);
            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.AllItemsAreUnique(movements);
            CollectionAssert.AllItemsAreInstancesOfType(movements, typeof(MovementDto));
        }

        [Test]
        public void ShouldGetAllMovesForCharacter()
        {
            var moves = _controller.GetMovesForCharacter(1);
            CollectionAssert.AllItemsAreNotNull(moves);
            CollectionAssert.AllItemsAreUnique(moves);
            CollectionAssert.AllItemsAreInstancesOfType(moves, typeof(MoveDto));
        }

        [Test]
        public void ShouldAddCharacter()
        {
            var newCharacter = TestObjects.Character();
            var result = ExecuteAndReturnCreatedAtRouteContent<CharacterDto>(() => _controller.PostCharacter(newCharacter));

            var latestCharacter = _controller.GetCharacters().ToList().Last();
            Assert.AreEqual(result, latestCharacter);
        }

        [Test]
        public void ShouldUpdateCharacter()
        {
            const string expectedName = "mewtwo";
            var character = _controller.GetCharacters().First();

            //act
            if (character != null)
            {
                character.Name = expectedName;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutCharacter(character.Id, character));
            }

            var updatedCharacter = ExecuteAndReturnContent<CharacterDto>(() => _controller.GetCharacter(character.Id));

            //assert
            Assert.That(updatedCharacter.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void ShouldDeleteCharacter()
        {
            var character = TestObjects.Character();
            ExecuteAndReturnCreatedAtRouteContent<CharacterDto>(() => _controller.PostCharacter(character));
            _controller.DeleteCharacter(character.Id);
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacter(character.Id));
        }
    }
}

using System.Collections.Generic;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;
using FrannHammer.Services;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTests : EffortBaseTest
    {
        private CharactersController _controller;
        private IMetadataService _service;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _service = new MetadataService(Context);
            _controller = new CharactersController(_service);
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
            var character = ExecuteAndReturnContent<dynamic>(() => _controller.GetCharacterByName(expectedName));

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
            var throws = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetThrowsForCharacter(1))
                .ToList();

            Assert.That(throws, Is.Not.Empty);
            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
            CollectionAssert.AllItemsAreInstancesOfType(throws, typeof(ThrowDto));
        }

        [Test]
        public void ShouldGetAllCharacters()
        {
            var characters = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacters())
                .ToList();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.AllItemsAreUnique(characters);
        }

        [Test]
        public void ShouldGetAllMovementsForCharacter()
        {
            var movements = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetMovementsForCharacter(1))
                .ToList();
            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.AllItemsAreUnique(movements);
        }

        [Test]
        public void ShouldGetAllMovesForCharacter()
        {
            var moves = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetMovesForCharacter(1))
                .ToList();
            CollectionAssert.AllItemsAreNotNull(moves);
            CollectionAssert.AllItemsAreUnique(moves);
            CollectionAssert.AllItemsAreInstancesOfType(moves, typeof(MoveDto));
        }

        [Test]
        public void ShouldAddCharacter()
        {
            var newCharacter = TestObjects.Character();
            var result = ExecuteAndReturnCreatedAtRouteContent<CharacterDto>(() => _controller.PostCharacter(newCharacter));

            var latestCharacter = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacters()).ToList().Last();
            Assert.AreEqual(result, latestCharacter);
        }

        [Test]
        public void ShouldUpdateCharacter()
        {
            const string expectedName = "mewtwo";
            var character = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacters()).First();

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

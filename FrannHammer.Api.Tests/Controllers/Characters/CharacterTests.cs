using System;
using System.Threading;
using System.Web.Http.Results;
using NUnit.Framework;
using FrannHammer.Api.Controllers;
using FrannHammer.Core.Models;
using FrannHammer.Api.DTOs;

namespace FrannHammer.Api.Tests.Controllers.Characters
{
    [TestFixture]
    public class CharacterTests : EffortBaseTest
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
            var character = ExecuteAndReturnContent<Character>(() => _controller.GetCharacter(1));
            Assert.That(character, Is.Not.Null);
        }

        [Test]
        public void CanGetCharacterByName()
        {
            const string expectedName = "Pikachu";
            var character = ExecuteAndReturnContent<Character>(() => _controller.GetCharacterByName(expectedName));

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
        public void ShouldGetAllCharacters()
        {
            var characters = _controller.GetCharacters();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.AllItemsAreUnique(characters);
            CollectionAssert.AllItemsAreInstancesOfType(characters, typeof(Character));
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
            var result = ExecuteAndReturnCreatedAtRouteContent<Character>(() => _controller.PostCharacter(newCharacter));

            Assert.AreEqual(newCharacter, result);
        }

        [Test]
        public void ShouldUpdateCharacter()
        {
            const string expectedName = "mewtwo";
            var character = TestObjects.Character();

            var dateTime = DateTime.Now;
            Thread.Sleep(100);
            //arrange
            var returnedCharacter =
                ExecuteAndReturnCreatedAtRouteContent<Character>(() => _controller.PostCharacter(character));
            //act
            if (returnedCharacter != null)
            {
                returnedCharacter.Name = expectedName;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutCharacter(returnedCharacter.Id, returnedCharacter));
            }

            var updatedCharacter = ExecuteAndReturnContent<Character>(() => _controller.GetCharacter(character.Id));

            //assert
            Assert.That(updatedCharacter.Name, Is.EqualTo(expectedName));
            Assert.That(updatedCharacter.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteCharacter()
        {
            var character = TestObjects.Character();
            ExecuteAndReturnCreatedAtRouteContent<Character>(() => _controller.PostCharacter(character));
            ExecuteAndReturnContent<Character>(() => _controller.DeleteCharacter(character.Id));
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacter(character.Id));
        }
    }
}

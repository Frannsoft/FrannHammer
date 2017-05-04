using System.Collections.Generic;
using FrannHammer.DataAccess.MongoDb;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.WebApi.Controllers;

namespace FrannHammer.WebApi.MongoDb.Integration.Tests
{
    [TestFixture]
    public class CharacterControllerTests : BaseControllerTests
    {
        private CharacterController _controller;

        public CharacterControllerTests()
            : base(typeof(Character))
        { }

        [SetUp]
        public void SetUp()
        {
            _controller = new CharacterController(new DefaultCharacterService(new MongoDbRepository<ICharacter>(MongoDatabase)));
        }

        private static void AssertCharacterIsValid(ICharacter character)
        {
            Assert.That(character, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Null);
            Assert.That(character.DisplayName, Is.Not.Null);
            Assert.That(character.ColorTheme, Is.Not.Null);
            //Assert.That(character.Description, Is.Not.Null);
            Assert.That(character.DisplayName, Is.Not.Null);
            Assert.That(character.FullUrl, Is.Not.Null);
            Assert.That(character.Id, Is.Not.Null);
            Assert.That(character.MainImageUrl, Is.Not.Null);
            Assert.That(character.Name, Is.Not.Null);
            //Assert.That(character.Style, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Null);
        }

        [Test]
        public void GetSingleCharacter()
        {
            var response = _controller.Get("5905f99f4696591ea4062d06") as OkNegotiatedContentResult<ICharacter>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var character = response.Content;
            AssertCharacterIsValid(character);
        }

        [Test]
        public void GetAllCharacters()
        {
            var response = _controller.GetAll() as OkNegotiatedContentResult<IEnumerable<ICharacter>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var characters = response.Content.ToList();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.IsNotEmpty(characters);
            CollectionAssert.AllItemsAreUnique(characters);
            characters.ForEach(AssertCharacterIsValid);
        }
    }
}

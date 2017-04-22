using System.Collections.Generic;
using System.Reflection;
using FrannHammer.DataAccess.MongoDb;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NUnit.Framework;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.WebApi.Controllers;

namespace FrannHammer.WebApi.MongoDb.Integration.Tests
{
    [TestFixture]
    public class CharacterControllerTests
    {
        private IMongoDatabase _mongoDatabase;
        private CharacterController _controller;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var classMap = new BsonClassMap(typeof(Character));
            var characterProperties = typeof(Character).GetProperties().Where(p => p.GetCustomAttribute<FriendlyNameAttribute>() != null);

            foreach (var prop in characterProperties)
            {
                classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
            }

            BsonClassMap.RegisterClassMap(classMap);

            var mongoClient = new MongoClient(new MongoUrl("mongodb://testuser:password@ds058739.mlab.com:58739/testfranndotexe"));
            _mongoDatabase = mongoClient.GetDatabase("testfranndotexe");

            _controller = new CharacterController(new DefaultCharacterService(new MongoDbRepository<ICharacter>(_mongoDatabase)));
        }

        private static void AssertCharacterIsValid(ICharacter character)
        {
            Assert.That(character, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Null);
            Assert.That(character.DisplayName, Is.Not.Null);
            Assert.That(character.ColorTheme, Is.Not.Null);
            Assert.That(character.Description, Is.Not.Null);
            Assert.That(character.DisplayName, Is.Not.Null);
            Assert.That(character.FullUrl, Is.Not.Null);
            Assert.That(character.Id, Is.GreaterThan(0));
            Assert.That(character.MainImageUrl, Is.Not.Null);
            Assert.That(character.Name, Is.Not.Null);
            Assert.That(character.Style, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Null);
        }

        [Test]
        public void GetSingleCharacter()
        {
            var response = _controller.GetCharacter(1) as OkNegotiatedContentResult<ICharacter>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var character = response.Content;
            AssertCharacterIsValid(character);
        }

        [Test]
        public void GetAllCharacters()
        {
            var response = _controller.GetAllCharacters() as OkNegotiatedContentResult<IEnumerable<ICharacter>>;

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

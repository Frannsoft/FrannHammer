using System.Linq;
using System.Reflection;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NUnit.Framework;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture]
    public class CharacterMetadataTests
    {
        private IRepository<ICharacter> _repository;
        private IMongoDatabase _mongoDatabase;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var classMap = new BsonClassMap(typeof(Character));
            var characterProperties = typeof(Character).GetProperties(BindingFlags.Public).Where(p => p.GetCustomAttribute<FriendlyNameAttribute>() != null);

            foreach (var prop in characterProperties)
            {
                classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
            }

            BsonClassMap.RegisterClassMap(classMap);

            var mongoClient = new MongoClient(new MongoUrl("mongodb://testuser:password@ds058739.mlab.com:58739/testfranndotexe"));
            _mongoDatabase = mongoClient.GetDatabase("testfranndotexe");
        }

        [Test]
        public void GetSingleCharacter()
        {
            _repository = new MongoDbRepository<ICharacter>(_mongoDatabase);

            var character = _repository.Get(1);
            Assert.That(character, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.Not.Empty);
        }

        [Test]
        public void GetAllCharacters()
        {
            _repository = new MongoDbRepository<ICharacter>(_mongoDatabase);

            var characters = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.IsNotEmpty(characters);
            CollectionAssert.AllItemsAreUnique(characters);
        }
    }
}

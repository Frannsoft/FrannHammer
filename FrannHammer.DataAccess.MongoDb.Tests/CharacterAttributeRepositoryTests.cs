using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture]
    public class CharacterAttributeRepositoryTests : BaseRepositoryTests
    {
        private IRepository<IAttribute> _repository;

        public CharacterAttributeRepositoryTests()
            : base(typeof(CharacterAttribute))
        { }

        [Test]
        public void GetSingleCharacterAttribute()
        {
            _repository = new MongoDbRepository<IAttribute>(MongoDatabase);

            var newlyAddedCharacterAttribute = _repository.Add(new CharacterAttribute
            {
                Name = "one",
                Owner = "me",
                Value = "test"
            });
            var characterAttribute = _repository.Get(newlyAddedCharacterAttribute.Id);

            Assert.That(characterAttribute, Is.Not.Null);
            Assert.That(characterAttribute.Id, Is.Not.Null);
            Assert.That(characterAttribute.Name, Is.Not.Null);
            Assert.That(characterAttribute.Owner, Is.Not.Null);
            Assert.That(characterAttribute.Value, Is.Not.Null);
        }

        [Test]
        public void GetAllCharacterAttributes()
        {
            _repository = new MongoDbRepository<IAttribute>(MongoDatabase);

            _repository.Add(new CharacterAttribute { Name = "one" });
            _repository.Add(new CharacterAttribute { Name = "two" });

            var characterAttributes = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(characterAttributes);
            CollectionAssert.IsNotEmpty(characterAttributes);
            CollectionAssert.AllItemsAreUnique(characterAttributes);
        }

        [Test]
        public void AddAndRemoveSingleCharacterAttribute()
        {
            _repository = new MongoDbRepository<IAttribute>(MongoDatabase);

            int previousCount = _repository.GetAll().Count();

            var newCharacterAttribute = new CharacterAttribute
            {
                Id = "99999",
                Name = "test"
            };

            _repository.Add(newCharacterAttribute);

            int newCount = _repository.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(newCharacterAttribute.Id);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture]
    public class CharacterAttributeRepositoryTests : BaseRepositoryTests
    {
        private IRepository<IAttribute> _repository;

        public CharacterAttributeRepositoryTests()
            : base(typeof(CharacterAttribute))
        { }

        [SetUp]
        public void SetUp()
        {
            Fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IAttribute),
                    typeof(CharacterAttribute)));
        }

        [Test]
        public void GetSingleCharacterAttribute()
        {
            _repository = new MongoDbRepository<IAttribute>(MongoDatabase);

            var newlyAddedCharacterAttribute = _repository.Add(Fixture.Create<CharacterAttribute>());
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

            _repository.Add(Fixture.Create<CharacterAttribute>());
            _repository.Add(Fixture.Create<CharacterAttribute>());

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

            var newCharacterAttribute = Fixture.Create<CharacterAttribute>();

            _repository.Add(newCharacterAttribute);

            int newCount = _repository.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(newCharacterAttribute.Id);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

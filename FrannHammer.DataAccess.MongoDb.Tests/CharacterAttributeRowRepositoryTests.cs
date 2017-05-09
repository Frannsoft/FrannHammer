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
    public class CharacterAttributeRowRepositoryTests : BaseRepositoryTests
    {
        private IRepository<ICharacterAttributeRow> _repository;

        public CharacterAttributeRowRepositoryTests()
            : base(typeof(CharacterAttribute), typeof(CharacterAttributeRow))
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
        public void GetSingleCharacterAttributeRow()
        {
            _repository = new MongoDbRepository<ICharacterAttributeRow>(MongoDatabase);

            var newlyAddedCharacterAttribute = _repository.Add(Fixture.Create<CharacterAttributeRow>());
            var characterAttributeRow = _repository.GetById(newlyAddedCharacterAttribute.Id);

            Assert.That(characterAttributeRow, Is.Not.Null);
            Assert.That(characterAttributeRow.Id, Is.Not.Null);
            Assert.That(characterAttributeRow.Name, Is.Not.Null);

            foreach (var attribute in characterAttributeRow.Values)
            {
                Assert.That(attribute.Owner, Is.Not.Null);
                Assert.That(attribute.Value, Is.Not.Null);
            }
        }

        [Test]
        public void GetAllCharacterAttributeRows()
        {
            _repository = new MongoDbRepository<ICharacterAttributeRow>(MongoDatabase);

            _repository.Add(Fixture.Create<CharacterAttributeRow>());
            _repository.Add(Fixture.Create<CharacterAttributeRow>());

            var characterAttributes = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(characterAttributes);
            CollectionAssert.IsNotEmpty(characterAttributes);
            CollectionAssert.AllItemsAreUnique(characterAttributes);
        }

        [Test]
        public void AddAndRemoveSingleCharacterAttributeRow()
        {
            _repository = new MongoDbRepository<ICharacterAttributeRow>(MongoDatabase);

            int previousCount = _repository.GetAll().Count();

            var newCharacterAttribute = Fixture.Create<CharacterAttributeRow>();

            _repository.Add(newCharacterAttribute);

            int newCount = _repository.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(newCharacterAttribute.Id);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    //TODO - this is a candidate for generic test fixture attribute
    [TestFixture]
    public class CharacterRepositoryTests : BaseRepositoryTests
    {
        private IRepository<ICharacter> _repository;

        public CharacterRepositoryTests()
            : base(typeof(Character))
        { }

        [SetUp]
        public void SetUp()
        {
            Fixture.Customizations.Add(
                new TypeRelay(
                    typeof(ICharacter),
                    typeof(Character)));
        }

        [Test]
        public void GetSingleCharacter()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            var newlyAddedCharacter = _repository.Add(Fixture.Create<ICharacter>());

            var character = _repository.Get(newlyAddedCharacter.Id);
            Assert.That(character, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.Not.Empty);
        }

        [Test]
        public void GetAllCharacters()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            _repository.Add(Fixture.Create<ICharacter>());
            _repository.Add(Fixture.Create<ICharacter>());

            var characters = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.IsNotEmpty(characters);
            CollectionAssert.AllItemsAreUnique(characters);
        }

        [Test]
        public void AddAndRemoveSingleCharacter()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            int previousCount = _repository.GetAll().Count();

            var newCharacter = Fixture.Create<ICharacter>();

            var newlyAddedCharacter = _repository.Add(newCharacter);

            var allCharacters = _repository.GetAll().ToList();
            int newCount = allCharacters.Count;

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(allCharacters.Last().Id);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

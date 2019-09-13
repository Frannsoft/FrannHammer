using AutoFixture;
using AutoFixture.Kernel;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
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
        public void GetSingleCharacterById()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            var newlyAddedCharacter = _repository.Add(Fixture.Create<ICharacter>());

            var character = _repository.GetSingleWhere(c => c.InstanceId == newlyAddedCharacter.InstanceId);
            Assert.That(character, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.Not.Empty);
        }

        [Test]
        public void GetSingleCharacterByName()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            var newlyAddedCharacter = _repository.Add(Fixture.Create<ICharacter>());

            var character = _repository.GetSingleWhere(c => c.Name == newlyAddedCharacter.Name);
            Assert.That(character, Is.Not.Null, $"{nameof(character)}");
            Assert.That(character.InstanceId, Is.EqualTo(newlyAddedCharacter.InstanceId), $"{nameof(character.InstanceId)}");
            Assert.That(character.Name, Is.EqualTo(newlyAddedCharacter.Name), $"{nameof(character.Name)}");
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

            _repository.Add(newCharacter);

            var allCharacters = _repository.GetAll().ToList();
            int newCount = allCharacters.Count;

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(allCharacters.Last().InstanceId);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

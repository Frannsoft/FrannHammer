using System;
using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture]
    public class CharacterRepositoryTests : BaseRepositoryTests
    {
        private IRepository<ICharacter> _repository;

        protected override Type ModelType => typeof(Character);

        [Test]
        public void GetSingleCharacter()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            var character = _repository.Get(1);
            Assert.That(character, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.Not.Empty);
        }

        [Test]
        public void GetAllCharacters()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            var characters = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.IsNotEmpty(characters);
            CollectionAssert.AllItemsAreUnique(characters);
        }

        //TODO - uses real mongodb to test add/delete.  Now do this for other model types
        [Test]
        public void AddAndRemoveSingleCharacter()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            int previousCount = _repository.GetAll().Count();

            var newCharacter = new Character
            {
                Id = 999,
                Name = "test"
            };

            _repository.Add(newCharacter);

            int newCount = _repository.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(newCharacter);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

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
    }
}

﻿using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    //TODO - this is a candidate for generic test fixture attribute
    //not really.  Generic test fixtures support is sketchy at best it seems.  
    //Plus, there's not really a clean way to add a type for character attribute row's Values property in the generic
    //type specifiers.  Refactoring these tests to call more generic base methods where possible should
    //alleviate most of the pain.
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

            var character = _repository.GetById(newlyAddedCharacter.Id);
            Assert.That(character, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.Not.Empty);
        }

        [Test]
        public void GetSingleCharacterByName()
        {
            _repository = new MongoDbRepository<ICharacter>(MongoDatabase);

            var newlyAddedCharacter = _repository.Add(Fixture.Create<ICharacter>());

            var character = _repository.GetByName(newlyAddedCharacter.Name);
            Assert.That(character, Is.Not.Null, $"{nameof(character)}");
            Assert.That(character.Id, Is.EqualTo(newlyAddedCharacter.Id), $"{nameof(character.Id)}");
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

            _repository.Delete(allCharacters.Last().Id);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class CharacterServiceTests
    {
        [Test]
        public void AddSingleCharacter()
        {
            var fakeCharacters = new List<ICharacter>
            {
                new Character {Name = "one"}
            };

            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.GetAll()).Returns(() => fakeCharacters);
            characterRepositoryMock.Setup(c => c.Add(It.IsAny<ICharacter>())).Callback<ICharacter>(c =>
            {
                fakeCharacters.Add(c);
            });
            var service = new DefaultCharacterService(characterRepositoryMock.Object);

            int previousCount = service.GetAll().Count();

            var newCharacter = new Character
            {
                Id = "999",
                Name = "two"
            };
            service.Add(newCharacter);

            int newCount = service.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));
        }

        [Test]
        public void ReturnsNullForNoCharacterFoundById()
        {
            var fakeCharacters = new List<ICharacter>
            {
                new Character
                {
                    Id = "1",
                    Name = "one"
                }
            };

            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.Get(It.IsAny<int>())).Returns<int>(id => fakeCharacters.FirstOrDefault(c => c.Id == id.ToString()));

            var service = new DefaultCharacterService(characterRepositoryMock.Object);

            var character = service.Get(0);

            Assert.That(character, Is.Null);
        }

        [Test]
        public void Error_RejectsNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultCharacterService(null);
            });
        }

        [Test]
        public void Error_RejectsNullCharacterForAddition()
        {
            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var service = new DefaultCharacterService(characterRepositoryMock.Object);

                service.Add(null);
            });
        }
    }
}

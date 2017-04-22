﻿using System;
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
    public class CharacterAttributeServiceTests
    {
        [Test]
        public void AddSingleCharacterAttribute()
        {
            var fakeCharacterAttributes = new List<IAttribute>
            {
                new CharacterAttribute {Name = "one"}
            };

            var characterAttributeRepositoryMock = new Mock<IRepository<IAttribute>>();
            characterAttributeRepositoryMock.Setup(c => c.GetAll()).Returns(() => fakeCharacterAttributes);
            characterAttributeRepositoryMock.Setup(c => c.Add(It.IsAny<IAttribute>())).Callback<IAttribute>(c =>
            {
                fakeCharacterAttributes.Add(c);
            });
            var service = new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object);

            int previousCount = service.GetAll().Count();

            var newCharacterAttribute = new CharacterAttribute
            {
                Id = 999,
                Name = "two"
            };
            service.Add(newCharacterAttribute);

            int newCount = service.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));
        }

        [Test]
        public void ReturnsNullForNoCharacterAttributeFoundById()
        {
            var fakeCharacterAttributes = new List<IAttribute>
            {
                new CharacterAttribute
                {
                    Id = 1,
                    Name = "one"
                }
            };

            var characterAttributeRepositoryMock = new Mock<IRepository<IAttribute>>();
            characterAttributeRepositoryMock.Setup(c => c.Get(It.IsAny<int>())).Returns<int>(id => fakeCharacterAttributes.FirstOrDefault(c => c.Id == id));

            var service = new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object);

            var characterAttribute = service.Get(0);

            Assert.That(characterAttribute, Is.Null);
        }

        [Test]
        public void Error_RejectsNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultCharacterAttributeService(null);
            });
        }

        [Test]
        public void Error_RejectsNullCharacterAttributeForAddition()
        {
            var characterAttributeRepositoryMock = new Mock<IRepository<IAttribute>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var service = new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object);

                service.Add(null);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class CharacterAttributeServiceTests : BaseServiceTests
    {
        [SetUp]
        public override void SetUp()
        {
            Fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IAttribute),
                    typeof(CharacterAttribute)));

            Fixture.Customizations.Add(
                new TypeRelay(
                    typeof(ICharacterAttributeRow),
                    typeof(DefaultCharacterAttributeRow)));
        }

        [Test]
        public void AddSingleCharacterAttribute()
        {
            var characterAttributeRows = Fixture.CreateMany<ICharacterAttributeRow>();

            var fakeCharacterAttributes = new List<ICharacterAttributeRow>(characterAttributeRows);

            var characterAttributeRepositoryMock = new Mock<IRepository<ICharacterAttributeRow>>();
            characterAttributeRepositoryMock.Setup(c => c.GetAll()).Returns(() => fakeCharacterAttributes);
            characterAttributeRepositoryMock.Setup(c => c.Add(It.IsAny<ICharacterAttributeRow>())).Callback<ICharacterAttributeRow>(c =>
            {
                fakeCharacterAttributes.Add(c);
            });
            var service = new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object);

            int previousCount = service.GetAll().Count();

            var newCharacterAttributes = Fixture.CreateMany<IAttribute>();
            var newCharacterAttributeRow = new DefaultCharacterAttributeRow(newCharacterAttributes);
            service.Add(newCharacterAttributeRow);

            int newCount = service.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));
        }

        [Test]
        public void ReturnsNullForNoCharacterAttributeFoundById()
        {
            var characterAttributeRows = Fixture.CreateMany<DefaultCharacterAttributeRow>();

            var fakeCharacterAttributeRows = new List<ICharacterAttributeRow>(characterAttributeRows);

            var characterAttributeRepositoryMock = new Mock<IRepository<ICharacterAttributeRow>>();
            characterAttributeRepositoryMock.Setup(c => c.Get(It.IsInRange("1", "2", Range.Inclusive))).Returns<string>(id => fakeCharacterAttributeRows.FirstOrDefault(c => c.Id == id.ToString()));

            var service = new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object);

            var characterAttribute = service.Get("0");

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
            var characterAttributeRepositoryMock = new Mock<IRepository<ICharacterAttributeRow>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var service = new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object);

                service.Add(null);
            });
        }
    }
}

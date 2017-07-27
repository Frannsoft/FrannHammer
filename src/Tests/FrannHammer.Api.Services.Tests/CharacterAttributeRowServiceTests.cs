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
    public class CharacterAttributeRowServiceTests
    {
        private IRepository<ICharacterAttributeRow> MakeCharacterAttributesRepository()
        {
            var items = new List<ICharacterAttributeRow>
            {
                new CharacterAttributeRow
                {
                    Name = "one",
                    Owner = "ownerone",
                    OwnerId = 1,
                    Values = new List<IAttribute>
                    {
                        new CharacterAttribute
                        {
                            Name = "oneattribute",
                            Owner = "ownerone",
                            OwnerId = 1,
                            Value = "onevalue"
                        }
                    }
                },
                new CharacterAttributeRow
                {
                    Name = "two",
                    Owner = "ownertwo",
                    OwnerId = 2,
                    Values = new List<IAttribute>
                    {
                        new CharacterAttribute
                        {
                            Name = "twoattribute",
                            Owner = "ownertwo",
                            OwnerId = 2,
                            Value = "twovalue"
                        }
                    }
                }
            };

            var mockedRepository = new Mock<IRepository<ICharacterAttributeRow>>();
            mockedRepository.Setup(r => r.GetSingleWhere(It.IsAny<Func<ICharacterAttributeRow, bool>>()))
                .Returns((Func<ICharacterAttributeRow, bool> where) => items.Single(where));

            return mockedRepository.Object;
        }

        [Test]
        public void GetSingleWithNameAndMatchingCharacterOwnerId_ReturnsMatchingOwnerIdAndNameWithCorrectValue()
        {
            const string expectedCharacterAttributeName = "two";
            const int expectedOwnerId = 2;
            const string expectedOwner = "ownertwo";
            const string expectedAttributeValue = "twovalue";

            var repository = MakeCharacterAttributesRepository();
            var sut = new DefaultCharacterAttributeService(repository, new DefaultCharacterAttributeNameProvider());

            var results = sut.GetSingleWithNameAndMatchingCharacterOwnerId(expectedCharacterAttributeName, expectedOwnerId).ToList();

            results.ForEach(result =>
            {
                Assert.That(result.Name, Is.EqualTo(expectedCharacterAttributeName));
                Assert.That(result.Owner, Is.EqualTo(expectedOwner));
                Assert.That(result.OwnerId, Is.EqualTo(expectedOwnerId));

                result.Values.ToList().ForEach(value =>
                {
                    Assert.That(value.Name, Is.Not.Null, $"{nameof(value.Name)}");
                    Assert.That(value.Owner, Is.EqualTo(expectedOwner));
                    Assert.That(value.OwnerId, Is.EqualTo(expectedOwnerId));
                    Assert.That(value.Value, Is.EqualTo(expectedAttributeValue));
                });
            });
        }

        [Test]
        public void GetSingleWithNameAndMatchingCharacterOwner_ReturnsMatchingOwnerIdAndNameWithCorrectValue()
        {
            const string expectedCharacterAttributeName = "two";
            const int expectedOwnerId = 2;
            const string expectedOwner = "ownertwo";
            const string expectedAttributeValue = "twovalue";

            var repository = MakeCharacterAttributesRepository();
            var sut = new DefaultCharacterAttributeService(repository, new DefaultCharacterAttributeNameProvider());

            var results = sut.GetAllWithNameAndMatchingCharacterOwner(expectedCharacterAttributeName, expectedOwner).ToList();

            results.ForEach(result =>
            {
                Assert.That(result.Name, Is.EqualTo(expectedCharacterAttributeName));
                Assert.That(result.Owner, Is.EqualTo(expectedOwner));
                Assert.That(result.OwnerId, Is.EqualTo(expectedOwnerId));

                result.Values.ToList().ForEach(value =>
                {
                    Assert.That(value.Name, Is.Not.Null, $"{nameof(value.Name)}");
                    Assert.That(value.Owner, Is.EqualTo(expectedOwner));
                    Assert.That(value.OwnerId, Is.EqualTo(expectedOwnerId));
                    Assert.That(value.Value, Is.EqualTo(expectedAttributeValue));
                });
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;


namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class MoveServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            MoveParseClassMap.RegisterType<IMove, Move>();
        }

        [TearDown]
        public void TearDown()
        {
            MoveParseClassMap.ClearAllRegisteredTypes();
        }

        [Test]
        public void ThrowsArgumentNullExceptionForNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultMoveService(null);
            });
        }

        [Test]
        public void NullValueForSpecificPropertyReturnsNullValueWhenRetrievingSpecificPropertyOfMoves()
        {
            const string expectedName = "testName";
            const string movePropertyValueUnderTest = "28&gt;";
            const string movePropertyUnderTest = "autoCancel";
            const int expectedNumberOfKeysForEachResult = 4;

            var items = new List<Move>
            {
                new Move {Name = expectedName, AutoCancel = movePropertyValueUnderTest},
                new Move {Name = expectedName, AutoCancel = null}
            };

            var mockRepository = new Mock<IRepository<IMove>>();
            mockRepository.Setup(r => r.GetAllWhere(It.IsAny<Func<IMove, bool>>()))
               .Returns((Func<IMove, bool> where) => items.Where(where));

            var sut = new DefaultMoveService(mockRepository.Object);

            var results = sut.GetAllPropertyDataWhereName(expectedName, movePropertyUnderTest).ToList();

            Assert.That(results.Count, Is.EqualTo(2), $"{nameof(results.Count)}");

            Assert.That(results[0][MoveNameKey], Is.EqualTo(expectedName), $"{nameof(results)}[0]{MoveNameKey} actual move name expected.");
            Assert.That(results[0][RawValueKey], Is.EqualTo(movePropertyValueUnderTest), $"{nameof(results)}[0][{RawValueKey}]");
            Assert.That(results[0][Cancel1Key], Is.EqualTo("28>"), $"{nameof(results)}[0][{Cancel1Key}]");
            Assert.That(results[0].Count, Is.EqualTo(expectedNumberOfKeysForEachResult), $"{nameof(results)}[0] Keys");

            Assert.That(results[1][MoveNameKey], Is.EqualTo(expectedName), $"{nameof(results)}[1]{MoveNameKey} actual move name expected.");
            Assert.That(results[1][RawValueKey], Is.EqualTo(string.Empty), $"{nameof(results)}[1]{RawValueKey}");
            Assert.That(results[1][Cancel1Key], Is.EqualTo(string.Empty), $"{nameof(results)}[1]{Cancel1Key}");
            Assert.That(results[1][Cancel2Key], Is.EqualTo(string.Empty), $"{nameof(results)}[1]{Cancel2Key}");
            Assert.That(results[1].Keys.Count, Is.EqualTo(expectedNumberOfKeysForEachResult), $"{nameof(results)}[1] Keys");
        }

        [Test]
        public void MissingPropertyParserTypeReturnsDictionaryWithJustMoveAndRawPropertyValue()
        {
            //register test double
            MoveParseClassMap.ClearAllRegisteredTypes();
            MoveParseClassMap.RegisterType<IMove, TestMove>();

            const string expectedName = "testMove";
            const string movePropertyUnderTest = "angle";

            var items = new List<TestMove>
            {
                new TestMove {Name = expectedName, Angle = "1"},
                new TestMove {Name = expectedName, Angle = "2"}
            };

            var mockRepository = new Mock<IRepository<IMove>>();
            mockRepository.Setup(r => r.GetAllWhere(It.IsAny<Func<IMove, bool>>()))
               .Returns((Func<IMove, bool> where) => items.Where(where));

            var sut = new DefaultMoveService(mockRepository.Object);

            var results = sut.GetAllPropertyDataWhereName(expectedName, movePropertyUnderTest).ToList();

            Assert.That(results.Count, Is.EqualTo(2), $"{nameof(results.Count)}");

            Assert.That(results.All(result => result.Keys.Any(key => key.Equals(RawValueKey))), $"Unable to find {RawValueKey} key");
            Assert.That(results.All(result => result.Keys.Any(key => key.Equals(MoveNameKey))), $"Unable to find {MoveNameKey} key");
        }

        private class TestMove : IMove
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string HitboxActive { get; set; }
            public string FirstActionableFrame { get; set; }
            public string BaseDamage { get; set; }
            public string Angle { get; set; }
            public string BaseKnockBackSetKnockback { get; set; }
            public string LandingLag { get; set; }
            public string AutoCancel { get; set; }
            public string KnockbackGrowth { get; set; }
            public string MoveType { get; set; }
            public string Owner { get; set; }
        }
    }
}
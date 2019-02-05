using AutoFixture;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static FrannHammer.Api.Services.Tests.ApiServiceTestSetupUtility;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class DefaultMoveServiceTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void ThrowsArgumentNullExceptionForNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultMoveService(null, new GameParameterParserService("test"));
            });
        }

        private static IEnumerable<Tuple<string, string[]>> MoveProperties()
        {
            yield return Tuple.Create("baseDamage", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey });
            yield return Tuple.Create("hitboxActive", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey });
            yield return Tuple.Create("angle", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey });
            yield return Tuple.Create("knockbackGrowth", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey });
            yield return Tuple.Create("autoCancel", new[] { Cancel1Key, Cancel2Key, MoveNameKey, RawValueKey });
            yield return Tuple.Create("firstActionableFrame", new[] { FrameKey, MoveNameKey, RawValueKey });
            yield return Tuple.Create("landingLag", new[] { FramesKey, MoveNameKey, RawValueKey });
            yield return Tuple.Create("baseKnockback", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey });
            yield return Tuple.Create("setKnockback", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey });
        }

        [Test]
        public void GetThrowsForCharacterGetsOnlyThrowMovesForThatCharacter()
        {
            const string expectedCharacterName = "mario";
            const string throwName = "throw";
            const string grabName = "grab";
            var items = new List<Move>
            {
                new Move {Name = "fthrow", MoveType = MoveType.Throw.GetEnumDescription(), Owner = expectedCharacterName},
                new Move {Name = "dash grab", MoveType = MoveType.Throw.GetEnumDescription(), Owner = "ganondorf"},
                new Move {Name = "nair", MoveType = MoveType.Aerial.GetEnumDescription(), Owner = expectedCharacterName}
            };

            var mockRepository = new Mock<IRepository<IMove>>();
            ConfigureGetAllWhereOnMockRepository(mockRepository, items);

            var sut = new DefaultMoveService(mockRepository.Object, new GameParameterParserService("Smash4"));

            var results = sut.GetAllThrowsWhereCharacterNameIs(expectedCharacterName).ToList();

            Assert.That(results.Count, Is.EqualTo(1), $"{nameof(results.Count)}");

            results.ForEach(result =>
            {
                Assert.That(result.Owner, Is.EqualTo(expectedCharacterName), $"{result.Owner}");
                Assert.That(result.MoveType, Is.EqualTo(MoveType.Throw.GetEnumDescription()),
                    $"{nameof(result.MoveType)}");
                Assert.That(result.Name.Contains(throwName) || result.Name.Contains(grabName),
                    $"expecting name '{result.Name} to contain {throwName} or '{grabName}'");
            });
        }

        [Test]
        public void ThrowsArgumentExceptionWhereCharacterNameParameterEmptyForGetAllThrows()
        {
            var sut = new DefaultMoveService(new Mock<IRepository<IMove>>().Object, new GameParameterParserService("Smash4"));

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.GetAllThrowsWhereCharacterNameIs(string.Empty);
            });
        }

        [Test]
        public void ThrowsArgumentExceptionWhereCharacterNameParameterNullForGetAllThrows()
        {
            var sut = new DefaultMoveService(new Mock<IRepository<IMove>>().Object, new GameParameterParserService("Smash4"));

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.GetAllThrowsWhereCharacterNameIs(null);
            });
        }

        [Test]
        public void ReturnsEmptyEnumerableForNoThrowMovesForValidCharacter()
        {
            const string expectedCharacterName = "testCharacter";

            var items = new List<Move>
            {
                new Move {Name = "test", Owner = expectedCharacterName, MoveType = MoveType.Aerial.GetEnumDescription()}
            };

            var mockRepository = new Mock<IRepository<IMove>>();
            ConfigureGetAllWhereOnMockRepository(mockRepository, items);

            var sut = new DefaultMoveService(mockRepository.Object, new GameParameterParserService("Smash4"));

            var results = sut.GetAllThrowsWhereCharacterNameIs(expectedCharacterName);

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(results, Is.Not.Null, "should not be null.");
            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(results, Is.Empty, "should be empty.");
        }


        [Test]
        public void VerifyGetAllThrowsForCharacterCallsGetAllWhere()
        {
            var items = new List<Move>
            {
                new Move {Name = "test"}
            };

            var mockRepository = new Mock<IRepository<IMove>>();
            ConfigureGetAllWhereOnMockRepository(mockRepository, items);

            var sut = new DefaultMoveService(mockRepository.Object, new GameParameterParserService("Smash4"));

            sut.GetAllThrowsWhereCharacterNameIs("dummyValue");

            mockRepository.VerifyAll();
        }

        private class TestMove : IMove
        {
            public string InstanceId { get; set; }
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
            public bool IsWeightDependent { get; set; }
            public int OwnerId { get; set; }
            public Games Game { get; set; }
        }
    }
}
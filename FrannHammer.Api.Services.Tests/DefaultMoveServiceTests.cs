using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;
using static FrannHammer.Api.Services.Tests.ApiServiceTestSetupUtility;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class DefaultMoveServiceTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            MoveParseClassMap.RegisterType<IMove, Move>();
            _fixture = new Fixture();
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
                new DefaultMoveService(null, new Mock<IQueryMappingService>().Object);
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
        public void DetailedMovesContainExpectedProperties()
        {
            const int expectedOwnerId = 1;
            const string expectedCharacterName = "mario";

            //mock move repo
            //add fake moves with all properties filled out.  Some should match the passed in name, others should not
            var matchingItems =
                _fixture.Create<Move>();

            matchingItems.OwnerId = expectedOwnerId;
            matchingItems.Owner = expectedCharacterName;
            matchingItems.Name = "test";

            var totalItems = _fixture.CreateMany<Move>().ToList();
            totalItems.Add(matchingItems);

            var mockRepository = ConfigureMockRepositoryWithSeedMoves(totalItems, _fixture);

            //get all move property data for a move
            var sut = new DefaultMoveService(mockRepository, new Mock<IQueryMappingService>().Object);
            var results = sut.GetAllMovePropertyDataForCharacter(new Character { OwnerId = expectedOwnerId })
                .ToList();

            Assert.That(results.Count, Is.GreaterThan(0), $"{nameof(results.Count)}");

            //assert all expected moves are present
            var rawMoves = sut.GetAllWhereCharacterNameIs(expectedCharacterName).ToList();

            rawMoves.ForEach(rawMove =>
            {
                Assert.That(results.Any(result => result.MoveName.Equals(rawMove.Name)),
                    $"Results does not have move '{rawMove.Name}'.");
            });

            var firstMove = results.First();

            Assert.That(firstMove.MoveData.Any(data => data.Data.Any(d => d.Name.Equals(Hitbox1Key))),
                $"{nameof(firstMove)} does not contain {Hitbox1Key}: {nameof(firstMove)}");
            Assert.That(firstMove.MoveData.Any(data => data.Data.Any(d => d.Name.Equals(Hitbox2Key))),
                $"{nameof(firstMove)} does not contain {Hitbox2Key}: {nameof(firstMove)}");
            Assert.That(firstMove.MoveData.Any(data => data.Data.Any(d => d.Name.Equals(Hitbox3Key))),
                $"{nameof(firstMove)} does not contain {Hitbox3Key}: {nameof(firstMove)}");
            Assert.That(firstMove.MoveData.Any(data => data.Data.Any(d => d.Name.Equals(Hitbox4Key))),
                $"{nameof(firstMove)} does not contain {Hitbox4Key}: {nameof(firstMove)}");
            Assert.That(firstMove.MoveData.Any(data => data.Data.Any(d => d.Name.Equals(Hitbox5Key))),
                $"{nameof(firstMove)} does not contain {Hitbox5Key}: {nameof(firstMove)}");
            Assert.That(firstMove.MoveData.Any(data => data.Data.Any(d => d.Name.Equals(RawValueKey))),
                $"{nameof(firstMove)} does not contain {RawValueKey}: {nameof(firstMove)}");
            Assert.That(firstMove.MoveData.Any(data => data.Data.Any(d => d.Name.Equals(NotesKey))),
                $"{nameof(firstMove)} does not contain {NotesKey}: {nameof(firstMove)}");
        }

        [Test]
        [TestCaseSource(nameof(MoveProperties))]
        public void GetAllPropertyDataForMoveByName(Tuple<string, string[]> testData)
        {
            string propertyName = testData.Item1;
            var propertyKeysToAssertOn = testData.Item2;

            const string expectedMoveName = "Jab 1";

            //add fake moves with all properties filled out.  Some should match the passed in name, others should not
            var matchingItems = _fixture.CreateMany<Move>().ToList();
            matchingItems.ForEach(move => { move.Name = expectedMoveName; });

            var nonMatchingItems = _fixture.CreateMany<Move>().ToList();
            nonMatchingItems.AddRange(matchingItems);

            var mockRepository = ConfigureMockRepositoryWithSeedMoves(matchingItems, _fixture);

            var sut = new DefaultMoveService(mockRepository, new Mock<IQueryMappingService>().Object);

            var response = sut.GetAllPropertyDataWhereName(expectedMoveName, propertyName);

            // ReSharper disable once PossibleNullReferenceException
            var results = response.ToList();

            //assert results are expected (all move results are named jab 1 and contain the expected property info and the amount of results equals the above matching items)
            Assert.That(results.Count, Is.EqualTo(matchingItems.Count));

            results.ForEach(result =>
            {
                Assert.That(result[MoveNameKey], Is.EqualTo(expectedMoveName), $"{nameof(result)}.{MoveNameKey}");

                foreach (string propertyKey in propertyKeysToAssertOn)
                {
                    Assert.That(result.Keys.Any(key => key.Equals(propertyKey, StringComparison.CurrentCultureIgnoreCase)), $"{nameof(result)}.{propertyKey}");
                }
            });
        }

        [Test]
        [TestCaseSource(nameof(MoveProperties))]
        public void GetAllPropertyDataForMoveById(Tuple<string, string[]> testData)
        {
            string propertyName = testData.Item1;
            var propertyKeysToAssertOn = testData.Item2;

            const string expectedMoveId = "111";

            //add fake moves with all properties filled out.  Some should match the passed in name, others should not
            var matchingItem = _fixture.Create<Move>();
            matchingItem.InstanceId = expectedMoveId;

            var mockRepository = ConfigureMockRepositoryWithSeedMoves(new List<Move> { matchingItem }, _fixture);

            var sut = new DefaultMoveService(mockRepository, new Mock<IQueryMappingService>().Object);

            var result = sut.GetPropertyDataWhereId(expectedMoveId, propertyName);

            Assert.That(result, Is.Not.Null, $"{nameof(result)}");

            Assert.That(result[MoveNameKey], Is.Not.Empty, $"{nameof(result)}.{MoveNameKey}");

            foreach (string propertyKey in propertyKeysToAssertOn)
            {
                Assert.That(result.Keys.Any(key => key.Equals(propertyKey, StringComparison.CurrentCultureIgnoreCase)), $"{nameof(result)}.{propertyKey}");
            }
        }

        [Test]
        public void ThrowsArgumentExceptionForPassedInMovePropertyThatDoesNotExistOnMove()
        {
            const string expectedMoveName = "Jab 1";

            //mock repository
            var mockRepository = new Mock<IRepository<IMove>>();

            //start with getting all move base damages for jab 1
            var sut = new DefaultMoveService(mockRepository.Object, new Mock<IQueryMappingService>().Object);

            Assert.Throws<ArgumentException>(() =>
            {
                sut.GetAllPropertyDataWhereName(expectedMoveName, "baseDamages");
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

            ConfigureGetAllWhereOnMockRepository(mockRepository, items);

            var sut = new DefaultMoveService(mockRepository.Object, new Mock<IQueryMappingService>().Object);

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

            var sut = new DefaultMoveService(mockRepository.Object, new Mock<IQueryMappingService>().Object);

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
            var sut = new DefaultMoveService(new Mock<IRepository<IMove>>().Object, new Mock<IQueryMappingService>().Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.GetAllThrowsWhereCharacterNameIs(string.Empty);
            });
        }

        [Test]
        public void ThrowsArgumentExceptionWhereCharacterNameParameterNullForGetAllThrows()
        {
            var sut = new DefaultMoveService(new Mock<IRepository<IMove>>().Object, new Mock<IQueryMappingService>().Object);

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

            var sut = new DefaultMoveService(mockRepository.Object, new Mock<IQueryMappingService>().Object);

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

            var sut = new DefaultMoveService(mockRepository.Object, new Mock<IQueryMappingService>().Object);

            sut.GetAllThrowsWhereCharacterNameIs("dummyValue");

            mockRepository.VerifyAll();
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
            ConfigureGetAllWhereOnMockRepository(mockRepository, items);

            var sut = new DefaultMoveService(mockRepository.Object, new Mock<IQueryMappingService>().Object);

            var results = sut.GetAllPropertyDataWhereName(expectedName, movePropertyUnderTest).ToList();

            Assert.That(results.Count, Is.EqualTo(2), $"{nameof(results.Count)}");

            Assert.That(results.All(result => result.Keys.Any(key => key.Equals(RawValueKey))), $"Unable to find {RawValueKey} key");
            Assert.That(results.All(result => result.Keys.Any(key => key.Equals(MoveNameKey))), $"Unable to find {MoveNameKey} key");
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
        }
    }
}
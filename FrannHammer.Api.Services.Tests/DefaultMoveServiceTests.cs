using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Api.Services.Contracts;
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
            yield return Tuple.Create("baseDamage", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey, NotesKey });
            yield return Tuple.Create("hitboxActive", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey, NotesKey });
            yield return Tuple.Create("angle", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey, NotesKey });
            yield return Tuple.Create("knockbackGrowth", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey, NotesKey });
            yield return Tuple.Create("autoCancel", new[] { Cancel1Key, Cancel2Key, MoveNameKey, RawValueKey, NotesKey });
            yield return Tuple.Create("firstActionableFrame", new[] { FrameKey, MoveNameKey, RawValueKey, NotesKey });
            yield return Tuple.Create("landingLag", new[] { FramesKey, MoveNameKey, RawValueKey, NotesKey });
            yield return Tuple.Create("baseKnockback", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey, NotesKey });
            yield return Tuple.Create("setKnockback", new[] { Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key, MoveNameKey, RawValueKey, NotesKey });
        }

        private IRepository<IMove> MakeRepository()
        {
            const int expectedOwnerId = 1;
            const string expectedCharacterName = "mario";

            //mock move repo
            //add fake moves with all properties filled out.  Some should match the passed in name, others should not
            var matchingItem = _fixture.Create<Move>();

            //TODO - just make a manual real life one.  The fixture created one has too much junk data.
            matchingItem.Angle = "50";
            matchingItem.AutoCancel = "20";
            matchingItem.BaseDamage = "40/30";
            matchingItem.BaseKnockBackSetKnockback = "W: 15/10/15";
            matchingItem.FirstActionableFrame = "25";
            matchingItem.HitboxActive = "3-6";
            matchingItem.KnockbackGrowth = "20";
            matchingItem.LandingLag = "20";
            matchingItem.OwnerId = expectedOwnerId;
            matchingItem.Owner = expectedCharacterName;
            matchingItem.MoveType = MoveType.Ground.GetEnumDescription();
            matchingItem.Name = "test";

            var totalItems = new List<Move>
            {
                matchingItem
            };
            totalItems.AddRange(_fixture.CreateMany<Move>().ToList());

            var mockRepository = ConfigureMockRepositoryWithSeedMoves(totalItems, _fixture);

            return mockRepository;
        }

        [Test]
        public void GetAllMovePropertyDataForCharacter_ReturnsAllMovesForCharacter()
        {
            var mockRepository = MakeRepository();
            var anonymousMove = mockRepository.GetAll().First();

            //get all move property data for a move
            var sut = new DefaultMoveService(mockRepository, new Mock<IQueryMappingService>().Object);
            var results = sut.GetAllMovePropertyDataForCharacter(new Character { OwnerId = anonymousMove.OwnerId })
                .ToList();

            Assert.That(results.Count, Is.GreaterThan(0), $"{nameof(results.Count)}");

            var storedMoves = mockRepository.GetAllWhere(move => move.Owner == anonymousMove.Owner).ToList();

            storedMoves.ForEach(rawMove =>
            {
                Assert.That(results.Any(result => result.MoveName.Equals(rawMove.Name)),
                    $"Results does not have move '{rawMove.Name}'.");
            });
        }

        [Test]
        public void GetAllMovePropertyDataForCharacter_EachMoveDataPropertyHasExpectedHitboxProperties()
        {
            var mockRepository = MakeRepository();
            var anonymousMove = mockRepository.GetAll().First(move => move.Name == "test");

            //get all move property data for a move
            var sut = new DefaultMoveService(mockRepository, new Mock<IQueryMappingService>().Object);
            var results = sut.GetAllMovePropertyDataForCharacter(new Character { OwnerId = anonymousMove.OwnerId })
                .ToList();

            var firstMove = results.First(move => move.MoveName == "test");

            var hitboxProperty = firstMove.MoveProperties.FirstOrDefault(property => property.Name.Equals("HitboxActive"));
            AssertHitboxBasedPropertyIsValid(hitboxProperty);

            var baseDamageProperty = firstMove.MoveProperties.FirstOrDefault(property => property.Name.Equals("BaseDamage"));
            AssertHitboxBasedPropertyIsValid(baseDamageProperty);

            var angleProperty = firstMove.MoveProperties.FirstOrDefault(property => property.Name.Equals("Angle"));
            AssertHitboxBasedPropertyIsValid(angleProperty);

            var knockbackGrowthProperty = firstMove.MoveProperties.FirstOrDefault(property => property.Name.Equals("KnockbackGrowth"));
            AssertHitboxBasedPropertyIsValid(knockbackGrowthProperty);

            var baseKnockbackSetKnockback = firstMove.MoveProperties.FirstOrDefault(property => property.Name.Equals("BaseKnockBackSetKnockback"));
            AssertHitboxBasedPropertyIsValid(baseKnockbackSetKnockback);

            var autoCancelProperty = firstMove.MoveProperties.FirstOrDefault(property => property.Name.Equals("AutoCancel"));
            AssertAutoCancelPropertyIsValid(autoCancelProperty);

            var firstActionableFrameProperty = firstMove.MoveProperties.FirstOrDefault(property => property.Name.Equals("FirstActionableFrame"));
            AssertFirstActionableFramePropertyIsValid(firstActionableFrameProperty);

            var landingLagProperty = firstMove.MoveProperties.FirstOrDefault(property => property.Name.Equals("LandingLag"));
            AssertLandingLagPropertyIsValid(landingLagProperty);
        }

        private void AssertLandingLagPropertyIsValid(ParsedMoveDataProperty landingLagProperty)
        {
            Assert.That(landingLagProperty, Is.Not.Null, $"{nameof(landingLagProperty)}");
            Assert.That(landingLagProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(FramesKey)), Is.EqualTo(1), $"{nameof(FramesKey)}:{landingLagProperty.Name}");
            Assert.That(landingLagProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(RawValueKey)), Is.EqualTo(1), $"{nameof(RawValueKey)}:{landingLagProperty.Name}");
        }

        private void AssertFirstActionableFramePropertyIsValid(ParsedMoveDataProperty firstActionableFrameProperty)
        {
            Assert.That(firstActionableFrameProperty, Is.Not.Null, $"{nameof(firstActionableFrameProperty)}");
            Assert.That(firstActionableFrameProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(FrameKey)), Is.EqualTo(1), $"{nameof(FrameKey)}:{firstActionableFrameProperty.Name}");
            Assert.That(firstActionableFrameProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(RawValueKey)), Is.EqualTo(1), $"{nameof(RawValueKey)}:{firstActionableFrameProperty.Name}");
        }

        private void AssertHitboxBasedPropertyIsValid(ParsedMoveDataProperty hitboxBasedProperty)
        {
            Assert.That(hitboxBasedProperty, Is.Not.Null, $"{nameof(hitboxBasedProperty)}");
            Assert.That(hitboxBasedProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(Hitbox1Key)), Is.EqualTo(1), $"{nameof(Hitbox1Key)}:{hitboxBasedProperty.Name}");
            Assert.That(hitboxBasedProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(Hitbox2Key)), Is.EqualTo(1), $"{nameof(Hitbox2Key)}:{hitboxBasedProperty.Name}");
            Assert.That(hitboxBasedProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(Hitbox3Key)), Is.EqualTo(1), $"{nameof(Hitbox3Key)}:{hitboxBasedProperty.Name}");
            Assert.That(hitboxBasedProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(Hitbox4Key)), Is.EqualTo(1), $"{nameof(Hitbox4Key)}:{hitboxBasedProperty.Name}");
            Assert.That(hitboxBasedProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(Hitbox5Key)), Is.EqualTo(1), $"{nameof(Hitbox5Key)}:{hitboxBasedProperty.Name}");
            Assert.That(hitboxBasedProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(RawValueKey)), Is.EqualTo(1), $"{nameof(RawValueKey)}:{hitboxBasedProperty.Name}");
            Assert.That(hitboxBasedProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(NotesKey)), Is.EqualTo(1), $"{nameof(NotesKey)}:{hitboxBasedProperty.Name}");
        }

        private void AssertAutoCancelPropertyIsValid(ParsedMoveDataProperty autoCancelProperty)
        {
            Assert.That(autoCancelProperty, Is.Not.Null, $"{nameof(autoCancelProperty)}");
            Assert.That(autoCancelProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(Cancel1Key)), Is.EqualTo(1), $"{nameof(Cancel1Key)}:{autoCancelProperty.Name}");
            Assert.That(autoCancelProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(Cancel2Key)), Is.EqualTo(1), $"{nameof(Cancel2Key)}:{autoCancelProperty.Name}");
            Assert.That(autoCancelProperty.MoveAttributes.Count(attribute => attribute.Name.Equals(RawValueKey)), Is.EqualTo(1), $"{nameof(RawValueKey)}:{autoCancelProperty.Name}");
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

            var matchingItem = _fixture.Create<Move>();

            //TODO - just make a manual real life one.  The fixture created one has too much junk data.
            matchingItem.Angle = "50";
            matchingItem.AutoCancel = "20";
            matchingItem.BaseDamage = "40/30";
            matchingItem.BaseKnockBackSetKnockback = "W: 15/10/15";
            matchingItem.FirstActionableFrame = "25";
            matchingItem.HitboxActive = "3-6";
            matchingItem.KnockbackGrowth = "20";
            matchingItem.LandingLag = "20";
            matchingItem.MoveType = MoveType.Ground.GetEnumDescription();
            matchingItem.Name = "test";

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
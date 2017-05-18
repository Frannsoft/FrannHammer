using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class MoveControllerTests : BaseControllerTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            MoveParseClassMap.RegisterType<IMove, Move>();
        }

        [TearDown]
        public void TearDown()
        {
            MoveParseClassMap.ClearAllRegisteredTypes();
        }

        [Test]
        public void ThrowsArgumentNullExceptionForNullMoveServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MoveController(null);
            });
        }

        private IRepository<IMove> ConfigureMockRepositoryWithSeedMoves(string seedMoveName, IEnumerable<Move> matchingMoves)
        {
            //add fake moves with all properties filled out.  Some should match the passed in name, others should not
            var matchingItems = matchingMoves;

            var itemsForMockRepository = Fixture.CreateMany<Move>().ToList();
            itemsForMockRepository.AddRange(matchingItems);

            //mock repository
            var mockRepository = new Mock<IRepository<IMove>>();
            mockRepository.Setup(r => r.GetAllWhere(It.IsAny<Func<IMove, bool>>()))
                .Returns((Func<IMove, bool> where) => itemsForMockRepository.Where(where));
            mockRepository.Setup(r => r.GetAll()).Returns(() => itemsForMockRepository);

            return mockRepository.Object;
        }

        [Test]
        [TestCase("baseDamage", Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key)]
        [TestCase("hitboxActive", Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key)]
        [TestCase("angle", Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key)]
        [TestCase("knockbackGrowth", Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key)]
        [TestCase("autoCancel", Cancel1Key, Cancel2Key, RawValueKey, MoveNameKey)]
        [TestCase("firstActionableFrame", FrameKey, RawValueKey, MoveNameKey)]
        [TestCase("landingLag", FramesKey, RawValueKey, MoveNameKey)]
        [TestCase("baseKnockback", Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key)]
        [TestCase("setKnockback", Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key)]
        public void GetAllHitboxPropertyDataForMoveByName(string propertyName, params string[] propertyKeysToAssertOn)
        {
            const string expectedMoveName = "Jab 1";

            //add fake moves with all properties filled out.  Some should match the passed in name, others should not
            var matchingItems = Fixture.CreateMany<Move>().ToList();
            matchingItems.ForEach(move => { move.Name = expectedMoveName; });

            var nonMatchingItems = Fixture.CreateMany<Move>().ToList();
            nonMatchingItems.AddRange(matchingItems);

            //mock repository
            var mockRepository = ConfigureMockRepositoryWithSeedMoves(expectedMoveName, matchingItems);

            //start with getting all move base damages for jab 1
            var moveService = new DefaultMoveService(mockRepository);
            var controller = new MoveController(moveService);

            var response = controller.GetAllPropertyDataForMoveByName(expectedMoveName, propertyName) as OkNegotiatedContentResult<IEnumerable<IDictionary<string, string>>>;

            Assert.That(response, Is.Not.Null, $"{nameof(response)}");

            // ReSharper disable once PossibleNullReferenceException
            var results = response.Content.ToList();

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
        public void ThrowsArgumentExceptionForPassedInMovePropertyThatDoesNotExistOnMove()
        {
            const string expectedMoveName = "Jab 1";

            //mock repository
            var mockRepository = new Mock<IRepository<IMove>>();

            //start with getting all move base damages for jab 1
            var moveService = new DefaultMoveService(mockRepository.Object);
            var controller = new MoveController(moveService);

            Assert.Throws<ArgumentException>(() =>
            {
                controller.GetAllPropertyDataForMoveByName(expectedMoveName, "baseDamages");
            });
        }
    }
}

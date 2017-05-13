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

        [Test]
        public void GetAllPropertyDataForMoveByName()
        {
            const string expectedMoveName = "Jab 1";

            //add fake moves with all properties filled out.  Some should match the passed in name, others should not
            var matchingItems = Fixture.CreateMany<Move>().ToList();
            matchingItems.ForEach(move => { move.Name = expectedMoveName; });

            var nonMatchingItems = Fixture.CreateMany<Move>().ToList();
            nonMatchingItems.AddRange(matchingItems);

            //mock repository
            var mockRepository = new Mock<IRepository<IMove>>();
            mockRepository.Setup(r => r.GetAllWhere(It.IsAny<Func<IMove, bool>>()))
                .Returns((Func<IMove, bool> where) => matchingItems.Where(where));

            //start with getting all move base damages for jab 1
            var moveService = new DefaultMoveService(mockRepository.Object);
            var controller = new MoveController(moveService);

            var response = controller.GetAllPropertyDataForMoveByName(expectedMoveName, "baseDamage") as OkNegotiatedContentResult<IEnumerable<IDictionary<string, string>>>;

            Assert.That(response, Is.Not.Null, $"{nameof(response)}");

            // ReSharper disable once PossibleNullReferenceException
            var results = response.Content.ToList();

            //assert results are expected (all move results are named jab 1 and contain the expected property info and the amount of results equals the above matching items)
            Assert.That(results.Count, Is.EqualTo(matchingItems.Count));

            results.ForEach(result =>
            {
                Assert.That(result[MoveNameKey], Is.EqualTo(expectedMoveName), $"{nameof(result)}.{MoveNameKey}");
                Assert.That(result[Hitbox1Key], Is.Not.Empty, $"{nameof(result)}.{Hitbox1Key}");
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

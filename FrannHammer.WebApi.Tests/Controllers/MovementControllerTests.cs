using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class MovementControllerTests : BaseControllerTests
    {
        [SetUp]
        public override void SetUp()
        {
            Fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IMovement),
                    typeof(Movement)));
        }

        [Test]
        public void ConstructorRejectsNullMovementService()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MovementController(null);
            });
        }

        [Test]
        public void Error_ReturnsNotFoundResultWhenAttributeDoesNotExist()
        {
            var movementRepositoryMock = new Mock<IRepository<IMovement>>();
            movementRepositoryMock.Setup(c => c.Get(It.IsInRange("0", "1", Range.Inclusive))).Returns(() => Fixture.Create<IMovement>());

            var controller =
                new MovementController(
                    new DefaultMovementService(movementRepositoryMock.Object));

            var response = controller.GetMovement("-1") as NotFoundResult;

            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void GetAMovementName()
        {
            var testMovement = Fixture.Create<IMovement>();
            var movementServiceMock = new Mock<IMovementService>();
            movementServiceMock.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => testMovement);

            var controller = new MovementController(movementServiceMock.Object);

            var response = controller.GetMovement("0") as OkNegotiatedContentResult<IMovement>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var movement = response.Content;

            Assert.That(movement.Name, Is.Not.Empty);
            Assert.That(movement.Name, Is.EqualTo(testMovement.Name), $"Movement name was not equal to {testMovement.Name}");
        }

        [Test]
        public void GetManyMovements()
        {
            var movementServiceMock = new Mock<IMovementService>();
            movementServiceMock.Setup(c => c.GetAll(It.IsAny<string>()))
                .Returns(() => Fixture.CreateMany<IMovement>());

            var controller = new MovementController(movementServiceMock.Object);

            var response = controller.GetAllMovements() as OkNegotiatedContentResult<IEnumerable<IMovement>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var movements = response.Content.ToList();

            CollectionAssert.AllItemsAreUnique(movements);
            CollectionAssert.IsNotEmpty(movements);

            movements.ForEach(attribute =>
            {
                Assert.That(attribute.Name, Is.Not.Empty);
                Assert.That(attribute.Value, Is.Not.Empty);
            });
        }
    }
}

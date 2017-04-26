using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.DataAccess.MongoDb;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using NUnit.Framework;

namespace FrannHammer.WebApi.MongoDb.Integration.Tests
{
    [TestFixture]
    public class MovementControllerTests : BaseControllerTests
    {
        private MovementController _controller;


        public MovementControllerTests()
            : base(typeof(Movement))
        { }

        [SetUp]
        public void SetUp()
        {
            _controller = new MovementController(new DefaultMovementService(new MongoDbRepository<IMovement>(MongoDatabase)));
        }

        private static void AssertmovementIsValid(IMovement movement)
        {
            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Name, Is.Not.Null);
            Assert.That(movement.OwnerId, Is.Not.Null);
            Assert.That(movement.Value, Is.Not.Null);
            Assert.That(movement.Id, Is.GreaterThan(0));
        }

        [Test]
        public void GetSingleMovement()
        {
            var response = _controller.GetMovement("1") as OkNegotiatedContentResult<IMovement>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var movement = response.Content;
            AssertmovementIsValid(movement);
        }

        [Test]
        public void GetAllMovements()
        {
            var response = _controller.GetAllMovements() as OkNegotiatedContentResult<IEnumerable<IMovement>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var movements = response.Content.ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.IsNotEmpty(movements);
            CollectionAssert.AllItemsAreUnique(movements);
            movements.ForEach(AssertmovementIsValid);
        }
    }
}

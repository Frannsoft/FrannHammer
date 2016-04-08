using System;
using System.Linq;
using System.Web.Http.Results;
using KuroganeHammer.Data.Api.DTOs;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    [TestFixture]
    public class MovementsControllerTest : BaseControllerTest
    {
        private Character _loadedCharacter;

        [SetUp]
        public void SetUp()
        {
            _loadedCharacter = TestObjects.Character();
            CharactersController.PostCharacter(_loadedCharacter);
        }


        [Test]
        public void ShouldGetMovements()
        {
            var movement = TestObjects.Movement();
            MovementsController.PostMovement(movement);

            var result = MovementsController.GetMovement(movement.Id) as OkNegotiatedContentResult<MovementDto>;

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllMovements()
        {
            var movements = TestObjects.Movement();
            MovementsController.PostMovement(movements);

            var results = MovementsController.GetMovements();

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        [TestCase("Weight")]
        public void ShouldGetAllMovementsByName(string name)
        {
            var movement = TestObjects.Movement();
            var movement2 = new Movement
            {
                Id = 2,
                LastModified = DateTime.Now,
                Name = name,
                OwnerId = _loadedCharacter.Id,
                Value = "3"
            };

            MovementsController.PostMovement(movement);
            MovementsController.PostMovement(movement2);

            var results = MovementsController.GetMovementsByName(name);

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().Name, Is.EqualTo(name));
        }

        [Test]
        public void ShouldAddMovements()
        {
            var movement = TestObjects.Movement();
            var result = MovementsController.PostMovement(movement) as CreatedAtRouteNegotiatedContentResult<Movement>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Content, Is.Not.Null);
            Assert.AreEqual(movement, result?.Content);
        }

        [Test]
        public void ShouldUpdateMovements()
        {
            const string expectedName = "jab 2";
            var movement = TestObjects.Movement();

            var dateTime = DateTime.Now;

            var returnedMovements =
                MovementsController.PostMovement(movement) as CreatedAtRouteNegotiatedContentResult<Movement>;

            if (returnedMovements != null)
            {
                returnedMovements.Content.Name = expectedName;
                MovementsController.PutMovement(returnedMovements.Content.Id, returnedMovements.Content);
            }

            var updatedMovements = MovementsController.GetMovement(movement.Id) as OkNegotiatedContentResult<MovementDto>;

            Assert.That(updatedMovements?.Content.Name, Is.EqualTo(expectedName));
            Assert.That(updatedMovements?.Content.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteMovements()
        {
            var movement = TestObjects.Movement();
            MovementsController.PostMovement(movement);

            MovementsController.DeleteMovement(movement.Id);

            var result = MovementsController.GetMovement(movement.Id) as NotFoundResult;
            Assert.That(result, Is.Not.Null);
        }
    }
}

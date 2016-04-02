using System;
using System.Linq;
using System.Web.Http.Results;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    [TestFixture]
    public class MovementsControllerTest : BaseControllerTest
    {

        [Test]
        public void ShouldGetMovements()
        {
            var movement = TestObjects.Movement();
            MovementsController.PostMovement(movement);

            var result = MovementsController.GetMovement(movement.Id) as OkNegotiatedContentResult<Movement>;

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

            var updatedMovements = MovementsController.GetMovement(movement.Id) as OkNegotiatedContentResult<Movement>;

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

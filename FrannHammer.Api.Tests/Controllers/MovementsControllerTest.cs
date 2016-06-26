using System;
using System.Linq;
using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Api.DTOs;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class MovementsControllerTest : EffortBaseTest
    {
        private MovementsController _controller;

        private Movement Post(Movement movement)
        {
            return ExecuteAndReturnCreatedAtRouteContent<Movement>(
                () => _controller.PostMovement(movement));
        }

        private MovementDto Get(int id)
        {
            return ExecuteAndReturnContent<MovementDto>(
                () => _controller.GetMovement(id));
        }

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new MovementsController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void ShouldGetMovement()
        {

            var movement = TestObjects.Movement();
            Post(movement);
            Get(movement.Id);
        }

        [Test]
        [Ignore("This intermittently fails due to the size of the response.  Arguably, a call like this shouldn't even be exposed.")]
        public void ShouldGetAllMovements()
        {
            //nunit seems a little slow when this isn't fully evaluated...
            var results = _controller.GetMovements().ToList();

            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(MovementDto));
        }

        [Test]
        [TestCase("Weight")]
        public void ShouldGetAllMovementsByName(string name)
        {
            var character = ExecuteAndReturnCreatedAtRouteContent<Character>(
                () => new CharactersController(Context).PostCharacter(TestObjects.Character()));

            var movement = TestObjects.Movement();
            var movement2 = new Movement
            {
                Id = 2,
                LastModified = DateTime.Now,
                Name = name,
                OwnerId = character.Id,
                Value = "3"
            };

            Post(movement);
            Post(movement2);

            var results = _controller.GetMovementsByName(name);

            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(MovementDto));
        }

        [Test]
        public void ShouldAddMovements()
        {
            var movement = TestObjects.Movement();
            Post(movement);
        }

        [Test]
        public void ShouldUpdateMovements()
        {
            const string expectedName = "jab 2";
            var movement = TestObjects.Movement();

            var dateTime = DateTime.Now;
            Thread.Sleep(100);
            var returnedMovements = Post(movement);

            if (returnedMovements != null)
            {
                returnedMovements.Name = expectedName;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutMovement(returnedMovements.Id, returnedMovements));
            }

            var updatedMovements = Get(movement.Id);

            Assert.That(updatedMovements?.Name, Is.EqualTo(expectedName));
            Assert.That(updatedMovements?.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteMovements()
        {
            var movement = TestObjects.Movement();
            Post(movement);

            _controller.DeleteMovement(movement.Id);

            ExecuteAndReturn<NotFoundResult>(() => _controller.GetMovement(movement.Id));
        }
    }
}

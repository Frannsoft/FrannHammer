using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class MovementsControllerTest : EffortBaseTest
    {
        private MovementsController _controller;

        private MovementDto Post(MovementDto movement)
        {
            return ExecuteAndReturnCreatedAtRouteContent<MovementDto>(
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

        //[Test]
        //[Ignore("This intermittently fails due to the size of the response.  Arguably, a call like this shouldn't even be exposed.")]
        //public void ShouldGetAllMovements()
        //{
        //    //nunit seems a little slow when this isn't fully evaluated...
        //    var results = _controller.GetMovements().ToList();

        //    CollectionAssert.AllItemsAreNotNull(results);
        //    CollectionAssert.AllItemsAreUnique(results);
        //    CollectionAssert.AllItemsAreInstancesOfType(results, typeof(Movement));
        //}

        [Test]
        [TestCase("Weight")]
        public void ShouldGetAllMovementsByName(string name)
        {
            var character = new CharactersController(Context).GetCharacters().First();

            var movement = TestObjects.Movement();
            var movement2 = new MovementDto
            {
                Id = 2,
                Name = name,
                OwnerId = character.Id,
                Value = "3"
            };

            Post(movement);
            Post(movement2);

            var results = _controller.GetMovementsByName(name).ToList();

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

            var movement = _controller.GetMovementsByName("Gravity").First();

            if (movement != null)
            {
                movement.Name = expectedName;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutMovement(movement.Id, movement));
            }

            var updatedMovements = Get(movement.Id);

            Assert.That(updatedMovements?.Name, Is.EqualTo(expectedName));
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

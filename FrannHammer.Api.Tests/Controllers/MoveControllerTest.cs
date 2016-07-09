using System;
using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class MoveControllerTest : EffortBaseTest
    {
        private MovesController _controller;

        private MoveDto Post(MoveDto move)
        {
            return ExecuteAndReturnCreatedAtRouteContent<MoveDto>(
                () => _controller.PostMove(move));
        }

        private MoveDto Get(int id)
        {
            return ExecuteAndReturnContent<MoveDto>(
                () => _controller.GetMove(id));
        }

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new MovesController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void ShouldGetMove()
        {
            var move = TestObjects.Move();
            Post(move);
            Get(move.Id);
        }

        //[Test]
        //[Ignore("This intermittently fails due to the size of the response.  Arguably, a call like this shouldn't even be exposed.")]
        //public void ShouldGetAllMoves()
        //{
        //    var results = _controller.GetMoves();
        //    CollectionAssert.AllItemsAreNotNull(results);
        //    CollectionAssert.AllItemsAreUnique(results);
        //    CollectionAssert.AllItemsAreInstancesOfType(results, typeof(Move));
        //}

        [Test]
        [TestCase("Jab+2")]
        public void ShouldGetAllMovesByName(string name)
        {
            //arrange
            var character = new CharactersController(Context).GetCharacters().First();

            var move = TestObjects.Move();
            var move2 = new MoveDto
            {
                Id = 2,
                Name = name,
                OwnerId = character.Id
            };

            Post(move);
            Post(move2);

            //act
            var results = _controller.GetMovesByName(name);

            //assert
            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(MoveDto));
        }

        [Test]
        public void ShouldAddMove()
        {
            var move = TestObjects.Move();
            var result = Post(move);

            var latestMove = _controller.GetMovesByName(move.Name).ToList().Last();

            Assert.AreEqual(result, latestMove);
        }

        [Test]
        public void ShouldUpdateMove()
        {
            const string expectedName = "jab 2";
            var move = _controller.GetMovesByName("Jab 1").First();

            if (move != null)
            {
                move.Name = expectedName;
                _controller.PutMove(move.Id, move);
            }

            var updatedMove = Get(move.Id);

            Assert.That(updatedMove?.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void ShouldDeleteMove()
        {
            var move = TestObjects.Move();
            Post(move);

            _controller.DeleteMove(move.Id);

            ExecuteAndReturn<NotFoundResult>(() => _controller.GetMove(move.Id));
        }
    }
}

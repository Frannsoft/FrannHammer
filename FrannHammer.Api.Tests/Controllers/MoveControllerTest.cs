using System;
using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Api.DTOs;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class MoveControllerTest : EffortBaseTest
    {
        private MovesController _controller;

        private Move Post(Move move)
        {
            return ExecuteAndReturnCreatedAtRouteContent<Move>(
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

        [Test]
        [Ignore("This intermittently fails due to the size of the response.  Arguably, a call like this shouldn't even be exposed.")]
        public void ShouldGetAllMoves()
        {
            var results = _controller.GetMoves();
            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(MoveDto));
        }

        [Test]
        [TestCase("Jab+2")]
        public void ShouldGetAllMovesByName(string name)
        {
            //arrange
            var character = ExecuteAndReturnCreatedAtRouteContent<Character>(
                () => new CharactersController(Context).PostCharacter(TestObjects.Character()));

            var move = TestObjects.Move();
            var move2 = new Move
            {
                Id = 2,
                LastModified = DateTime.Now,
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

            Assert.AreEqual(move, result);
        }

        [Test]
        public void ShouldUpdateMove()
        {
            const string expectedName = "jab 2";
            var move = TestObjects.Move();

            var dateTime = DateTime.Now;
            Thread.Sleep(100);
            var returnedMove = Post(move);

            if (returnedMove != null)
            {
                returnedMove.Name = expectedName;
                _controller.PutMove(returnedMove.Id, returnedMove);
            }

            var updatedMove = Get(move.Id);

            Assert.That(updatedMove?.Name, Is.EqualTo(expectedName));
            Assert.That(updatedMove?.LastModified, Is.GreaterThan(dateTime));
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

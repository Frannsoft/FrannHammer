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

        [Test]
        public void ShouldGetHitboxDataForMove()
        {
            var move = _controller.GetMoves().First();

            var hitbox = ExecuteAndReturnContent<HitboxDto>(() => _controller.GetMoveHitboxData(move.Id));

            //assert
            Assert.That(hitbox, Is.Not.Null);
            Assert.That(hitbox, Is.TypeOf<HitboxDto>());
        }

        [Test]
        public void ShouldGetAngleDataForMove()
        {
            var move = _controller.GetMoves().First();

            var angle = ExecuteAndReturnContent<AngleDto>(() => _controller.GetMoveAngleData(move.Id));

            //assert
            Assert.That(angle, Is.Not.Null);
            Assert.That(angle, Is.TypeOf<AngleDto>());
        }

        [Test]
        public void ShouldGetBaseDamageDataForMove()
        {
            var move = _controller.GetMoves().First();

            var baseDamage = ExecuteAndReturnContent<BaseDamageDto>(() => _controller.GetMoveBaseDamageData(move.Id));

            //assert
            Assert.That(baseDamage, Is.Not.Null);
            Assert.That(baseDamage, Is.TypeOf<BaseDamageDto>());
        }

        [Test]
        public void ShouldGetKnockbackGrowthDataForMove()
        {
            var move = _controller.GetMoves().First();

            var knockbackGrowth = ExecuteAndReturnContent<KnockbackGrowthDto>(() => _controller.GetMoveKnockbackGrowthData(move.Id));

            //assert
            Assert.That(knockbackGrowth, Is.Not.Null);
            Assert.That(knockbackGrowth, Is.TypeOf<KnockbackGrowthDto>());
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

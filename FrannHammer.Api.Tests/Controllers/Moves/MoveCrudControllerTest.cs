using System;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.DTOs;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.Moves
{
    [TestFixture]
    public class MoveCrudControllerTest : BaseControllerTest
    {
        private Character _loadedCharacter;

        [SetUp]
        public void SetUp()
        {
            _loadedCharacter = TestObjects.Character();
            CharactersController.PostCharacter(_loadedCharacter);
        }

        [Test]
        public void ShouldGetMove()
        {
            var move = TestObjects.Move();
            MovesController.PostMove(move);

            var result = MovesController.GetMove(move.Id) as OkNegotiatedContentResult<MoveDto>;

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllMoves()
        {
            var move = TestObjects.Move();
            MovesController.PostMove(move);

            var results = MovesController.GetMoves();

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        [TestCase("Jab+2")]
        public void ShouldGetAllMovesByName(string name)
        {
            //arrange

            var move = TestObjects.Move();
            var move2 = new Move
            {
                Id = 2,
                LastModified = DateTime.Now,
                Name = name,
                OwnerId = _loadedCharacter.Id
            };

            MovesController.PostMove(move);
            MovesController.PostMove(move2);

            //act
            var results = MovesController.GetMovesByName(name);

            //assert
            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().Name, Is.EqualTo(name));
        }

        [Test]
        public void ShouldAddMove()
        {
            var move = TestObjects.Move();
            var result = MovesController.PostMove(move) as CreatedAtRouteNegotiatedContentResult<Move>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Content, Is.Not.Null);
            Assert.AreEqual(move, result?.Content);
        }

        [Test]
        public void ShouldUpdateMove()
        {
            const string expectedName = "jab 2";
            var move = TestObjects.Move();

            var dateTime = DateTime.Now;

            var returnedMove = MovesController.PostMove(move) as CreatedAtRouteNegotiatedContentResult<Move>;

            if (returnedMove != null)
            {
                returnedMove.Content.Name = expectedName;
                MovesController.PutMove(returnedMove.Content.Id, returnedMove.Content);
            }

            var updatedMove = MovesController.GetMove(move.Id) as OkNegotiatedContentResult<MoveDto>;

            Assert.That(updatedMove?.Content.Name, Is.EqualTo(expectedName));
            Assert.That(updatedMove?.Content.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteMove()
        {
            var move = TestObjects.Move();
            MovesController.PostMove(move);

            MovesController.DeleteMove(move.Id);

            var result = MovesController.GetMove(move.Id) as NotFoundResult;
            Assert.That(result, Is.Not.Null);
        }
    }
}

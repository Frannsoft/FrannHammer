using System;
using System.Linq;
using System.Web.Http.Results;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    [TestFixture]
    public class MoveControllerTest : BaseControllerTest
    {

        [Test]
        public void ShouldGetMove()
        {
            var move = TestObjects.Move();
            MovesController.PostMove(move);

            var result = MovesController.GetMove(move.Id) as OkNegotiatedContentResult<Move>;

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

            var updatedMove = MovesController.GetMove(move.Id) as OkNegotiatedContentResult<Move>;

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

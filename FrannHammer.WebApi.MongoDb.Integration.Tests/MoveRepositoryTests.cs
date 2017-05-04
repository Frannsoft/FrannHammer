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
    public class MoveRepositoryTests : BaseControllerTests
    {
        private MoveController _controller;

        public MoveRepositoryTests()
            : base(typeof(Move))
        { }

        [SetUp]
        public void SetUp()
        {
            _controller = new MoveController(new DefaultMoveService(new MongoDbRepository<IMove>(MongoDatabase)));
        }

        [Test]
        public void GetSingleMoveById()
        {
            var response = _controller.Get("5905f9a04696591ea4062d19") as OkNegotiatedContentResult<IMove>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var move = response.Content;
            AssertMoveIsValid(move);
        }

        private static void AssertMoveIsValid(IMove move)
        {
            Assert.That(move.Angle, Is.Not.Null);
            Assert.That(move.BaseDamage, Is.Not.Null);
            Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null);
            Assert.That(move.FirstActionableFrame, Is.Not.Null);
            Assert.That(move.HitboxActive, Is.Not.Null);
            Assert.That(move.Id, Is.Not.Null);
            Assert.That(move.KnockbackGrowth, Is.Not.Null);
            Assert.That(move.Name, Is.Not.Null);
            Assert.That(move.Owner, Is.Not.Null);
        }

        [Test]
        public void GetAllMoves()
        {
            var response = _controller.GetAll() as OkNegotiatedContentResult<IEnumerable<IMove>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var moves = response.Content.ToList();

            CollectionAssert.IsNotEmpty(moves);
            CollectionAssert.AllItemsAreUnique(moves);
            moves.ForEach(AssertMoveIsValid);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.DataAccess.MongoDb;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NUnit.Framework;

namespace FrannHammer.WebApi.MongoDb.Integration.Tests
{
    [TestFixture]
    public class MoveRepositoryTests
    {
        private IMongoDatabase _mongoDatabase;
        private MoveController _controller;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var classMap = new BsonClassMap(typeof(Move));
            var moveProperties =
                typeof(Move).GetProperties()
                    .Where(p => p.GetCustomAttribute<FriendlyNameAttribute>() != null);

            foreach (var prop in moveProperties)
            {
                classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
            }
            
            BsonClassMap.RegisterClassMap(classMap);
            var mongoClient = new MongoClient(new MongoUrl("mongodb://testuser:password@ds058739.mlab.com:58739/testfranndotexe"));
            _mongoDatabase = mongoClient.GetDatabase("testfranndotexe");

            _controller = new MoveController(new DefaultMoveService(new MongoDbRepository<IMove>(_mongoDatabase)));
        }

        [Test]
        public void GetSingleMoveById()
        {
            var response = _controller.GetMove(1) as OkNegotiatedContentResult<IMove>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var move = response.Content;
            AssertMoveIsValid(move);
        }

        private static void AssertMoveIsValid(IMove move)
        {
            Assert.That(move.Angle, Is.Not.Null);
            Assert.That(move.AutoCancel, Is.Not.Null);
            Assert.That(move.BaseDamage, Is.Not.Null);
            Assert.That(move.BaseKnockbackSetKnockback, Is.Not.Null);
            Assert.That(move.FirstActionableFrame, Is.Not.Null);
            Assert.That(move.HitboxActive, Is.Not.Null);
            Assert.That(move.Id, Is.GreaterThan(0));
            Assert.That(move.KnockbackGrowth, Is.Not.Null);
            Assert.That(move.Name, Is.Not.Null);
            Assert.That(move.OwnerId, Is.GreaterThan(0));
        }

        [Test]
        public void GetAllMoves()
        {
            var response = _controller.GetAllMoves() as OkNegotiatedContentResult<IEnumerable<IMove>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var moves = response.Content.ToList();

            CollectionAssert.IsNotEmpty(moves);
            CollectionAssert.AllItemsAreUnique(moves);
            moves.ForEach(AssertMoveIsValid);
        }
    }
}

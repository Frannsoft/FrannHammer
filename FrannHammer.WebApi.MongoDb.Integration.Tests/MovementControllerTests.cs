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
    public class MovementControllerTests
    {
        private IMongoDatabase _mongoDatabase;
        private MovementController _controller;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var classMap = new BsonClassMap(typeof(Movement));
            var movementProperties = typeof(Movement).GetProperties().Where(p => p.GetCustomAttribute<FriendlyNameAttribute>() != null);

            foreach (var prop in movementProperties)
            {
                classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
            }

            BsonClassMap.RegisterClassMap(classMap);

            var mongoClient = new MongoClient(new MongoUrl("mongodb://testuser:password@ds058739.mlab.com:58739/testfranndotexe"));
            _mongoDatabase = mongoClient.GetDatabase("testfranndotexe");

            _controller = new MovementController(new DefaultMovementService(new MongoDbRepository<IMovement>(_mongoDatabase)));
        }

        private static void AssertmovementIsValid(IMovement movement)
        {
            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Name, Is.Not.Null);
            Assert.That(movement.OwnerId, Is.Not.Null);
            Assert.That(movement.Value, Is.Not.Null);
            Assert.That(movement.Id, Is.GreaterThan(0));
        }

        [Test]
        public void GetSingleMovement()
        {
            var response = _controller.GetMovement(1) as OkNegotiatedContentResult<IMovement>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var movement = response.Content;
            AssertmovementIsValid(movement);
        }

        [Test]
        public void GetAllMovements()
        {
            var response = _controller.GetAllMovements() as OkNegotiatedContentResult<IEnumerable<IMovement>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var movements = response.Content.ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.IsNotEmpty(movements);
            CollectionAssert.AllItemsAreUnique(movements);
            movements.ForEach(AssertmovementIsValid);
        }
    }
}

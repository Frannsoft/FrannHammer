using System;
using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture]
    public class MovementRepositoryTests : BaseRepositoryTests
    {
        private IRepository<IMovement> _repository;

        public MovementRepositoryTests()
            : base(typeof(Movement))
        { }

        [Test]
        public void GetSingleMovement()
        {
            _repository = new MongoDbRepository<IMovement>(MongoDatabase);

            var newlyAddedMovement = _repository.Add(new Movement { Name = "one", Value = "test" });

            var movement = _repository.Get(newlyAddedMovement.Id);

            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Value, Is.Not.Null);
        }

        [Test]
        public void GetAllMovements()
        {
            _repository = new MongoDbRepository<IMovement>(MongoDatabase);

            _repository.Add(new Movement { Name = "one" });
            _repository.Add(new Movement { Name = "two" });

            var movements = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.IsNotEmpty(movements);
            CollectionAssert.AllItemsAreUnique(movements);
        }

        [Test]
        public void AddAndRemoveSingleMovement()
        {
            _repository = new MongoDbRepository<IMovement>(MongoDatabase);

            int previousCount = _repository.GetAll().Count();

            var newMovement = new Movement
            {
                Id = "99999",
                Name = "test"
            };

            _repository.Add(newMovement);

            var newlyAddedMovement = _repository.GetAll().Last();

            int newCount = _repository.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(newlyAddedMovement.Id);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

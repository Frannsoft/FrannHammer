﻿using AutoFixture;
using AutoFixture.Kernel;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture]
    public class MovementRepositoryTests : BaseRepositoryTests
    {
        private IRepository<IMovement> _repository;

        public MovementRepositoryTests()
            : base(typeof(Movement))
        { }

        [SetUp]
        public void SetUp()
        {
            Fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IMovement),
                    typeof(Movement)));
        }

        [Test]
        public void GetSingleMovement()
        {
            _repository = new MongoDbRepository<IMovement>(MongoDatabase);

            var newlyAddedMovement = _repository.Add(Fixture.Create<IMovement>());

            var movement = _repository.GetSingleWhere(m => m.InstanceId == newlyAddedMovement.InstanceId);

            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Value, Is.Not.Null);
        }

        [Test]
        public void GetAllWhere_MovementDoesNotExist_ThrowsException()
        {
            _repository = new MongoDbRepository<IMovement>(MongoDatabase);

            Assert.Throws<ResourceNotFoundException>(() =>
            {
                _repository.GetAllWhere(m => m.Owner == "doesnotexist");
            });
        }

        [Test]
        public void GetAllMovements()
        {
            _repository = new MongoDbRepository<IMovement>(MongoDatabase);

            _repository.Add(Fixture.Create<IMovement>());
            _repository.Add(Fixture.Create<IMovement>());

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

            var newMovement = Fixture.Create<IMovement>();

            _repository.Add(newMovement);

            var newlyAddedMovement = _repository.GetAll().Last();

            int newCount = _repository.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(newlyAddedMovement.InstanceId);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

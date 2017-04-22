﻿using System;
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
        protected override Type ModelType => typeof(Movement);
        private IRepository<IMovement> _repository;

        [Test]
        public void GetSingleMovement()
        {
            _repository = new MongoDbRepository<IMovement>(MongoDatabase);

            var movement = _repository.Get(1);
            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Id, Is.GreaterThan(0));
            Assert.That(movement.OwnerId, Is.GreaterThan(0));
            Assert.That(movement.Value, Is.Not.Null);
        }

        [Test]
        public void GetAllMovements()
        {
            _repository = new MongoDbRepository<IMovement>(MongoDatabase);

            var movements = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.IsNotEmpty(movements);
            CollectionAssert.AllItemsAreUnique(movements);
        }
    }
}

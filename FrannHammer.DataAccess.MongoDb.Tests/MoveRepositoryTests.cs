using System;
using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture]
    public class MoveRepositoryTests : BaseRepositoryTests
    {
        protected override Type ModelType => typeof(Move);
        private IRepository<IMove> _repository;

        [Test]
        public void GetSingleMove()
        {
            _repository = new MongoDbRepository<IMove>(MongoDatabase);

            var move = _repository.Get(1);

            Assert.That(move, Is.Not.Null);
            Assert.That(move.Id, Is.GreaterThan(0));
            Assert.That(move.OwnerId, Is.GreaterThan(0));
            Assert.That(move.Angle, Is.Not.Null);
            Assert.That(move.AutoCancel, Is.Not.Null);
            Assert.That(move.BaseDamage, Is.Not.Null);
            Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null);
            Assert.That(move.FirstActionableFrame, Is.Not.Null);
            Assert.That(move.HitboxActive, Is.Not.Null);
            Assert.That(move.KnockbackGrowth, Is.Not.Null);
            Assert.That(move.MoveType, Is.Not.Null);
            Assert.That(move.KnockbackGrowth, Is.Not.Null);
        }

        [Test]
        public void GetAllMoves()
        {
            _repository = new MongoDbRepository<IMove>(MongoDatabase);

            var moves = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(moves);
            CollectionAssert.IsNotEmpty(moves);
            CollectionAssert.AllItemsAreUnique(moves);
        }
    }
}

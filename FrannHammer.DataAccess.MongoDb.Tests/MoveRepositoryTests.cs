using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    [TestFixture]
    public class MoveRepositoryTests : BaseRepositoryTests
    {
        private IRepository<IMove> _repository;

        public MoveRepositoryTests()
            : base(typeof(Move))
        { }

        [SetUp]
        public void SetUp()
        {
            Fixture.Customizations.Add(
                new TypeRelay(
                typeof(IMove),
                typeof(Move)));
        }

        [Test]
        public void GetSingleMove()
        {
            _repository = new MongoDbRepository<IMove>(MongoDatabase);

            var newlyAddedMove = _repository.Add(Fixture.Create<IMove>());

            var move = _repository.GetSingleWhere(m => m.InstanceId == newlyAddedMove.InstanceId);

            Assert.That(move, Is.Not.Null);
            Assert.That(move.InstanceId, Is.Not.Null);
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

            _repository.Add(Fixture.Create<IMove>());
            _repository.Add(Fixture.Create<IMove>());

            var moves = _repository.GetAll().ToList();

            CollectionAssert.AllItemsAreNotNull(moves);
            CollectionAssert.IsNotEmpty(moves);
            CollectionAssert.AllItemsAreUnique(moves);
        }

        [Test]
        public void AddAndRemoveSingleMove()
        {
            _repository = new MongoDbRepository<IMove>(MongoDatabase);

            int previousCount = _repository.GetAll().Count();

            var newMove = Fixture.Create<IMove>();

            _repository.Add(newMove);

            int newCount = _repository.GetAll().Count();

            Assert.That(newCount, Is.EqualTo(previousCount + 1));

            _repository.Delete(newMove.InstanceId);

            Assert.That(_repository.GetAll().Count(), Is.EqualTo(previousCount));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    public class MovementIntegrityTest : BaseDataIntegrityTest
    {
        private List<Movement> _movements;


        [SetUp]
        public void SetUp()
        {
            _movements = Context.Movements.ToList();
        }

        [Test]
        public void AllMovementDataHasOwnerId() => _movements.ForEach(m => Assert.That(m.OwnerId > 0));

        [Test]
        public void AllMovementDataHasValue() => _movements.ForEach(m => Assert.That(!string.IsNullOrEmpty(m.Value)));

        [Test]
        public void AllMovementDataHasName() => _movements.ForEach(m => Assert.That(!string.IsNullOrEmpty(m.Name)));
    }
}

using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    public class ThrowsIntegrityTest : BaseDataIntegrityTest
    {
        private List<Throw> _throws;

        [SetUp]
        public void SetUp()
        {
            _throws = Context.Throws.ToList();
        }

        [Test]
        public void ThrowCountIsExpected()
        {
            Assert.That(_throws.Count, Is.EqualTo(236));
        }

        [Test]
        public void EveryThrowHasThrowTypeId()
        {
            _throws.ForEach(t => Assert.That(t.ThrowTypeId > 0));
        }

        [Test]
        public void EveryThrowHasMoveId()
        {
            _throws.ForEach(t => Assert.That(t.MoveId > 0));
        }
    }
}

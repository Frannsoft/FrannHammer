using System.Linq;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    public class MovesIntegrityTest : BaseDataIntegrityTest
    {
        [Test]
        public void MovesCountIsExpectedAmount()
        {
            var moves = Context.Moves;

            Assert.That(moves.ToList().Count, Is.EqualTo(2841));
        }

        [Test]
        public void EveryMoveHasName()
        {
            var moves = Context.Moves.ToList();

           moves.ForEach(m => Assert.That(!string.IsNullOrEmpty(m.Name)));
        }

        [Test]
        public void EveryMoveHasOwnerId()
        {
            var moves = Context.Moves.ToList();

            moves.ForEach(m => Assert.That(m.OwnerId > 0));
        }
    }
}

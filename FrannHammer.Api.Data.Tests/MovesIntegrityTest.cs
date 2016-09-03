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

            Assert.That(moves.ToList().Count, Is.EqualTo(2795));
        }
    }
}

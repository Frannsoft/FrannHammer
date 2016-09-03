using System.Linq;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    public class ThrowsIntegrityTest : BaseDataIntegrityTest
    {
        [Test]
        public void ThrowCountIsExpected()
        {
            var throws = Context.Throws.ToList();

            Assert.That(throws.Count, Is.EqualTo(236));
        }
    }
}

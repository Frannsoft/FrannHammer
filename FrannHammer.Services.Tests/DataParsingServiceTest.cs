using FrannHammer.Services.MoveSearch;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class DataParsingServiceTest
    {
        private DataParsingService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new DataParsingService();
        }

        [Test]
        public void SingleValueSingleHitbox()
        {
            const int expectedCount = 1;

            var numberRanges = _service.Parse("5");

            Assert.That(numberRanges.Count == expectedCount);

            foreach (var range in numberRanges)
            {
                Assert.That(range.Start, Is.EqualTo(5));
                Assert.That(!range.End.HasValue);
            }
        }

        [Test]
        public void SingleValueMultipleHitbox()
        {
            const int expectedCount = 2;

            var numberRanges = _service.Parse("5,7");

            Assert.That(numberRanges.Count == expectedCount);

            //first range
            Assert.That(numberRanges[0].Start, Is.EqualTo(5));
            Assert.That(!numberRanges[0].End.HasValue);

            //second range
            Assert.That(numberRanges[1].Start, Is.EqualTo(7));
            Assert.That(!numberRanges[1].End.HasValue);
        }

        [Test]
        public void MultiValueSingleHitbox()
        {
            const int expectedCount = 1;

            var numberRanges = _service.Parse("5-7");

            Assert.That(numberRanges.Count == expectedCount);
            Assert.That(numberRanges[0].Start, Is.EqualTo(5));
            Assert.That(numberRanges[0].End.HasValue);
            Assert.That(numberRanges[0].End.Value, Is.EqualTo(7));
        }

        [Test]
        public void MultiValueMultipleHitbox()
        {
            const int expectedCount = 3;

            var numberRanges = _service.Parse("5-7,9-11,13-17");

            Assert.That(numberRanges.Count == expectedCount);

            //first range
            Assert.That(numberRanges[0].Start == 5);
            Assert.That(numberRanges[0].End.HasValue);
            Assert.That(numberRanges[0].End.Value, Is.EqualTo(7));

            //second range
            Assert.That(numberRanges[1].Start == 9);
            Assert.That(numberRanges[1].End.HasValue);
            Assert.That(numberRanges[1].End.Value, Is.EqualTo(11));

            //third range
            Assert.That(numberRanges[2].Start == 13);
            Assert.That(numberRanges[2].End.HasValue);
            Assert.That(numberRanges[2].End.Value, Is.EqualTo(17));
        }
    }
}

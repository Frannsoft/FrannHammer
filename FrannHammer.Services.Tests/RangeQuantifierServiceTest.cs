using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class RangeQuantifierServiceTest
    {
        private RangeQuantifierService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new RangeQuantifierService();
        }

        [Test]
        public void ReturnsTrueInBetweenRange()
        {
            Assert.That(_service.IsBetween(5, 10, new NumberRange(1, 15)), "Number was marked as not inbetween range");
        }

        [Test]
        public void ReturnsFalseForNumberOutOfExpectedInBetweenRange()
        {
            Assert.That(!_service.IsBetween(10, 5, new NumberRange(15)), "Number was marked as in between range");
        }

        [Test]
        public void ReturnsTrueForNumberIsGreaterThanWhenExpected()
        {
            Assert.That(_service.IsGreaterThan(15, 10), "Number was marked as less than boundary");
        }

        [Test]
        public void ReturnsFalseForNumberIsGreaterThanWhenExpected()
        {
            Assert.That(_service.IsGreaterThan(10, 5), "Number was marked as greater than boundary");
        }

        [Test]
        public void ReturnsTrueForTwoEqualIntegers()
        {
            Assert.That(_service.IsEqualTo(4, 4), "Numbers were marked as not equal");
        }

        [Test]
        public void ReturnsFalseForTwoInequalIntegers()
        {
            Assert.That(!_service.IsEqualTo(3, 4), "Numbers were marked as not equal");
        }
    }
}

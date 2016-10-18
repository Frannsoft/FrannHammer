using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class HitboxStartupSearchPredicateServiceTest
    {
        private HitboxStartupSearchPredicateService _service;
        private const string UnableToFindValueMessage = "Unable to find value in hitbox range";
        private const string FoundValueMessage = "Found value in hitbox range";

        [SetUp]
        public void SetUp()
        {
            _service = new HitboxStartupSearchPredicateService();
        }

        [Test]
        public void DoesNotFindHitboxStartupFrameInMessyHitbox()
        {
            const int valueUnderTest = 1;

            Assert.That(!_service.IsValueInRange("54-197 (Tackle), 258 (Generated on: Frame 13)", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 20
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsHitboxStartupFrameInBetweenStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 3,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 6
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsHitboxStartupFrameIsLessThanStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.LessThan
            }), UnableToFindValueMessage);
        }

        [Test]
        [TestCase(6)]
        [TestCase(5)]
        public void FindsHitboxStartupFrameIsLessThanOrEqualToStandardHitbox(int startValue)
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.LessThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        [TestCase(5)]
        [TestCase(4)]
        public void FindsHitboxStartupFrameIsGreaterThanOrEqualToStandardHitbox(int startValue)
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotIncludeBoundariesInDefinitionOfBetween()
        {
            Assert.That(!_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 15
            }), FoundValueMessage);
        }

        [Test]
        public void FindsHitboxStartupFrameEqualToStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsHitboxStartupFrameGreaterThanStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.GreaterThan
            }), UnableToFindValueMessage);
        }


        [Test]
        public void FindsHitboxStartupFrameInSingleFrameHitbox()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 3,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 6
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsHitboxStartupFrameInCommaSeperatedHitboxes()
        {
            Assert.That(_service.IsValueInRange("5,8,11", new RangeModel
            {
                StartValue = 3,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 6
            }), UnableToFindValueMessage);
        }
    }
}

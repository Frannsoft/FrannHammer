using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class HitboxActiveSearchPredicateServiceTest
    {
        private HitboxActiveSearchPredicateService _service;
        private const string UnableToFindValueMessage = "Unable to find value in hitbox range";
        private const string FoundValueMessage = "Found value in hitbox range";

        [SetUp]
        public void SetUp()
        {
            _service = new HitboxActiveSearchPredicateService();
        }

        [Test]
        public void FindsValidHitboxActiveFrameInBetween()
        {
            Assert.That(_service.IsValueInRange("3-15", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 10
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidHitboxActiveFrameInMessyHitbox()
        {
            const int valueUnderTest = 5;

            Assert.That(!_service.IsValueInRange("54-197 (Tackle), 258 (Generated on: Frame 13)", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidHitboxActiveFrame()
        {
            Assert.That(_service.IsValueInRange("3-10", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsHitboxActiveFrameEqualToStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsHitboxActiveFrameGreaterThanStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.GreaterThan
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsHitboxActiveFrameInBetweenStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 12
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
        [TestCase(6)]
        [TestCase(5)]
        public void FindsHitboxActiveFrameIsLessThanOrEqualToStandardHitbox(int startValue)
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.LessThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsHitboxActiveFrameIsLessThanStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.LessThan
            }), UnableToFindValueMessage);
        }

        [Test]
        [TestCase(7)]
        [TestCase(6)]
        [TestCase(5)]
        public void FindsHitboxActiveFrameIsGreaterThanOrEqualToStandardHitbox(int startValue)
        {
            Assert.That(_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        public void ShouldNotFindHitboxActiveFrameWhenGreaterThanOrEqualTo()
        {
            Assert.That(!_service.IsValueInRange("5-15", new RangeModel
            {
                StartValue = 16,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidHitboxActiveFrameForSingleFrameHitbox()
        {
            const int valueUnderTest = 5;

            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotFindValidHitboxActiveFrameIfOutsideRange()
        {
            const int valueUnderTest = 5;

            Assert.That(!_service.IsValueInRange("13-20", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), FoundValueMessage);
        }
    }
}

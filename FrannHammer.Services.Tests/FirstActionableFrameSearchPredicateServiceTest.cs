using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class FirstActionableFrameSearchPredicateServiceTest
    {
        private FirstActionableFrameSearchPredicateService _service;
        private const string UnableToFindValueMessage = "Unable to find value in first actionable frame range";
        private const string FoundValueMessage = "Found value in first actionable frame range";

        [SetUp]
        public void SetUp()
        {
            _service = new FirstActionableFrameSearchPredicateService();
        }

        [Test]
        public void FindsValidFirstActionableFrameInBetween()
        {
            Assert.That(_service.IsValueInRange("3", new RangeModel
            {
                StartValue = 2,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 10
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidFirstActionableFrameInMessyHitbox()
        {
            const int valueUnderTest = 5;

            Assert.That(!_service.IsValueInRange("54-197 (Tackle), 258 (Generated on: Frame 13)", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidFirstActionableFrame()
        {
            Assert.That(_service.IsValueInRange("3", new RangeModel
            {
                StartValue = 3,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsFirstActionableFrameEqualToStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsFirstActionableFrameGreaterThanStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.GreaterThan
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsFirstActionableFrameInBetweenStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 12
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsFirstActionableFrameInBetween()
        {
            Assert.That(!_service.IsValueInRange("46", new RangeModel
            {
                StartValue = 1,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 40
            }), FoundValueMessage);
        }

        [Test]
        public void DoesNotIncludeBoundariesInDefinitionOfBetween()
        {
            Assert.That(!_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 15
            }), FoundValueMessage);
        }

        [Test]
        [TestCase(6)]
        [TestCase(5)]
        public void FindsFirstActionableFrameIsLessThanOrEqualToStandardHitbox(int startValue)
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.LessThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsFirstActionableFrameIsLessThanStandardHitbox()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.LessThan
            }), UnableToFindValueMessage);
        }

        [Test]
        [TestCase(4)]
        [TestCase(3)]
        [TestCase(5)]
        public void FindsFirstActionableFrameIsGreaterThanOrEqualTo(int startValue)
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        public void ShouldNotFindFirstActionableFrameWhenGreaterThanOrEqualTo()
        {
            Assert.That(!_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 16,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidFirstActionableFrameForSingleFrameHitbox()
        {
            const int valueUnderTest = 5;

            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotFindValidFirstActionableFrameIfOutsideRange()
        {
            const int valueUnderTest = 5;

            Assert.That(!_service.IsValueInRange("13", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), FoundValueMessage);
        }
    }
}

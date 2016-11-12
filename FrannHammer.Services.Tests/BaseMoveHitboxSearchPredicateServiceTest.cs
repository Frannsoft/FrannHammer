using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class BaseMoveHitboxSearchPredicateServiceTest
    {
        private SearchPredicateService _service;
        private const string UnableToFindValueMessage = "Unable to find value in range";
        private const string FoundValueMessage = "Found value in range";

        [SetUp]
        public void SetUp()
        {
            _service = new SearchPredicateService();
        }

        [Test]
        public void FindsValidBaseDamageFrameInBetween()
        {
            Assert.That(_service.IsValueInRange("7", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 10
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidBaseDamageFrameInMessyBaseDamage()
        {
            const int valueUnderTest = 5;

            Assert.That(!_service.IsValueInRange("54-197 (Tackle), 258 (Generated on: Frame 13)", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidBaseDamageFrame()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsBaseDamageFrameEqualToStandardBaseDamage()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsBaseDamageFrameGreaterThanStandardBaseDamage()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.GreaterThan
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsBaseDamageFrameInBetweenStandardBaseDamage()
        {
            Assert.That(_service.IsValueInRange("7", new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 12
            }), UnableToFindValueMessage);
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
        public void FindsBaseDamageFrameIsLessThanOrEqualToStandardBaseDamage(int startValue)
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.LessThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsBaseDamageFrameIsLessThanStandardBaseDamage()
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.LessThan
            }), UnableToFindValueMessage);
        }

        [Test]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void FindsBaseDamageFrameIsGreaterThanOrEqualToStandardBaseDamage(int startValue)
        {
            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        public void ShouldNotFindBaseDamageFrameWhenGreaterThanOrEqualTo()
        {
            Assert.That(!_service.IsValueInRange("15", new RangeModel
            {
                StartValue = 16,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidBaseDamageFrameForSingleFrameBaseDamage()
        {
            const int valueUnderTest = 5;

            Assert.That(_service.IsValueInRange("5", new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotFindValidBaseDamageFrameIfOutsideRange()
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

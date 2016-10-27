using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class LandingLagSearchPredicateServiceTest
    {
        private LandingLagSearchPredicateService _service;
        private const string UnableToFindValueMessage = "Unable to find value in range";
        private const string FoundValueMessage = "Found value in range";

        [SetUp]
        public void SetUp()
        {
            _service = new LandingLagSearchPredicateService();
        }

        [Test]
        public void FindsValidLandingLagFrameInBetween()
        {
            const int valueUnderTest = 7;

            var model = new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 10
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(predicate(new LandingLag
            {
                Frames = valueUnderTest
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidLandingLagFrame()
        {
            var model = new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);


            Assert.That(predicate(new LandingLag
            {
                Frames = 5
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsLandingLagFrameEqualToStandardLandingLag()
        {
            var model = new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            };

            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(predicate(new LandingLag
            {
                Frames = 5
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsLandingLagFrameGreaterThanStandardLandingLag()
        {
            var model = new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.GreaterThan
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(predicate(new LandingLag
            {
                Frames = 5
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsLandingLagFrameInBetweenStandardLandingLag()
        {
            var model = new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 12
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(predicate(new LandingLag
            {
                Frames = 7
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotIncludeBoundariesInDefinitionOfBetween()
        {
            var model = new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 15
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(!predicate(new LandingLag
            {
                Frames = 5
            }), FoundValueMessage);
        }

        [Test]
        [TestCase(6)]
        [TestCase(5)]
        public void FindsLandingLagFrameIsLessThanOrEqualToStandardLandingLag(int startValue)
        {
            var model = new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.LessThanOrEqualTo
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(predicate(new LandingLag
            {
                Frames = 5
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsLandingLagFrameIsLessThanStandardLandingLag()
        {
            var model = new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.LessThan
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(predicate(new LandingLag
            {
                Frames = 5
            }), UnableToFindValueMessage);
        }

        [Test]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void FindsLandingLagFrameIsGreaterThanOrEqualToStandardLandingLag(int startValue)
        {
            var model = new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(predicate(new LandingLag
            {
                Frames = 5
            }), UnableToFindValueMessage);
        }

        public void ShouldNotFindLandingLagFrameWhenGreaterThanOrEqualTo()
        {
            var model = new RangeModel
            {
                StartValue = 16,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(!predicate(new LandingLag
            {
                Frames = 15
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidLandingLagFrameForSingleFrameLandingLag()
        {
            const int valueUnderTest = 5;
            var model = new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);

            Assert.That(predicate(new LandingLag
            {
                Frames = 5
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotFindValidLandingLagFrameIfOutsideRange()
        {
            const int valueUnderTest = 5;
            var model = new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            };
            var predicate = _service.GetLandingLagSearchPredicate(model);


            Assert.That(!predicate(new LandingLag
            {
                Frames = 13
            }), FoundValueMessage);
        }

    }
}

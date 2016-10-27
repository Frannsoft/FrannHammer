using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class AutoCancelSearchPredicateServiceTest
    {
        private AutoCancelSearchPredicateService _service;
        private const string UnableToFindValueMessage = "Unable to find value in range";
        private const string FoundValueMessage = "Found value in range";

        [SetUp]
        public void SetUp()
        {
            _service = new AutoCancelSearchPredicateService();
        }

        [Test]
        public void FindsValidAutoCancelFrameInBetween()
        {
            const string valueUnderTest = "7";

            var model = new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 10
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = valueUnderTest
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidAutoCancelFrameWithTwoCancelValuesStandard()
        {
            var model = new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 20
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);


            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5",
                Cancel2 = "15"
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidAutoCancelFrameWithTwoCancelValuesMixed()
        {
            var model = new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 20
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5",
                Cancel2 = "15-18"
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidAutoCancelFrame()
        {
            var model = new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5>",
                Cancel2 = "6"
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsAutoCancelFrameEqualToStandardAutoCancel()
        {
            var model = new RangeModel
            {
                StartValue = 5,
                RangeQuantifier = RangeQuantifier.EqualTo
            };

            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5"
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsAutoCancelFrameGreaterThanStandardAutoCancel()
        {
            var model = new RangeModel
            {
                StartValue = 4,
                RangeQuantifier = RangeQuantifier.GreaterThan
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5"
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsAutoCancelFrameInBetweenStandardAutoCancel()
        {
            var model = new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.Between,
                EndValue = 12
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "7"
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
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(!predicate(new Autocancel
            {
                Cancel1 = "5"
            }), FoundValueMessage);
        }

        [Test]
        [TestCase(6)]
        [TestCase(5)]
        public void FindsAutoCancelFrameIsLessThanOrEqualToStandardAutoCancel(int startValue)
        {
            var model = new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.LessThanOrEqualTo
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5"
            }), UnableToFindValueMessage);
        }

        [Test]
        public void FindsAutoCancelFrameIsLessThanStandardAutoCancel()
        {
            var model = new RangeModel
            {
                StartValue = 6,
                RangeQuantifier = RangeQuantifier.LessThan
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5"
            }), UnableToFindValueMessage);
        }

        [Test]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void FindsAutoCancelFrameIsGreaterThanOrEqualToStandardAutoCancel(int startValue)
        {
            var model = new RangeModel
            {
                StartValue = startValue,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5"
            }), UnableToFindValueMessage);
        }

        public void ShouldNotFindAutoCancelFrameWhenGreaterThanOrEqualTo()
        {
            var model = new RangeModel
            {
                StartValue = 16,
                RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(!predicate(new Autocancel
            {
                Cancel1 = "15"
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesFindValidAutoCancelFrameForSingleFrameAutoCancel()
        {
            const int valueUnderTest = 5;
            var model = new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(predicate(new Autocancel
            {
                Cancel1 = "5"
            }), UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotFindValidAutoCancelFrameIfOutsideRange()
        {
            const int valueUnderTest = 5;
            var model = new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            };
            var predicate = _service.GetAutoCancelSearchPredicate(model);

            Assert.That(!predicate(new Autocancel
            {
                Cancel1 = "13"
            }), FoundValueMessage);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;
using FrannHammer.Services.Tests.Harnesses;
using NUnit.Framework;
using System;

namespace FrannHammer.Services.Tests.MoveSearch
{
    [TestFixture]
    [LongRunning]
    public class MetadataServiceSearchTest : ServiceBaseTest
    {
        private IMetadataService _metadataService;
        private IMoveSearchHarness _moveSearchHarness;

        private static IEnumerable<RangeModel> Ranges()
        {
            yield return new RangeModel { StartValue = 5, RangeQuantifier = RangeConstraint.EqualTo };
            yield return new RangeModel { StartValue = 20, RangeQuantifier = RangeConstraint.EqualTo };
            yield return new RangeModel { StartValue = 10, RangeQuantifier = RangeConstraint.GreaterThan };
            yield return new RangeModel { StartValue = 10, RangeQuantifier = RangeConstraint.LessThan };
            yield return new RangeModel { StartValue = 20, RangeQuantifier = RangeConstraint.GreaterThanOrEqualTo };
            yield return new RangeModel { StartValue = 20, RangeQuantifier = RangeConstraint.LessThanOrEqualTo };
            yield return new RangeModel { StartValue = 10, RangeQuantifier = RangeConstraint.Between, EndValue = 20 };
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _metadataService = new MetadataService(Context, ResultValidationService);
            _moveSearchHarness = new MoveSearchHarness(string.Empty);
        }

        private IList<dynamic> AssertSearchResultsAreValid(MoveSearchModel searchModel, Action<List<dynamic>> countAssertion = null,
            string fields = "")
        {
            var results = _metadataService.GetAll<MoveDto>(searchModel, null).ToList();

            Assert.That(results, Is.Not.Null);

            if (countAssertion == null)
            {
                Assert.That(results.Count, Is.GreaterThan(0));
            }
            else
            {
                countAssertion(results);
            }

            HarnessAsserts.ExpandoObjectIsCorrect(results, string.IsNullOrEmpty(fields) ? $"{Id},{Name}" : $"{fields}");
            return results;
        }

        [Test]
        public void ReturnsAllMovesForCharacterWhenNoOtherAttributesSpecified()
        {
            var searchModel = new MoveSearchModel
            {
                CharacterName = "Ganondorf"
            };

            AssertSearchResultsAreValid(searchModel,
                    results => Assert.That(results.Count, Is.EqualTo(44)),
                    $"{Id},{Name}");
        }

        [Test]
        public void ReturnsSingleResultUsingMultipleSearchAttributes()
        {
            var searchModel = new MoveSearchModel
            {
                Angle = new RangeModel { StartValue = 10, RangeQuantifier = RangeConstraint.GreaterThan },
                AutoCancel = new RangeModel { StartValue = 5, RangeQuantifier = RangeConstraint.GreaterThanOrEqualTo },
                BaseDamage = new RangeModel { StartValue = 3, RangeQuantifier = RangeConstraint.LessThanOrEqualTo },
                BaseKnockback = new RangeModel { StartValue = 50, RangeQuantifier = RangeConstraint.GreaterThan },
                FirstActionableFrame = new RangeModel { StartValue = 30, RangeQuantifier = RangeConstraint.Between, EndValue = 40 },
                HitboxActiveLength = new RangeModel { StartValue = 1, RangeQuantifier = RangeConstraint.GreaterThan },
                HitboxActiveOnFrame = new RangeModel { StartValue = 2, RangeQuantifier = RangeConstraint.GreaterThan },
                HitboxStartupFrame = new RangeModel { StartValue = 3, RangeQuantifier = RangeConstraint.GreaterThanOrEqualTo },
                KnockbackGrowth = new RangeModel { StartValue = 30, RangeQuantifier = RangeConstraint.LessThanOrEqualTo },
                Name = "Fair 2",
                CharacterName = "Bayonetta"
            };

            AssertSearchResultsAreValid(searchModel,
                results => Assert.That(results.Count, Is.EqualTo(1)),
                $"{Id},{Name}");
        }

        [Test]
        public void ReturnsResultsForMultipleSearchAttributes()
        {
            var searchModel = new MoveSearchModel
            {
                Angle = new RangeModel { StartValue = 10, RangeQuantifier = RangeConstraint.GreaterThan },
                AutoCancel = new RangeModel { StartValue = 5, RangeQuantifier = RangeConstraint.GreaterThanOrEqualTo },
                BaseDamage = new RangeModel { StartValue = 3, RangeQuantifier = RangeConstraint.LessThanOrEqualTo },
                BaseKnockback = new RangeModel { StartValue = 50, RangeQuantifier = RangeConstraint.GreaterThan }
            };

            AssertSearchResultsAreValid(searchModel);
        }

        [Test]
        [TestCaseSource(nameof(Ranges))]
        public void ReturnsBaseDamageOnlyResult(RangeModel rangeModel)
        {
            var searchModel = new MoveSearchModel
            {
                BaseDamage = rangeModel
            };
            var service = new SearchPredicateService();
            var func = service.GetPredicate<BaseDamage>(rangeModel);

            _moveSearchHarness.SearchResultCollectionIsValid(searchModel, Context, func);
        }

        [Test]
        [TestCaseSource(nameof(Ranges))]
        public void ReturnsHitboxActiveLengthOnlyResult(RangeModel rangeModel)
        {
            var searchModel = new MoveSearchModel
            {
                HitboxActiveLength = rangeModel
            };

            var service = new HitboxActiveLengthSearchPredicateService();
            var func = service.GetHitboxActiveLengthPredicate(rangeModel);

            _moveSearchHarness.SearchResultCollectionIsValid(searchModel, Context, func);
        }

        [Test]
        [TestCaseSource(nameof(Ranges))]
        public void ReturnsAngleOnlyResult(RangeModel rangeModel)
        {
            var searchModel = new MoveSearchModel
            {
                Angle = rangeModel
            };

            var searchFunc = new SearchPredicateService().GetPredicate<Angle>(rangeModel);

            _moveSearchHarness.SearchResultCollectionIsValid(searchModel, Context, searchFunc);
        }

        [Test]
        [TestCase("jab")]
        [TestCase("Dair")]
        [TestCase("jab 1")]
        [TestCase("Jab")]
        [TestCase(" Ftilt 2 ")]
        public void ReturnsNameOnlyResult(string valueUnderTest)
        {
            var model = new MoveSearchModel
            {
                Name = valueUnderTest
            };

            var results = AssertSearchResultsAreValid(model);

            foreach (var result in results)
            {
                Assert.That(result.Name.ToLower().Contains(valueUnderTest.Trim().ToLower()));
            }
        }

        [Test]
        [TestCase("mario")]
        [TestCase("Ganondorf")]
        [TestCase("Dr. Mario")]
        [TestCase("Bowser jr")]
        [TestCase(" Bowser Jr ")]
        public void ReturnsCharacterNameOnlyResult(string valueUnderTest)
        {
            var model = new MoveSearchModel
            {
                CharacterName = valueUnderTest
            };

            var results = AssertSearchResultsAreValid(model);

            foreach (var result in results)
            {
                int moveOwnerId = result.OwnerId;
                var characterName = Context.Characters.Single(c => c.Id == moveOwnerId).DisplayName;
                Assert.That(characterName.ToLower().Contains(characterName.Trim().ToLower()));
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;
using FrannHammer.Services.Tests.Harnesses;
using NUnit.Framework;

namespace FrannHammer.Services.Tests.MoveSearch
{
    [TestFixture]
    public class MetadataServiceSearchTest : ServiceBaseTest
    {
        private IMetadataService _metadataService;
        private IMoveSearchHarness _moveSearchHarness;

        private static IEnumerable<RangeModel> Ranges()
        {
            yield return new RangeModel { StartValue = 5, RangeQuantifier = RangeQuantifier.EqualTo };
            yield return new RangeModel { StartValue = 20, RangeQuantifier = RangeQuantifier.EqualTo };
            yield return new RangeModel { StartValue = 10, RangeQuantifier = RangeQuantifier.GreaterThan };
            yield return new RangeModel { StartValue = 10, RangeQuantifier = RangeQuantifier.LessThan };
            yield return new RangeModel { StartValue = 20, RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo };
            yield return new RangeModel { StartValue = 20, RangeQuantifier = RangeQuantifier.LessThanOrEqualTo };
            yield return new RangeModel { StartValue = 10, RangeQuantifier = RangeQuantifier.Between, EndValue = 20 };
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _metadataService = new MetadataService(Context, ResultValidationService);
            _moveSearchHarness = new MoveSearchHarness(string.Empty);
        }

        [Test]
        public void ReturnsResultsForMultipleSearchAttributes()
        {
            var searchModel = new ComplexMoveSearchModel
            {
                Angle = new RangeModel { StartValue = 10, RangeQuantifier = RangeQuantifier.GreaterThan },
                AutoCancel = new RangeModel { StartValue = 5, RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo },
                BaseDamage = new RangeModel { StartValue = 3, RangeQuantifier = RangeQuantifier.LessThanOrEqualTo },
                BaseKnockback = new RangeModel { StartValue = 50, RangeQuantifier = RangeQuantifier.GreaterThan }
            };

            var results = _metadataService.GetAll<MoveDto>(searchModel).ToList();

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.GreaterThan(0));
            HarnessAsserts.ExpandoObjectIsCorrect(results, $"{Id},{Name}");
        }

        [Test]
        [TestCaseSource(nameof(Ranges))]
        public void ReturnsBaseDamageOnlyResult(RangeModel rangeModel)
        {
            var searchModel = new ComplexMoveSearchModel
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
            var searchModel = new ComplexMoveSearchModel
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
            var searchModel = new ComplexMoveSearchModel
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
            var model = new ComplexMoveSearchModel
            {
                Name = valueUnderTest
            };

            var results = _metadataService.GetAll<MoveDto>(model).ToList();

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.GreaterThan(0));
            HarnessAsserts.ExpandoObjectIsCorrect(results, $"{Id},{Name}");

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
            var model = new ComplexMoveSearchModel
            {
                CharacterName = valueUnderTest
            };

            var results = _metadataService.GetAll<MoveDto>(model).ToList();

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.GreaterThan(0));
            HarnessAsserts.ExpandoObjectIsCorrect(results, $"{Id},{Name}");

            foreach (var result in results)
            {
                int moveOwnerId = result.OwnerId;
                var characterName = Context.Characters.Single(c => c.Id == moveOwnerId).DisplayName;
                Assert.That(characterName.ToLower().Contains(characterName.Trim().ToLower()));
            }
        }
    }
}

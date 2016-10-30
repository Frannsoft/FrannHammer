using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services.Tests.Harnesses;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class MetadataServiceSearchTest : ServiceBaseTest
    {
        private IMetadataService _metadataService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _metadataService = new MetadataService(Context, ResultValidationService);
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

        [Test]
        [TestCase(5)]
        public void ReturnsHitboxActiveLengthOnlyResult(int valueUnderTest)
        {
            var rangeModel = new RangeModel
            {
                StartValue = valueUnderTest,
                RangeQuantifier = RangeQuantifier.EqualTo
            };

            var searchModel = new ComplexMoveSearchModel
            {
                HitboxActiveLength = rangeModel
            };

            var results = _metadataService.GetAll<MoveDto>(searchModel).ToList();

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.GreaterThan(0));
            HarnessAsserts.ExpandoObjectIsCorrect(results, $"{Id},{Name}");

            var service = new HitboxActiveLengthSearchPredicateService();
            foreach (var result in results)
            {
                var func = service.GetHitboxActiveLengthPredicate(rangeModel);

                int moveId = result.Id;
                string moveName = result.Name;
                var thisMovesHitboxes = Context.Hitbox.Where(h => h.MoveId == moveId);

                foreach (var hitbox in thisMovesHitboxes)
                {
                    Assert.That(func(hitbox), $"Matching hitboxes of {moveName} were not able to be found!");
                }
            }
        }

        [Test]
        public void ReturnsBaseDamageOnlyResult()
        {
            var rangeModel = new RangeModel
            {
                StartValue = 20,
                RangeQuantifier = RangeQuantifier.EqualTo
            };

            var searchModel = new ComplexMoveSearchModel
            {
                BaseDamage = rangeModel
            };

            var results = _metadataService.GetAll<MoveDto>(searchModel).ToList();

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.GreaterThan(0));
            HarnessAsserts.ExpandoObjectIsCorrect(results, $"{Id},{Name}");

            var service = new BaseDamageSearchPredicateService();
            foreach (var result in results)
            {
                var func = service.GetBaseDamagePredicate(rangeModel);

                int moveId = result.Id;
                string moveName = result.Name;
                var thisMovesBaseDamages = Context.BaseDamage.Where(h => h.MoveId == moveId);

                foreach (var baseDamage in thisMovesBaseDamages)
                {
                    Assert.That(func(baseDamage), $"Matching base damages of {moveName} were not able to be found!");
                }
            }
        }
    }
}

using FrannHammer.Models;
using FrannHammer.Services.MoveSearch;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FrannHammer.Services.Tests.MoveSearch
{
    [TestFixture]
    public class MoveSearchModelRedisServiceTest
    {
        [Test]
        public void ConvertsToExpectedRedisKey()
        {
            var searchModel = new MoveSearchModel
            {
                Angle = new RangeModel { StartValue = 10, RangeQuantifier = RangeConstraint.GreaterThan },
                AutoCancel = new RangeModel { StartValue = 5, RangeQuantifier = RangeConstraint.GreaterThanOrEqualTo },
                BaseDamage = new RangeModel { StartValue = 3, RangeQuantifier = RangeConstraint.LessThanOrEqualTo },
                BaseKnockback = new RangeModel { StartValue = 50, RangeQuantifier = RangeConstraint.GreaterThan },
                FirstActionableFrame =
                    new RangeModel { StartValue = 30, RangeQuantifier = RangeConstraint.Between, EndValue = 40 },
                HitboxActiveLength = new RangeModel { StartValue = 1, RangeQuantifier = RangeConstraint.GreaterThan },
                HitboxActiveOnFrame = new RangeModel { StartValue = 2, RangeQuantifier = RangeConstraint.GreaterThan },
                HitboxStartupFrame =
                    new RangeModel { StartValue = 3, RangeQuantifier = RangeConstraint.GreaterThanOrEqualTo },
                KnockbackGrowth = new RangeModel { StartValue = 30, RangeQuantifier = RangeConstraint.LessThanOrEqualTo },
                Name = "jab 1",
                CharacterName = "mario"
            };

            string expected = JsonConvert.SerializeObject(searchModel);

            var service = new MoveSearchModelRedisService();

            var actualRedisKey = service.MoveSearchModelToRedisKey(searchModel);

            Assert.That(actualRedisKey, Is.EqualTo(expected));
        }
    }
}

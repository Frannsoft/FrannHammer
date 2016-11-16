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
            //string expected =
            //    $"{nameof(MoveSearchModel.Name)}:jab 1;" +
            //    $"{nameof(MoveSearchModel.CharacterName)}:mario;" +
            //    $"{nameof(MoveSearchModel.Angle)}:{nameof(RangeModel.StartValue)}:10,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.GreaterThan)},{nameof(RangeModel.EndValue)}:0;" +
            //    $"{nameof(MoveSearchModel.AutoCancel)}:{nameof(RangeModel.StartValue)}:5,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.GreaterThanOrEqualTo)},{nameof(RangeModel.EndValue)}:0;" +
            //    $"{nameof(MoveSearchModel.BaseDamage)}:{nameof(RangeModel.StartValue)}:3,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.LessThanOrEqualTo)},{nameof(RangeModel.EndValue)}:0;" +
            //    $"{nameof(MoveSearchModel.BaseKnockback)}:{nameof(RangeModel.StartValue)}:50,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.GreaterThan)},{nameof(RangeModel.EndValue)}:0;" +
            //    $"{nameof(MoveSearchModel.FirstActionableFrame)}:{nameof(RangeModel.StartValue)}:30,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.Between)},{nameof(RangeModel.EndValue)}:40;" +
            //    $"{nameof(MoveSearchModel.HitboxActiveLength)}:{nameof(RangeModel.StartValue)}:1,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.GreaterThan)},{nameof(RangeModel.EndValue)}:0;" +
            //    $"{nameof(MoveSearchModel.HitboxActiveOnFrame)}:{nameof(RangeModel.StartValue)}:2,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.GreaterThan)},{nameof(RangeModel.EndValue)}:0;" +
            //    $"{nameof(MoveSearchModel.HitboxStartupFrame)}:{nameof(RangeModel.StartValue)}:3,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.GreaterThanOrEqualTo)},{nameof(RangeModel.EndValue)}:0;" +
            //    $"{nameof(MoveSearchModel.KnockbackGrowth)}:{nameof(RangeModel.StartValue)}:30,{nameof(RangeModel.RangeQuantifier)}:{nameof(RangeQuantifier.LessThanOrEqualTo)},{nameof(RangeModel.EndValue)}:0;" +
            //    $"{nameof(MoveSearchModel.LandingLag)}:" +
            //    $"{nameof(MoveSearchModel.SetKnockback)}:;";

            var searchModel = new MoveSearchModel
            {
                Angle = new RangeModel { StartValue = 10, RangeQuantifier = RangeQuantifier.GreaterThan },
                AutoCancel = new RangeModel { StartValue = 5, RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo },
                BaseDamage = new RangeModel { StartValue = 3, RangeQuantifier = RangeQuantifier.LessThanOrEqualTo },
                BaseKnockback = new RangeModel { StartValue = 50, RangeQuantifier = RangeQuantifier.GreaterThan },
                FirstActionableFrame =
                    new RangeModel { StartValue = 30, RangeQuantifier = RangeQuantifier.Between, EndValue = 40 },
                HitboxActiveLength = new RangeModel { StartValue = 1, RangeQuantifier = RangeQuantifier.GreaterThan },
                HitboxActiveOnFrame = new RangeModel { StartValue = 2, RangeQuantifier = RangeQuantifier.GreaterThan },
                HitboxStartupFrame =
                    new RangeModel { StartValue = 3, RangeQuantifier = RangeQuantifier.GreaterThanOrEqualTo },
                KnockbackGrowth = new RangeModel { StartValue = 30, RangeQuantifier = RangeQuantifier.LessThanOrEqualTo },
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

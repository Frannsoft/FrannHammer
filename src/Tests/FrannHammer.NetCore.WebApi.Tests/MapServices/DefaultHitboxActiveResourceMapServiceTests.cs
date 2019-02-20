using FrannHammer.Domain;
using FrannHammer.NetCore.WebApi.HypermediaServices.MapServices;
using NUnit.Framework;
using System;

namespace FrannHammer.NetCore.WebApi.MapServices
{
    [TestFixture]
    public class DefaultHitboxActiveResourceMapServiceTests
    {
        [Test]
        [TestCase("5-6|Adv:-7", "5-6")]
        [TestCase("20-30|Adv:-10|SD:+5", "20-30")]
        public void ReturnsFirstValueFromRawValuesSeparatedBySemiColon(string rawValue, string expectedParsedValue)
        {
            var move = new Move
            {
                HitboxActive = rawValue
            };

            var sut = new DefaultHitboxActiveResourceMapService();
            string actualValue = sut.MapFrom(move);

            Assert.That(actualValue, Is.EqualTo(expectedParsedValue));
        }

        [Test]
        public void ReturnsEmptyStringIfNoData()
        {
            var move = new Move
            {
                HitboxActive = string.Empty
            };
            var sut = new DefaultHitboxActiveResourceMapService();
            string actualValue = sut.MapFrom(move);

            Assert.That(actualValue, Is.Empty);
        }

        [Test]
        public void ThrowsIfMoveIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultHitboxActiveResourceMapService().MapFrom(null));
        }
    }
}

using FrannHammer.Domain;
using FrannHammer.NetCore.WebApi.HypermediaServices.MapServices;
using NUnit.Framework;
using System;

namespace FrannHammer.NetCore.WebApi.MapServices
{
    [TestFixture]
    public class DefaultBaseDamageResourceMapServiceTests
    {
        [Test]
        [TestCase("11|1v1: 13.2", "11")]
        [TestCase("13/13/14|1v1: 15.6/15.6/16.8", "13/13/14")]
        public void ReturnsFirstValueFromRawValuesSeparatedBySemiColon(string rawValue, string expectedParsedValue)
        {
            var move = new Move { BaseDamage = rawValue };

            var sut = new DefaultBaseDamageResourceMapService();
            string actualValue = sut.MapFrom(move);

            Assert.That(actualValue, Is.EqualTo(expectedParsedValue));
        }

        [Test]
        public void ReturnsEmptyStringIfNoData()
        {
            var move = new Move { BaseDamage = string.Empty };
            var sut = new DefaultBaseDamageResourceMapService();
            string actualValue = sut.MapFrom(move);

            Assert.That(actualValue, Is.Empty);
        }

        [Test]
        public void ReturnsNormalWhenOnlyNormalPresent()
        {
            var move = new Move { BaseDamage = "11" };
            var sut = new DefaultBaseDamageResourceMapService();
            string actualResult = sut.MapFrom(move);

            Assert.That(actualResult, Is.EqualTo("11"));
        }

        [Test]
        public void ThrowsIfMoveIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultBaseDamageResourceMapService().MapFrom(null));
        }
    }
}

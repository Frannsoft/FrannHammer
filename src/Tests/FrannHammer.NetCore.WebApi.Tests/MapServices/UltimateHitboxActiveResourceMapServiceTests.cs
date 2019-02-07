using FrannHammer.Domain;
using FrannHammer.NetCore.WebApi.HypermediaServices.MapServices;
using NUnit.Framework;
using System;

namespace FrannHammer.NetCore.WebApi.MapServices
{
    [TestFixture]
    public class UltimateHitboxActiveResourceMapServiceTests
    {
        [Test]
        public void ReturnsFramesAndAdvWhenPresent()
        {
            var move = new Move
            {
                HitboxActive = "5-6;Adv:-7"
            };

            var sut = new UltimateHitboxActiveResourceMapService();
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("5-6"));
            Assert.That(actualResult.Adv, Is.EqualTo("-7"));
        }

        [Test]
        public void ReturnsJustAdvWhenOtherPropertiesArePresent()
        {
            var move = new Move
            {
                HitboxActive = "10-12;Adv:-9, SD:+1, Shieldstun Multiplier: 1.3"
            };
            var sut = new UltimateHitboxActiveResourceMapService();
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("10-12"));
            Assert.That(actualResult.Adv, Is.EqualTo("-9"));
        }

        [Test]
        public void ReturnsEmptyFramesWhenNoFramesPresent()
        {
            var move = new Move
            {
                HitboxActive = string.Empty
            };

            var sut = new UltimateHitboxActiveResourceMapService();

            var actualResult = sut.MapFrom(move);
            Assert.That(actualResult.Frames, Is.Empty);
            Assert.That(actualResult.Adv, Is.Empty);
        }

        [Test]
        public void ThrowsIfMoveIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultHitboxActiveResourceMapService().MapFrom(null));
        }
    }
}

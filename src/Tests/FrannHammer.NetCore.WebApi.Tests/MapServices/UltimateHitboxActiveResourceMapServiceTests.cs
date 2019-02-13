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
                HitboxActive = "5-6|Adv:-7"
            };

            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("5-6"));
            Assert.That(actualResult.Adv, Is.EqualTo("-7"));
        }

        [Test]
        public void ReturnsSDWhenPresent()
        {
            var move = new Move { HitboxActive = "10-12|Adv:-9, SD:+1, Shieldstun Multiplier: 1.3" };

            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("10-12"));
            Assert.That(actualResult.Adv, Is.EqualTo("-9"));
            Assert.That(actualResult.SD, Is.EqualTo("+1"));
        }

        [Test]
        public void ReturnsFacingRestrict()
        {
            var move = new Move { HitboxActive = "35-38|Facing Restrict: 4" };
            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());

            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("35-38"));
            Assert.That(actualResult.FacingRestrict, Is.EqualTo("4"));
        }

        [Test]
        public void ReturnsSetWeightAsBoolean()
        {
            var move = new Move { HitboxActive = "13-14|Set Weight" };
            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());

            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("13-14"));
            Assert.That(actualResult.SetWeight, Is.EqualTo(true));
        }

        [Test]
        public void ReturnsGroundOnlyAsBoolean()
        {
            var move = new Move { HitboxActive = "13-14|Set Weight, Ground Only" };
            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("13-14"));
            Assert.That(actualResult.GroundOnly, Is.EqualTo(true));
        }

        [Test]
        public void ReturnsSuperArmor()
        {
            var move = new Move { HitboxActive = "80-83|Super Armor:21-74" };
            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());

            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("80-83"));
            Assert.That(actualResult.SuperArmor, Is.EqualTo("21-74"));
        }

        [Test]
        public void ReturnsHeadMultiplier()
        {
            var move = new Move { HitboxActive = "19-20|Head Multiplier: 1.15" };

            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());

            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("19-20"));
            Assert.That(actualResult.HeadMultiplier, Is.EqualTo("1.15"));
        }

        [Test]
        public void ReturnsRehitRateWhenPresent()
        {
            var move = new Move { HitboxActive = "6-52|Rehit Rate: 6" };
            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());

            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("6-52"));
            Assert.That(actualResult.RehitRate, Is.EqualTo("6"));
        }

        [Test]
        public void ReturnsShieldstunMultiplier()
        {
            var move = new Move { HitboxActive = "10-12|Adv:-9, SD:+1, Shieldstun Multiplier: 1.3" };

            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("10-12"));
            Assert.That(actualResult.Adv, Is.EqualTo("-9"));
            Assert.That(actualResult.SD, Is.EqualTo("+1"));
            Assert.That(actualResult.ShieldstunMultiplier, Is.EqualTo("1.3"));
        }

        [Test]
        public void ReturnsJustAdvWhenOtherPropertiesArePresent()
        {
            var move = new Move { HitboxActive = "10-12|Adv:-9, SD:+1, Shieldstun Multiplier: 1.3" };

            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("10-12"));
            Assert.That(actualResult.Adv, Is.EqualTo("-9"));
        }

        [Test]
        public void ReturnsIntangible()
        {
            var move = new Move { HitboxActive = "4-6|Intangible: 1-7" };

            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());

            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Frames, Is.EqualTo("4-6"));
            Assert.That(actualResult.Intangible, Is.EqualTo("1-7"));
        }

        [Test]
        public void ReturnsEmptyFramesWhenNoFramesPresent()
        {
            var move = new Move
            {
                HitboxActive = string.Empty
            };

            var sut = new UltimateHitboxActiveResourceMapService(new TooltipPartParser());

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

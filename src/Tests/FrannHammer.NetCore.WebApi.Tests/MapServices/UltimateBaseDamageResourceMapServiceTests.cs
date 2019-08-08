using FrannHammer.Domain;
using FrannHammer.NetCore.WebApi.HypermediaServices.MapServices;
using NUnit.Framework;

namespace FrannHammer.NetCore.WebApi.MapServices
{
    [TestFixture]
    public class UltimateBaseDamageResourceMapServiceTests
    {
        [Test]
        public void ReturnsNormalAnd1v1WhenPresent()
        {
            var move = new Move
            {
                BaseDamage = "11|1v1: 13.2"
            };

            var sut = new UltimateBaseDamageResourceMapService(new TooltipPartParser());
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Normal, Is.EqualTo("11"));
            Assert.That(actualResult.Vs1, Is.EqualTo("13.2"));
        }

        [Test]
        public void ReturnsJustNormalAndEmpty1v1When1v1NotPresent()
        {
            var move = new Move { BaseDamage = "13/13/14" };

            var sut = new UltimateBaseDamageResourceMapService(new TooltipPartParser());
            var actualResult = sut.MapFrom(move);

            Assert.That(actualResult.Normal, Is.EqualTo("13/13/14"));
            Assert.That(actualResult.Vs1, Is.Empty);
        }
    }
}

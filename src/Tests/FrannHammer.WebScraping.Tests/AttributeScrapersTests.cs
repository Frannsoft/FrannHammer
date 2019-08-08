using FrannHammer.WebScraping.Attributes;
using FrannHammer.WebScraping.Contracts.Attributes;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class AttributeScrapersTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullAttributeScrapingService()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                AttributeScrapers.AllWithScrapingServices(null, string.Empty);
            });
        }

        [Test]
        public void AllWithScrapingServicesContainsNoDuplicateScrapers()
        {
            var attributeScrapingServiceMock = new Mock<IAttributeScrapingServices>();

            var actual = AttributeScrapers.AllWithScrapingServices(attributeScrapingServiceMock.Object, string.Empty).ToList();

            Assert.That(actual.Count, Is.EqualTo(actual.Distinct().Count()), "There are duplicate scrapers in the returned enumerable.");
        }
    }
}

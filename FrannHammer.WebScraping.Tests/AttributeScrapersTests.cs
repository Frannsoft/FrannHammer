using System;
using System.Linq;
using System.Reflection;
using FrannHammer.WebScraping.Attributes;
using FrannHammer.WebScraping.Contracts.Attributes;
using Moq;
using NUnit.Framework;

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
                AttributeScrapers.AllWithScrapingServices(null);
            });
        }

        [Test]
        public void AllWithScrapingServicesReturnsAllAttributeScrapers()
        {
            var referencedAssemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            var webScrapingAssembly = Assembly.Load(referencedAssemblyNames.First(an => an.Name.Equals("FrannHammer.WebScraping")));

            var expectedAttributeScrapers = webScrapingAssembly.GetTypes().Where(t => t.IsSubclassOf(typeof(AttributeScraper)))
                .OrderBy(scraper => scraper.Name);

            var attributeScrapingServiceMock = new Mock<IAttributeScrapingServices>();
            var actualAttributeScrapers = AttributeScrapers.AllWithScrapingServices(attributeScrapingServiceMock.Object)
                .Select(scraper => scraper.GetType())
                .OrderBy(type => type.Name);

            Assert.That(actualAttributeScrapers, Is.EqualTo(expectedAttributeScrapers));
        }

        [Test]
        public void AllWithScrapingServicesContainsNoDuplicateScrapers()
        {
            var attributeScrapingServiceMock = new Mock<IAttributeScrapingServices>();

            var actual = AttributeScrapers.AllWithScrapingServices(attributeScrapingServiceMock.Object).ToList();

            Assert.That(actual.Count, Is.EqualTo(actual.Distinct().Count()), "There are duplicate scrapers in the returned enumerable.");
        }
    }
}

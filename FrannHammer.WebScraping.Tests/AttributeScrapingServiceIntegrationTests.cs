using System.Collections.Generic;
using System.Linq;
using FrannHammer.WebScraping.Contracts;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class AttributeScrapingServiceIntegrationTests
    {
        private IAttributeScrapingServices _scrapingServices;

        [SetUp]
        public void SetUp()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var attributeProvider = new DefaultAttributeProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();

            _scrapingServices = new DefaultAttributeScrapingServices(htmlParserProvider, attributeProvider, pageDownloader,
                webClientProvider);
        }

        [Test]
        public void ScrapeAirSpeedData()
        {
            var airSpeedScraper = new AirSpeedScraper(_scrapingServices);
            var airSpeedAttributes = airSpeedScraper.Scrape().ToList();

            CollectionAssert.IsNotEmpty(airSpeedAttributes);
            CollectionAssert.AllItemsAreNotNull(airSpeedAttributes);
            CollectionAssert.AllItemsAreUnique(airSpeedAttributes);

            airSpeedAttributes.ForEach(attribute =>
            {
                Assert.That(attribute.AttributeFlag, Is.EqualTo(airSpeedScraper.AttributeName));
                Assert.That(attribute.Value, Is.Not.Empty);
                Assert.That(attribute.Name, Is.Not.Empty);
            });
        }

        [Test]
        public void ScrapeCollectionOfAttributes()
        {
            var scrapers = new List<AttributeScraper>
                {
                    new AirSpeedScraper(_scrapingServices),
                    new AirDodgeScraper(_scrapingServices)
                };

            foreach (var scraper in scrapers)
            {
                var attributes = scraper.Scrape().ToList();

                CollectionAssert.IsNotEmpty(attributes);
                CollectionAssert.AllItemsAreNotNull(attributes);
                CollectionAssert.AllItemsAreUnique(attributes);

                attributes.ForEach(attribute =>
                {
                    Assert.That(attribute.AttributeFlag, Is.EqualTo(scraper.AttributeName));
                    Assert.That(attribute.Value, Is.Not.Empty);
                    Assert.That(attribute.Name, Is.Not.Empty);
                });
            }
        }
    }
}

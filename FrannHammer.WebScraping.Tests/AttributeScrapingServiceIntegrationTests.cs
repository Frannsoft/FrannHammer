using System.Linq;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class AttributeScrapingServiceIntegrationTests
    {
        [Test]
        public void ScrapeAirSpeedData()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var attributeProvider = new DefaultAttributeProvider();
            var pageDownloader = new PageDownloader();
            var webClientProvider = new WebClientProvider();

            var scrapingServices = new DefaultScrapingServices(htmlParserProvider, attributeProvider, pageDownloader,
                webClientProvider);

            var airSpeedScraper = new AirSpeedScraper();
            var airSpeedAttributes = airSpeedScraper.Scrape("http://kuroganehammer.com/Smash4/AirSpeed", scrapingServices).ToList();

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
    }
}

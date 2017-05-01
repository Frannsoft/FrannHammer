using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Attributes;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultAttributeScrapingServiceIntegrationTests
    {
        private static void AssertAttributeCollectionIsValid(IAttributeScraper attributeScraper, List<ICharacterAttributeRow> attributeRows)
        {
            CollectionAssert.IsNotEmpty(attributeRows);
            CollectionAssert.AllItemsAreNotNull(attributeRows);
            CollectionAssert.AllItemsAreUnique(attributeRows);

            attributeRows.ForEach(row =>
            {
                Assert.That(row.CharacterName, Is.Not.Empty);
                Assert.That(row.Name, Is.EqualTo(attributeScraper.AttributeName));
                row.Values.ToList().ForEach(attribute =>
                {
                    Assert.That(attribute.Owner, Is.Not.Null);
                    Assert.That(attribute.Value, Is.Not.Null);
                    Assert.That(attribute.Name, Is.Not.Null);
                });
            });
        }

        private static IEnumerable<AttributeScraper> Scrapers()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var attributeProvider = new DefaultAttributeProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);
            var scrapingServices = new DefaultAttributeScrapingServices(attributeProvider, webServices);

            yield return new AirSpeedScraper(scrapingServices);
            yield return new AerialJumpScraper(scrapingServices);
            yield return new AirAccelerationScraper(scrapingServices);
            yield return new AirDecelerationScraper(scrapingServices);
            yield return new AirDodgeScraper(scrapingServices);
            yield return new AirFrictionScraper(scrapingServices);
            yield return new CounterScraper(scrapingServices);
            yield return new DashLengthScraper(scrapingServices);
            yield return new FallSpeedScraper(scrapingServices);
            yield return new FullHopScraper(scrapingServices);
            yield return new GravityScraper(scrapingServices);
            yield return new JumpSquatScraper(scrapingServices);
            yield return new LedgeHopScraper(scrapingServices);
            yield return new ShortHopScraper(scrapingServices);
            yield return new SpotdodgeScraper(scrapingServices);
            yield return new TractionScraper(scrapingServices);
            yield return new WalkSpeedScraper(scrapingServices);
        }

        [Test]
        [TestCaseSource(nameof(Scrapers))]
        public void ScrapeAttributeRowData(AttributeScraper scraper)
        {
            var attributeRows = scraper.Scrape(Characters.Greninja).ToList();

            AssertAttributeCollectionIsValid(scraper, attributeRows);
        }
    }
}

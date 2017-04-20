using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Attributes;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultAttributeScrapingServiceIntegrationTests
    {
        private IAttributeScrapingServices _scrapingServices;

        [SetUp]
        public void SetUp()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var attributeProvider = new DefaultAttributeProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);

            _scrapingServices = new DefaultAttributeScrapingServices(attributeProvider, webServices);
        }

        private static void AssertAttributeCollectionIsValid(IAttributeScraper attributeScraper, List<ICharacterAttributeRow> attributeRows)
        {
            CollectionAssert.IsNotEmpty(attributeRows);
            CollectionAssert.AllItemsAreNotNull(attributeRows);
            CollectionAssert.AllItemsAreUnique(attributeRows);

            attributeRows.ForEach(row =>
            {
                Assert.That(row.CharacterName, Is.Not.Empty);

                row.Values.ToList().ForEach(attribute =>
                {
                    Assert.That(attribute.AttributeFlag, Is.EqualTo(attributeScraper.AttributeName));
                    Assert.That(attribute.Value, Is.Not.Empty);
                    Assert.That(attribute.Name, Is.Not.Empty);
                });
            });
        }

        [Test]
        public void ScrapeAirSpeedData()
        {
            var airSpeedScraper = new AirSpeedScraper(_scrapingServices);
            var airSpeedAttributes = airSpeedScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(airSpeedScraper, airSpeedAttributes);
        }

        [Test]
        public void ScrapeAirDodgeData()
        {
            var airDodgeScraper = new AirDodgeScraper(_scrapingServices);
            var airDodgeAttributes = airDodgeScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(airDodgeScraper, airDodgeAttributes);
        }

        [Test]
        public void ScrapeAirAccelerationData()
        {
            var airAccelerationScraper = new AirAccelerationScraper(_scrapingServices);
            var airAccelerationAttributes = airAccelerationScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(airAccelerationScraper, airAccelerationAttributes);
        }

        [Test]
        public void ScrapeAirDecelerationData()
        {
            var airDecelerationScraper = new AirDecelerationScraper(_scrapingServices);
            var airDecelerationAttributes = airDecelerationScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(airDecelerationScraper, airDecelerationAttributes);
        }

        [Test]
        public void ScrapeAirFrictionData()
        {
            var airFrictionScraper = new AirFrictionScraper(_scrapingServices);
            var airFrictionAttributes = airFrictionScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(airFrictionScraper, airFrictionAttributes);
        }

        [Test]
        public void ScrapeDashLengthData()
        {
            var dashLengthScraper = new DashLengthScraper(_scrapingServices);
            var dashLengthAttributes = dashLengthScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(dashLengthScraper, dashLengthAttributes);
        }

        [Test]
        public void ScrapeFallSpeedData()
        {
            var fallSpeedScraper = new FallSpeedScraper(_scrapingServices);
            var fallSpeedAttributes = fallSpeedScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(fallSpeedScraper, fallSpeedAttributes);
        }

        [Test]
        public void ScrapeGravityData()
        {
            var gravityScraper = new GravityScraper(_scrapingServices);
            var gravityAttributes = gravityScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(gravityScraper, gravityAttributes);
        }

        [Test]
        public void ScrapeShortHopData()
        {
            var shortHopScraper = new ShortHopScraper(_scrapingServices);
            var shortHopAttributes = shortHopScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(shortHopScraper, shortHopAttributes);
        }

        [Test]
        public void ScrapeFullHopData()
        {
            var fullHopScraper = new FullHopScraper(_scrapingServices);
            var fullHopAttributes = fullHopScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(fullHopScraper, fullHopAttributes);
        }

        [Test]
        public void ScrapeAerialJumpData()
        {
            var aerialJumpScraper = new AerialJumpScraper(_scrapingServices);
            var aerialJumpAttributes = aerialJumpScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(aerialJumpScraper, aerialJumpAttributes);
        }

        [Test]
        public void ScrapeLedgeHopData()
        {
            var ledgeHopScraper = new LedgeHopScraper(_scrapingServices);
            var ledgeHopAttributes = ledgeHopScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(ledgeHopScraper, ledgeHopAttributes);
        }

        [Test]
        public void ScrapeJumpSquatData()
        {
            var jumpSquatScraper = new JumpSquatScraper(_scrapingServices);
            var jumpSquatAttributes = jumpSquatScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(jumpSquatScraper, jumpSquatAttributes);
        }

        [Test]
        public void ScrapeSpotDodgeData()
        {
            var spotDodgeScraper = new SpotdodgeScraper(_scrapingServices);
            var spotDodgeAttributes = spotDodgeScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(spotDodgeScraper, spotDodgeAttributes);
        }

        [Test]
        public void ScrapeTractionData()
        {
            var tractionScraper = new TractionScraper(_scrapingServices);
            var tractionAttributes = tractionScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(tractionScraper, tractionAttributes);
        }

        [Test]
        public void ScrapeWalkSpeedData()
        {
            var walkSpeedScraper = new TractionScraper(_scrapingServices);
            var walkSpeedAttributes = walkSpeedScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(walkSpeedScraper, walkSpeedAttributes);
        }

        [Test]
        public void ScrapeCountersData()
        {
            var counterScraper = new CounterScraper(_scrapingServices);
            var counterAttributes = counterScraper.Scrape().ToList();

            AssertAttributeCollectionIsValid(counterScraper, counterAttributes);
        }

        [Test]
        public void ScrapeCollectionOfAttributes()
        {
            var scrapers = new List<AttributeScraper>
                {
                    new AirSpeedScraper(_scrapingServices),
                    new AirAccelerationScraper(_scrapingServices),
                    new AirDecelerationScraper(_scrapingServices)
                };

            foreach (var scraper in scrapers)
            {
                var attributes = scraper.Scrape().ToList();
                AssertAttributeCollectionIsValid(scraper, attributes);
            }
        }
    }
}

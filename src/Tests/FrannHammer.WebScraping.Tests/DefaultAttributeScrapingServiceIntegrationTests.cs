using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Attributes;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultAttributeScrapingServiceIntegrationTests
    {
        private static DefaultAttributeScrapingServices MakeAttributeScrapingServices()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var attributeProvider = new DefaultAttributeProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);
            var scrapingServices = new DefaultAttributeScrapingServices(attributeProvider, webServices);

            return scrapingServices;
        }

        private static void AssertAttributeCollectionIsValid(IAttributeScraper attributeScraper, List<ICharacterAttributeRow> attributeRows)
        {
            Assert.That(attributeRows.Count, Is.GreaterThan(0), $"{nameof(attributeRows.Count)}");
            CollectionAssert.AllItemsAreNotNull(attributeRows);
            CollectionAssert.AllItemsAreUnique(attributeRows);

            attributeRows.ForEach(row =>
            {
                Assert.That(row.Owner, Is.Not.Empty);
                Assert.That(row.Name, Is.EqualTo(attributeScraper.AttributeName));
                row.Values.ToList().ForEach(attribute =>
                {
                    Assert.That(attribute.Name, Is.Not.EqualTo("RANK"));
                    Assert.That(attribute.Name, Is.Not.EqualTo("CHARACTER"));
                    Assert.That(attribute.Owner, Is.Not.Null);
                    Assert.That(attribute.Value, Is.Not.Null);
                    Assert.That(attribute.Name, Is.Not.Null);
                });
            });
        }

        private static IEnumerable<AttributeScraper> Scrapers()
        {
            var scrapingServices = MakeAttributeScrapingServices();
            yield return new AirSpeedScraper(scrapingServices);
            yield return new AerialJumpScraper(scrapingServices);
            yield return new AirAccelerationScraper(scrapingServices);
            yield return new AirDecelerationScraper(scrapingServices);
            yield return new AirDodgeScraper(scrapingServices);
            yield return new AirFrictionScraper(scrapingServices);
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
            yield return new RollScraper(scrapingServices);
            yield return new RunSpeedScraper(scrapingServices);
            yield return new ShieldSizeScraper(scrapingServices);
        }

        [Test]
        [TestCaseSource(nameof(Scrapers))]
        public void ScrapeAttributeRowData(AttributeScraper scraper)
        {
            var attributeRows = scraper.Scrape(Characters.DarkPit).ToList();

            AssertAttributeCollectionIsValid(scraper, attributeRows);
        }

        [Test]
        public void ScraperAttributeRowData_Counters()
        {
            var scrapingServices = MakeAttributeScrapingServices();

            var sut = new CounterScraper(scrapingServices);
            var results = sut.Scrape(Characters.Greninja).ToList();

            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        public void ScrapeAttributeRowData_Reflectors()
        {
            var scrapingServices = MakeAttributeScrapingServices();

            var sut = new ReflectorScraper(scrapingServices);
            var results = sut.Scrape(Characters.DarkPit).ToList();

            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCaseSource(nameof(Scrapers))]
        public void ScrapeAttributeRowData_CharacterHasSpaceInName(AttributeScraper scraper)
        {
            var littleMac = new LittleMac();
            var attributeRows = scraper.Scrape(littleMac).ToList();

            AssertAttributeCollectionIsValid(scraper, attributeRows);
        }

        private static IEnumerable<WebCharacter> TestCharacters()
        {
            foreach (var character in Characters.All)
            {
                yield return character;
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCharacters))]
        public void ScrapeAttributeRowData_AllCharacters_ReturnsValidData(WebCharacter character)
        {
            var scrapingServices = MakeAttributeScrapingServices();
            var sut = new AirSpeedScraper(scrapingServices);

            var attributeRows = sut.Scrape(character).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
        }

        [Test]
        public void ScrapeRollsData_ForGreninja_ReturnsBothEntriesSinceForwardAndBackRollAreDifferent()
        {
            var scrapingServices = MakeAttributeScrapingServices();
            var sut = new RollScraper(scrapingServices);

            var attributeRows = sut.Scrape(Characters.Greninja).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
            Assert.That(attributeRows.Count, Is.EqualTo(2), $"{nameof(attributeRows.Count)}");
        }
    }
}

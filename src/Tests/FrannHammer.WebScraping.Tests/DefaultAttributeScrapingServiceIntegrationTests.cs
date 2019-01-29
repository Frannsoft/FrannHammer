using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Attributes;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultAttributeScrapingServiceIntegrationTests
    {
        private static DefaultAttributeScrapingServices _scrapingServices = MakeAttributeScrapingServices();

        private static DefaultAttributeScrapingServices MakeAttributeScrapingServices()
        {
            var instanceIdGenerator = new InstanceIdGenerator();
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var attributeProvider = new DefaultAttributeProvider(instanceIdGenerator);
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

        private static IEnumerable<AttributeScraper> ScrapersSmash4()
        {
            var smash4Scrapers = AttributeScrapers.AllWithScrapingServices(_scrapingServices, $"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}").ToList();
            foreach (var scraper in smash4Scrapers)
            {
                yield return scraper;
            }
        }

        private static IEnumerable<AttributeScraper> ScrapersUltimate()
        {
            var ultimateScrapers = AttributeScrapers.AllWithScrapingServices(_scrapingServices, $"{Keys.KHSiteBaseUrl}{Keys.UltimateUrl}").ToList();

            foreach (var scraper in ultimateScrapers)
            {
                yield return scraper;
            }
        }

        [Test]
        [TestCaseSource(nameof(ScrapersSmash4))]
        [TestCaseSource(nameof(ScrapersUltimate))]
        public void ScrapeAttributeRowData(AttributeScraper scraper)
        {
            var attributeRows = scraper.Scrape(Characters.DarkPit).ToList();
            AssertAttributeCollectionIsValid(scraper, attributeRows);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        [TestCase(Keys.KHSiteBaseUrl + Keys.UltimateUrl)]
        public void ScraperAttributeRowData_Counters(string urlUnderTest)
        {
            var sut = new CounterScraper(_scrapingServices, urlUnderTest);
            var results = sut.Scrape(Characters.Greninja).ToList();

            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        [TestCase(Keys.KHSiteBaseUrl + Keys.UltimateUrl)]
        public void ScrapeAttributeRowData_Reflectors(string urlUnderTest)
        {
            var sut = new ReflectorScraper(_scrapingServices, urlUnderTest);
            var results = sut.Scrape(Characters.DarkPit).ToList();

            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCaseSource(nameof(ScrapersSmash4))]
        public void ScrapeAttributeRowData_CharacterHasSpaceInName_Smash4(AttributeScraper scraper)
        {
            var drMario = new DrMario();
            drMario.SourceUrl = $"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}{drMario.EscapedCharacterName}";
            var attributeRows = scraper.Scrape(drMario).ToList();

            AssertAttributeCollectionIsValid(scraper, attributeRows);
        }

        [Test]
        [TestCaseSource(nameof(ScrapersUltimate))]
        public void ScrapeAttributeRowData_CharacterHasSpaceInName_Ultimate(AttributeScraper scraper)
        {
            var littleMac = new LittleMac();
            littleMac.SourceUrl = $"{Keys.KHSiteBaseUrl}{Keys.UltimateUrl}{littleMac.EscapedCharacterName}";
            var attributeRows = scraper.Scrape(littleMac).ToList();

            AssertAttributeCollectionIsValid(scraper, attributeRows);
        }

        private static IEnumerable<WebCharacter> TestCharactersSmash4()
        {
            foreach (var character in Characters.All.Where(c => c.OwnerId <= 58))
            {
                yield return character;
            }
        }

        private static IEnumerable<WebCharacter> TestCharactersUltimate()
        {
            foreach (var character in Characters.All)
            {
                yield return character;
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCharactersSmash4))]
        public void ScrapeAttributeRowData_AllCharacters_ReturnsValidData_Smash4(WebCharacter character)
        {
            var sut = new AirSpeedScraper(_scrapingServices, $"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}");

            var attributeRows = sut.Scrape(character).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
        }

        [Test]
        [TestCaseSource(nameof(TestCharactersUltimate))]
        public void ScrapeAttributeRowData_AllCharacters_ReturnsValidData_Ultimate(WebCharacter character)
        {
            var sut = new AirSpeedScraper(_scrapingServices, $"{Keys.KHSiteBaseUrl}{Keys.UltimateUrl}");

            var attributeRows = sut.Scrape(character).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
        }

        [Test]
        public void ScrapeRollsData_ForGreninja_ReturnsBothEntriesSinceForwardAndBackRollAreDifferent()
        {
            var sut = new RollScraper(_scrapingServices, $"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}");

            var attributeRows = sut.Scrape(Characters.Greninja).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
            Assert.That(attributeRows.Count, Is.EqualTo(2), $"{nameof(attributeRows.Count)}");
        }

        [Test]
        public void ScrapeRunSpeed_ForNess_ReturnsRunSpeedEvenWithAltName()
        {
            var sut = new RunSpeedScraper(_scrapingServices, $"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}");

            var attributeRows = sut.Scrape(Characters.Ness).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
            Assert.That(attributeRows.Count, Is.EqualTo(1), $"{nameof(attributeRows.Count)}");
        }
    }
}

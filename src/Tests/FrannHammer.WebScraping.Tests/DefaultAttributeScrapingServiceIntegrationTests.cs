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

        private static void AssertAttributeCollectionIsValid(IAttributeScraper attributeScraper, List<ICharacterAttributeRow> attributeRows, string attributeDisplayName = "")
        {
            Assert.That(attributeRows.Count, Is.GreaterThan(0), $"{nameof(attributeRows.Count)}");
            CollectionAssert.AllItemsAreNotNull(attributeRows);
            CollectionAssert.AllItemsAreUnique(attributeRows);

            attributeRows.ForEach(row =>
            {
                Assert.That(row.Owner, Is.Not.Empty);
                Assert.That(row.Name, Is.EqualTo(string.IsNullOrEmpty(attributeDisplayName) ? attributeScraper.AttributeName : attributeDisplayName));
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
            var attributeRows = scraper.Scrape(Characters.Ryu).ToList();
            AssertAttributeCollectionIsValid(scraper, attributeRows);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        [TestCase(Keys.KHSiteBaseUrl + Keys.UltimateUrl)]
        public void ScraperAttributeRowData_Counters(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Counters);
            var results = sut.Scrape(Characters.Greninja).ToList();

            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        [TestCase(Keys.KHSiteBaseUrl + Keys.UltimateUrl)]
        public void ScraperAttributeRowData_Weight(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Weight);
            var results = sut.Scrape(Characters.Ryu).ToList();

            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_HardTrip(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Trip, AttributeScrapers.HardTrip, ScrapingConstants.XPathTableNodeAttributesWithNoDescription);
            var results = sut.Scrape(Characters.Roy).ToList();

            AssertAttributeCollectionIsValid(sut, results, "HardTrip");
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_SoftTrip(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Trip, AttributeScrapers.SoftTrip, ScrapingConstants.XPathTableNodeAttributesWithDescription);
            var results = sut.Scrape(Characters.Charizard).ToList();

            AssertAttributeCollectionIsValid(sut, results, "SoftTrip");
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_GetUpOnBack(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.GetUpStand, AttributeScrapers.GetUpStandOnBack, ScrapingConstants.XPathTableNodeAttributesWithNoDescription);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            AssertAttributeCollectionIsValid(sut, results, "GetUp On Back");
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_GetUpOnFront(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.GetUpStand, AttributeScrapers.GetUpStandOnFront, ScrapingConstants.XPathTableNodeAttributesWithDescription);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            AssertAttributeCollectionIsValid(sut, results, "GetUp On Front");
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        [TestCase(Keys.KHSiteBaseUrl + Keys.UltimateUrl)]
        public void ScrapeAttributeRowData_Reflectors(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Reflector);
            var results = sut.Scrape(Characters.DarkPit).ToList();

            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCaseSource(nameof(ScrapersSmash4))]
        public void ScrapeAttributeRowData_CharacterHasSpaceInName_Smash4(AttributeScraper scraper)
        {
            var palutena = new Palutena();
            palutena.SourceUrl = $"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}{palutena.EscapedCharacterName}";
            var attributeRows = scraper.Scrape(palutena).ToList();

            AssertAttributeCollectionIsValid(scraper, attributeRows, scraper.AttributeDisplayName);
        }

        [Test]
        [TestCaseSource(nameof(ScrapersUltimate))]
        public void ScrapeAttributeRowData_CharacterHasSpaceInName_Ultimate(AttributeScraper scraper)
        {
            var palutena = new Palutena();
            palutena.SourceUrl = $"{Keys.KHSiteBaseUrl}{Keys.UltimateUrl}{palutena.EscapedCharacterName}";
            var attributeRows = scraper.Scrape(palutena).ToList();

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
            var sut = new AttributeScraper($"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}", _scrapingServices, AttributeScrapers.AirSpeed);

            var attributeRows = sut.Scrape(character).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
        }

        [Test]
        [TestCaseSource(nameof(TestCharactersUltimate))]
        public void ScrapeAttributeRowData_AllCharacters_ReturnsValidData_Ultimate(WebCharacter character)
        {
            var sut = new AttributeScraper($"{Keys.KHSiteBaseUrl}{Keys.UltimateUrl}", _scrapingServices, AttributeScrapers.AirSpeed);

            var attributeRows = sut.Scrape(character).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
        }

        [Test]
        public void ScrapeRollsData_ForGreninja_ReturnsBothEntriesSinceForwardAndBackRollAreDifferent()
        {
            var sut = new AttributeScraper($"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}", _scrapingServices, AttributeScrapers.Rolls);

            var attributeRows = sut.Scrape(Characters.Greninja).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
            Assert.That(attributeRows.Count, Is.EqualTo(2), $"{nameof(attributeRows.Count)}");
        }

        [Test]
        public void ScrapeRunSpeed_ForNess_ReturnsRunSpeedEvenWithAltName()
        {
            var sut = new AttributeScraper($"{Keys.KHSiteBaseUrl}{Keys.Smash4Url}", _scrapingServices, AttributeScrapers.RunSpeed);

            var attributeRows = sut.Scrape(Characters.Ness).ToList();

            AssertAttributeCollectionIsValid(sut, attributeRows);
            Assert.That(attributeRows.Count, Is.EqualTo(1), $"{nameof(attributeRows.Count)}");
        }
    }
}

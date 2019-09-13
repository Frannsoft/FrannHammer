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

        private static IEnumerable<WebCharacter> CharacterWithSpecialCharactersInNames()
        {
            yield return new Lucina();
            yield return new Marth();
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
        [TestCaseSource(nameof(CharacterWithSpecialCharactersInNames))]
        public void AttributesAreScrapedAsExpectedForCharactersWithSpecialCharactersInName(WebCharacter character)
        {
            var scraper = new AttributeScraper(Keys.KHSiteBaseUrl + Keys.Smash4Url, _scrapingServices, AttributeScrapers.Gravity);
            var attributeRows = scraper.Scrape(character).ToList();
            AssertAttributeCollectionIsValid(scraper, attributeRows, scraper.AttributeDisplayName);
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

            Assert.That(results.First().Values.First().Value, Is.EqualTo("103"));
            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_HardTrip(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Trip,
                AttributeScrapers.HardTrip, ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(Characters.Roy).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("41"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("1-5"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.HardTrip);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_SoftTrip(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Trip,
                AttributeScrapers.SoftTrip, ScrapingConstants.XPathTableNodeAttributesSecondTable);
            var results = sut.Scrape(Characters.Charizard).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("34"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("1-5"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.SoftTrip);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_GetUpOnBack(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.GetUpStand,
                AttributeScrapers.GetUpStandOnBack, ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("29"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("1-22"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.GetUpStandOnBack);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_GetUpOnFront(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.GetUpStand,
                AttributeScrapers.GetUpStandOnFront, ScrapingConstants.XPathTableNodeAttributesSecondTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("30"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("1-22"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.GetUpStandOnFront);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_GetUpBackRollBack(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.GetUpBackRoll,
                AttributeScrapers.GetUpBackRollBack, ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("1-20"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("36"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.GetUpBackRollBack);
        }


        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_GetUpBackRollFront(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.GetUpBackRoll,
                AttributeScrapers.GetUpBackRollFront, ScrapingConstants.XPathTableNodeAttributesSecondTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("1-20"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("36"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.GetUpBackRollFront);
        }


        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_GetUpForwardRollBack(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.GetUpForwardRoll,
                AttributeScrapers.GetUpForwardRollBack, ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("1-20"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("36"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.GetUpForwardRollBack);
        }


        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_GetUpForwardRollFront(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.GetUpForwardRoll,
                AttributeScrapers.GetUpForwardRollFront, ScrapingConstants.XPathTableNodeAttributesSecondTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("1-20"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("36"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.GetUpForwardRollFront);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_TechFrameData(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Tech,
                AttributeScrapers.TechFrameData, ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("27"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("1-20"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.TechFrameData);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_TechRollForward(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Tech,
                AttributeScrapers.TechRollForward, ScrapingConstants.XPathTableNodeAttributesSecondTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("41"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("1-20"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.TechRollForward);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_TechRollBack(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.Tech,
                AttributeScrapers.TechRollBackward, ScrapingConstants.XPathTableNodeAttributesThirdTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("41"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("1-20"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.TechRollBackward);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_LedgeAttack(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.LedgeAttack,
               xpathToTable: ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("19-21 (Intangible: 1-16)"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("7"));
            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_LedgeGetUp(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.LedgeGetUp,
               xpathToTable: ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("1-32"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("34"));
            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_LedgeJump(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.LedgeJump,
               xpathToTable: ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("1-12"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("13"));
            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_LedgeRoll(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.LedgeRoll,
                xpathToTable: ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("1-26"));
            Assert.That(results.First().Values.Last().Value, Is.EqualTo("50"));

            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_JabLockFront(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.JabLock,
                AttributeScrapers.JabLockFront, ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("26"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.JabLockFront);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_JabLockBack(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.JabLock,
                AttributeScrapers.JabLockBack, ScrapingConstants.XPathTableNodeAttributesSecondTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("24"));
            AssertAttributeCollectionIsValid(sut, results, AttributeScrapers.JabLockBack);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_ShortHop(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.ShortHop,
                xpathToTable: ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("17.179214"));
            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_FullHop(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.FullHop,
                xpathToTable: ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("35.599998"));
            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_AerialJump(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.AerialJump,
                xpathToTable: ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("35.599998"));
            AssertAttributeCollectionIsValid(sut, results);
        }

        [Test]
        [TestCase(Keys.KHSiteBaseUrl + Keys.Smash4Url)]
        public void ScraperAttributeRowData_LedgeHop(string urlUnderTest)
        {
            var sut = new AttributeScraper(urlUnderTest, _scrapingServices, AttributeScrapers.LedgeHop,
                xpathToTable: ScrapingConstants.XPathTableNodeAttributesFirstTable);
            var results = sut.Scrape(new WiiFitTrainer()).ToList();

            Assert.That(results.First().Values.First().Value, Is.EqualTo("37.799999"));
            AssertAttributeCollectionIsValid(sut, results);
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

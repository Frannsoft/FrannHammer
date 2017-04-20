using System.Linq;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultMoveScrapingServiceIntegrationTests
    {
        private IMoveScrapingServices _scrapingServices;

        [SetUp]
        public void SetUp()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var moveProvider = new DefaultMoveProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);

            _scrapingServices = new DefaultMoveScrapingServices(moveProvider, webServices);
        }

        [Test]
        public void ScrapeGroundMovesForCharacter()
        {
            var groundMoveScrapingService = new GroundMoveScraper(_scrapingServices);

            var groundMoves = groundMoveScrapingService.Scrape(Characters.Greninja.SourceUrl).ToList();

            CollectionAssert.AllItemsAreNotNull(groundMoves);
            CollectionAssert.AllItemsAreUnique(groundMoves);
            CollectionAssert.IsNotEmpty(groundMoves);

            groundMoves.ForEach(move =>
            {
                Assert.That(move.Name, Is.Not.Empty, "Move name should not be empty.");
            });
        }

        [Test]
        public void ScrapeAerialMovesForCharacter()
        {
            var aerialMoveScrapingService = new AerialMoveScraper(_scrapingServices);

            var aerialMoves = aerialMoveScrapingService.Scrape(Characters.Greninja.SourceUrl).ToList();

            CollectionAssert.AllItemsAreNotNull(aerialMoves);
            CollectionAssert.AllItemsAreUnique(aerialMoves);
            CollectionAssert.IsNotEmpty(aerialMoves);

            aerialMoves.ForEach(move =>
            {
                Assert.That(move.Name, Is.Not.Empty, "Move name should not be empty.");
            });
        }

        [Test]
        public void ScrapeSpecialMovesForCharacter()
        {
            var specialMoveScrapingService = new SpecialMoveScraper(_scrapingServices);

            var specialMoves = specialMoveScrapingService.Scrape(Characters.Greninja.SourceUrl).ToList();

            CollectionAssert.AllItemsAreNotNull(specialMoves);
            CollectionAssert.AllItemsAreUnique(specialMoves);
            CollectionAssert.IsNotEmpty(specialMoves);

            specialMoves.ForEach(move =>
            {
                Assert.That(move.Name, Is.Not.Empty, "Move name should not be empty.");
            });
        }
    }
}

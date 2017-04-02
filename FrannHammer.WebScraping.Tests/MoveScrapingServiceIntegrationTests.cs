using System.Linq;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Domain;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class MoveScrapingServiceIntegrationTests
    {
        private IMoveScrapingServices _scrapingServices;

        [SetUp]
        public void SetUp()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var moveProvider = new DefaultMoveProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();

            _scrapingServices = new DefaultMoveScrapingServices(htmlParserProvider, moveProvider, pageDownloader,
                webClientProvider);
        }

        [Test]
        public void ScrapeGroundMovesForCharacter()
        {
            var groundMoveScrapingService = new GroundMoveScrapingService(Characters.Greninja.SourceUrl, _scrapingServices);

            var groundMoves = groundMoveScrapingService.Scrape().ToList();

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
            var aerialMoveScrapingService = new AerialMoveScrapingService(Characters.Greninja.SourceUrl, _scrapingServices);

            var aerialMoves = aerialMoveScrapingService.Scrape().ToList();

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
            var specialMoveScrapingService = new SpecialMoveScrapingService(Characters.Greninja.SourceUrl, _scrapingServices);

            var specialMoves = specialMoveScrapingService.Scrape().ToList();

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

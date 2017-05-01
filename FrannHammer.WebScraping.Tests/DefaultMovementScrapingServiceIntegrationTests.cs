using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Movements;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Movements;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultMovementScrapingServiceIntegrationTests
    {
        private IMovementScrapingServices _scrapingServices;

        [SetUp]
        public void SetUp()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var movementProvider = new DefaultMovementProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);

            _scrapingServices = new DefaultMovementScrapingServices(movementProvider, webServices);
        }

        private static void AssertMovementIsValid(IMovement movement)
        {
            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Name, Is.Not.Null);
            Assert.That(movement.Value, Is.Not.Null);
            Assert.That(movement.OwnerId, Is.Not.Null);
        }


        [Test]
        public void ScrapeMovementsForCharacter()
        {
            var movementScrapingService = new DefaultMovementScraper(_scrapingServices);
            var movements = movementScrapingService.GetMovementsForCharacter(Characters.Greninja).ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.AllItemsAreUnique(movements);
            CollectionAssert.IsNotEmpty(movements);

            movements.ForEach(AssertMovementIsValid);
        }
    }
}

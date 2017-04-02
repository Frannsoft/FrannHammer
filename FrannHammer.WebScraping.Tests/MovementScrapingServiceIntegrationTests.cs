using System;
using System.Linq;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Domain;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class MovementScrapingServiceIntegrationTests
    {
        private IMovementScrapingServices _scrapingServices;

        [SetUp]
        public void SetUp()
        {
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var movementProvider = new DefaultMovementProvider();
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();

            _scrapingServices = new DefaultMovementScrapingServices(htmlParserProvider, movementProvider, pageDownloader,
                webClientProvider);
        }

        [Test]
        public void ScrapeMovementsForCharacter()
        {
            var movementScrapingService = new DefaultMovementScrapingService(_scrapingServices);
            var movements = movementScrapingService.GetMovementsForCharacter(Characters.Greninja).ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.AllItemsAreUnique(movements);
            CollectionAssert.IsNotEmpty(movements);

            movements.ForEach(movement =>
            {
                Assert.That(movement.Name, Is.Not.Empty, "Movement name should not be empty.");
                Assert.That(movement.Value, Is.Not.Empty, "Movement value should not be empty.");
            });
        }
    }
}

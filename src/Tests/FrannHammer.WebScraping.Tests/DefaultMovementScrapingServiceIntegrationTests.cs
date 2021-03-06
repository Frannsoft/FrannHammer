﻿using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Contracts.Movements;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Movements;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultMovementScrapingServiceIntegrationTests
    {
        private IMovementScrapingServices _scrapingServices;

        [SetUp]
        public void SetUp()
        {
            var instanceIdGenerator = new InstanceIdGenerator();
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var movementProvider = new DefaultMovementProvider(instanceIdGenerator);
            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            var webServices = new DefaultWebServices(htmlParserProvider, webClientProvider, pageDownloader);

            _scrapingServices = new DefaultMovementScrapingServices(movementProvider, webServices);
        }

        private static void AssertMovementIsValid(IMovement movement, IHaveAName character)
        {
            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Name, Is.Not.Null);
            Assert.That(movement.Value, Is.Not.Null);
            Assert.That(movement.Owner, Is.EqualTo(character.Name), $"{nameof(movement.Owner)}");
        }

        [Test]
        public void ScrapeMovementsForCharacter()
        {
            var character = Characters.Greninja;

            character.SourceUrl = $"{Keys.KHSiteBaseUrl}Smash4/{character.EscapedCharacterName}";
            var movementScrapingService = new DefaultMovementScraper(_scrapingServices);
            var movements = movementScrapingService.GetMovementsForCharacter(character).ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.AllItemsAreUnique(movements);
            CollectionAssert.IsNotEmpty(movements);

            movements.ForEach(movement =>
            {
                AssertMovementIsValid(movement, character);
            });
        }
    }
}

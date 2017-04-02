using System;
using FrannHammer.WebScraping.Domain;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class CharacterDataScrapingServiceIntegrationTests
    {
        [Test]
        [Ignore("Test is significantly outdated now.  Will be rewritten.")]
        public void CanGetCharacterDataFromWeb()
        {
            var greninja = Characters.Greninja;

            var pageDownloader = new DefaultPageDownloader();
            var webClientProvider = new DefaultWebClientProvider();
            string html = pageDownloader.DownloadPageSource(new Uri(greninja.SourceUrl), webClientProvider);

            var htmlDocProvider = new HtmlDocProvider();

            var htmlParser = new HtmlParser(html, htmlDocProvider);
            var imageScrapingService = new ImageScrapingService(new ImageScrapingProvider());
            //var moveScrapingService = new DefaultMoveScrapingService(htmlParser, new MoveProvider());
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var attributeProvider = new DefaultAttributeProvider();
            var movementScrapingService = new DefaultMovementScrapingService(new DefaultMovementScrapingServices(htmlParserProvider, new MovementProvider(), pageDownloader, webClientProvider));

            var attributeScrapingService = new DefaultAttributeScrapingService(htmlParserProvider, attributeProvider, pageDownloader, webClientProvider);

            //var characterDataScrapingService = new CharacterDataScrapingService(htmlParser, imageScrapingService, movementScrapingService, moveScrapingService, attributeScrapingService);
            //characterDataScrapingService.PopulateCharacterFromWeb(greninja);

            Assert.That(greninja.DisplayName, Is.EqualTo("Greninja"));
            Assert.That(greninja.MainImageUrl, Is.Not.Empty);
            Assert.That(Uri.IsWellFormedUriString(greninja.MainImageUrl, UriKind.Absolute), $"Malformed main image url: '{greninja.MainImageUrl}'");
            CollectionAssert.IsNotEmpty(greninja.Movements);
            CollectionAssert.IsNotEmpty(greninja.Moves);
            CollectionAssert.IsNotEmpty(greninja.Attributes);
        }
    }
}

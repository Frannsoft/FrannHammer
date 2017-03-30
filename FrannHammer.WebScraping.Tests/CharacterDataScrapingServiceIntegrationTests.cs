using System;
using FrannHammer.WebScraping.Domain;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class CharacterDataScrapingServiceIntegrationTests
    {
        [Test]
        public void CanGetCharacterDataFromWeb()
        {
            var greninja = Characters.Greninja;

            var pageDownloader = new PageDownloader();
            var webClientProvider = new WebClientProvider();
            string html = pageDownloader.DownloadPageSource(new Uri(greninja.SourceUrl), webClientProvider);

            var htmlDocProvider = new HtmlDocProvider();

            var htmlParser = new HtmlParser(html, htmlDocProvider);
            var imageScrapingService = new ImageScrapingService(new ImageScrapingProvider());
            var movementScrapingService = new MovementScrapingService(htmlParser, new MovementProvider());
            var moveScrapingService = new MoveScrapingService(htmlParser, new MoveProvider());
            var htmlParserProvider = new DefaultHtmlParserProvider();
            var attributeProvider = new DefaultAttributeProvider();

            var attributeScrapingService = new DefaultAttributeScrapingService(htmlParserProvider, attributeProvider, pageDownloader, webClientProvider);

            var characterDataScrapingService = new CharacterDataScrapingService(htmlParser, imageScrapingService, movementScrapingService, moveScrapingService, attributeScrapingService);
            characterDataScrapingService.PopulateCharacterFromWeb(greninja);

            Assert.That(greninja.DisplayName, Is.EqualTo("Greninja"));
            Assert.That(greninja.MainImageUrl, Is.Not.Empty);
            Assert.That(Uri.IsWellFormedUriString(greninja.MainImageUrl, UriKind.Absolute), $"Malformed main image url: '{greninja.MainImageUrl}'");
            CollectionAssert.IsNotEmpty(greninja.Movements);
            CollectionAssert.IsNotEmpty(greninja.Moves);
            CollectionAssert.IsNotEmpty(greninja.Attributes);
        }
    }
}

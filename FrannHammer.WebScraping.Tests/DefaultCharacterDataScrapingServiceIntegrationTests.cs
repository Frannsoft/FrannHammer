using System;
using System.Collections.Generic;
using FrannHammer.WebScraping.Attributes;
using FrannHammer.WebScraping.Character;
using FrannHammer.WebScraping.Contracts;
using FrannHammer.WebScraping.Contracts.Attributes;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Contracts.HtmlParsing;
using FrannHammer.WebScraping.Contracts.Images;
using FrannHammer.WebScraping.Contracts.Movements;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Contracts.PageDownloading;
using FrannHammer.WebScraping.Contracts.WebClients;
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Images;
using FrannHammer.WebScraping.Movements;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;

namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    public class DefaultCharacterDataScrapingServiceIntegrationTests
    {
        private IMovementScrapingServices _movementScrapingServices;
        private IMoveScrapingServices _moveScrapingServices;
        private IAttributeScrapingServices _attributeScrapingServices;
        private IHtmlParserProvider _htmlParserProvider;
        private IMovementProvider _movementProvider;
        private IPageDownloader _pageDownloader;
        private IWebClientProvider _webClientProvider;
        private IAttributeProvider _attributeProvider;
        private IMoveProvider _moveProvider;
        private IImageScrapingService _imageScrapingService;
        private IImageScrapingProvider _imageScrapingProvider;
        private IMovementScraper _movementScraper;
        private ICharacterDataScrapingServices _characterDataScrapingServices;
        private GroundMoveScraper _groundMoveScraper;
        private AerialMoveScraper _aerialMoveScraper;
        private SpecialMoveScraper _specialMoveScraper;
        private ICharacterMoveScraper _characterMoveScraper;
        private IWebServices _webServices;
        private DefaultCharacterDataScraper _characterDataScraper;

        [SetUp]
        public void SetUp()
        {
            _htmlParserProvider = new DefaultHtmlParserProvider();
            _movementProvider = new DefaultMovementProvider();
            _moveProvider = new DefaultMoveProvider();
            _pageDownloader = new DefaultPageDownloader();
            _webClientProvider = new DefaultWebClientProvider();
            _attributeProvider = new DefaultAttributeProvider();
            _imageScrapingProvider = new DefaultImageScrapingProvider();
            _imageScrapingService = new DefaultImageScrapingService(_imageScrapingProvider);

            _webServices = new DefaultWebServices(_htmlParserProvider, _webClientProvider, _pageDownloader);

            _attributeScrapingServices = new DefaultAttributeScrapingServices(_attributeProvider, _webServices);
            _moveScrapingServices = new DefaultMoveScrapingServices(_moveProvider, _webServices);
            _movementScrapingServices = new DefaultMovementScrapingServices(_movementProvider, _webServices);

            _groundMoveScraper = new GroundMoveScraper(_moveScrapingServices);
            _aerialMoveScraper = new AerialMoveScraper(_moveScrapingServices);
            _specialMoveScraper = new SpecialMoveScraper(_moveScrapingServices);

            _characterMoveScraper = new DefaultCharacterMoveScraper(_groundMoveScraper, _aerialMoveScraper, _specialMoveScraper);

            var attributeScrapers = new List<IAttributeScraper>
            {
                            new AerialJumpScraper(_attributeScrapingServices),
                            new AirAccelerationScraper(_attributeScrapingServices),
                            new AirDecelerationScraper(_attributeScrapingServices),
                            new AirFrictionScraper(_attributeScrapingServices),
                            new DashLengthScraper(_attributeScrapingServices),
                            new FallSpeedScraper(_attributeScrapingServices),
                            new FullHopScraper(_attributeScrapingServices),
                            new GravityScraper(_attributeScrapingServices),
                            new JumpSquatScraper(_attributeScrapingServices),
                            new LedgeHopScraper(_attributeScrapingServices),
                            new ShortHopScraper(_attributeScrapingServices),
                            new SpotdodgeScraper(_attributeScrapingServices),
                            new TractionScraper(_attributeScrapingServices),
                            new WalkSpeedScraper(_attributeScrapingServices),
                            new AirSpeedScraper(_attributeScrapingServices),
                            new AirDodgeScraper(_attributeScrapingServices)
            };
            _movementScraper = new DefaultMovementScraper(_movementScrapingServices);

            _characterDataScrapingServices = new DefaultCharacterDataScrapingServices(_imageScrapingService, _movementScraper,
                attributeScrapers, _characterMoveScraper, _webServices);

            _characterDataScraper = new DefaultCharacterDataScraper(_characterDataScrapingServices);
        }

        private static IEnumerable<WebCharacter> Characters()
        {
            return Domain.Characters.All;
        }

        [Test]
        [TestCaseSource(nameof(Characters))]
        public void EnsureCharactersCanBeScraped(WebCharacter character)
        {
            _characterDataScraper.PopulateCharacterFromWeb(character);

            Assert.That(character.FullUrl, Is.Not.Empty);
            Assert.That(character.ColorTheme, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.Not.Empty);
            Assert.That(character.ThumbnailUrl, Is.Not.Null);
            Assert.That(character.MainImageUrl, Is.Not.Empty);

            Uri testUri;
            Assert.That(Uri.TryCreate(character.MainImageUrl, UriKind.RelativeOrAbsolute, out testUri), $"Malformed main image url: '{character.MainImageUrl}'");

            CollectionAssert.IsNotEmpty(character.Movements, $"Movements for character '{character.Name}' are empty.");
            CollectionAssert.IsNotEmpty(character.Moves, $"Moves for character '{character.Name}' are empty.");
            CollectionAssert.IsNotEmpty(character.Attributes, $"Attributes for character '{character.Name}' are empty.");
        }
    }
}

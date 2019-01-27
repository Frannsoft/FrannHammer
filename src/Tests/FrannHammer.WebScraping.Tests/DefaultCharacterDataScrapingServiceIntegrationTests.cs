using FrannHammer.Domain;
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
using FrannHammer.WebScraping.Contracts.UniqueData;
using FrannHammer.WebScraping.Contracts.WebClients;
using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Images;
using FrannHammer.WebScraping.Movements;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.Unique;
using FrannHammer.WebScraping.WebClients;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static FrannHammer.Tests.Utility.Categories;


namespace FrannHammer.WebScraping.Tests
{
    [TestFixture]
    [TestFixtureSource(nameof(FixtureArgs))]
    public class DefaultCharacterDataScrapingServiceIntegrationTests
    {
        private static IMovementScrapingServices _movementScrapingServices;
        private static IMoveScrapingServices _moveScrapingServices;
        private static IAttributeScrapingServices _attributeScrapingServices;
        private static IHtmlParserProvider _htmlParserProvider;
        private static IMovementProvider _movementProvider;
        private static IPageDownloader _pageDownloader;
        private static IWebClientProvider _webClientProvider;
        private static IAttributeProvider _attributeProvider;
        private static IMoveProvider _moveProvider;
        private static IImageScrapingService _imageScrapingService;
        private static IImageScrapingProvider _imageScrapingProvider;
        private static IMovementScraper _movementScraper;
        private static ICharacterDataScrapingServices _characterDataScrapingServices;
        private static GroundMoveScraper _groundMoveScraper;
        private static AerialMoveScraper _aerialMoveScraper;
        private static SpecialMoveScraper _specialMoveScraper;
        private static ThrowMoveScraper _throwMoveScraper;
        private static ICharacterMoveScraper _characterMoveScraper;
        private static IWebServices _webServices;
        private static DefaultCharacterDataScraper _characterDataScraper;
        private static IUniqueDataScrapingServices _uniqueDataScrapingServices;
        private static IUniqueDataProvider _uniqueDataProvider;

        private readonly string _game;
        private string _urlUnderTest;

        public DefaultCharacterDataScrapingServiceIntegrationTests(string game)
        {
            _game = game;
            _characterDataScraper = MakeCharacterDataScraper();
        }

        private static object[] FixtureArgs =
        {
            new object[]{"Smash4"},
            new object[]{"Ultimate"}
        };

        public DefaultCharacterDataScraper MakeCharacterDataScraper()
        {
            var instanceIdGenerator = new InstanceIdGenerator();
            _htmlParserProvider = new DefaultHtmlParserProvider();
            _movementProvider = new DefaultMovementProvider(instanceIdGenerator);
            _moveProvider = new DefaultMoveProvider(instanceIdGenerator);
            _pageDownloader = new DefaultPageDownloader();
            _webClientProvider = new DefaultWebClientProvider();
            _attributeProvider = new DefaultAttributeProvider(instanceIdGenerator);
            _imageScrapingProvider = new DefaultImageScrapingProvider();
            _imageScrapingService = new DefaultImageScrapingService(_imageScrapingProvider);
            _uniqueDataProvider = new DefaultUniqueDataProvider(instanceIdGenerator);
            _webServices = new DefaultWebServices(_htmlParserProvider, _webClientProvider, _pageDownloader);

            _attributeScrapingServices = new DefaultAttributeScrapingServices(_attributeProvider, _webServices);
            _moveScrapingServices = new DefaultMoveScrapingServices(_moveProvider, _webServices);
            _movementScrapingServices = new DefaultMovementScrapingServices(_movementProvider, _webServices);
            _uniqueDataScrapingServices = new DefaultUniqueDataScrapingServices(_uniqueDataProvider, _webServices);

            _groundMoveScraper = new GroundMoveScraper(_moveScrapingServices);
            _aerialMoveScraper = new AerialMoveScraper(_moveScrapingServices);
            _specialMoveScraper = new SpecialMoveScraper(_moveScrapingServices);
            _throwMoveScraper = new ThrowMoveScraper(_moveScrapingServices);
            _characterMoveScraper = new DefaultCharacterMoveScraper(new List<IMoveScraper> {
                _groundMoveScraper, _aerialMoveScraper, _specialMoveScraper, _throwMoveScraper});

            var attributeScrapers = AttributeScrapers.AllWithScrapingServices(_attributeScrapingServices, _urlUnderTest);

            _movementScraper = new DefaultMovementScraper(_movementScrapingServices);

            _characterDataScrapingServices = new DefaultCharacterDataScrapingServices(_imageScrapingService, _movementScraper,
                attributeScrapers, _characterMoveScraper, _uniqueDataScrapingServices, _webServices, instanceIdGenerator);

            return new DefaultCharacterDataScraper(_characterDataScrapingServices);
        }

        private static IEnumerable<WebCharacter> Characters()
        {
            return new List<WebCharacter> { Domain.Characters.DarkPit, Domain.Characters.Bowser, Domain.Characters.DrMario };
        }

        [Test]
        [Category(LongRunning)]
        [TestCaseSource(nameof(Characters))]
        public void ExpectedCharacterDataCanBeScraped(WebCharacter character)
        {
            _urlUnderTest = $"{Keys.KHSiteBaseUrl}{_game}/";
            _characterDataScraper = MakeCharacterDataScraper();
            character = _characterDataScraper.PopulateCharacterFromWeb(character, _urlUnderTest);

            Assert.That(character.FullUrl, Is.Not.Empty);
            Assert.That(character.ColorTheme, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.Not.Empty);
            Assert.That(character.ThumbnailUrl, Is.Not.Null);
            Assert.That(character.MainImageUrl, Is.Not.Empty);

            Uri testUri;
            Assert.That(Uri.TryCreate(character.MainImageUrl, UriKind.Absolute, out testUri), $"Malformed main image url: '{character.MainImageUrl}'");
            Assert.That(Uri.TryCreate(character.ThumbnailUrl, UriKind.Absolute, out testUri), $"Malformed thumbnail image url: '{character.ThumbnailUrl}'");

            CollectionAssert.IsNotEmpty(character.Movements, $"Movements for character '{character.Name}' are empty.");
            CollectionAssert.IsNotEmpty(character.Moves, $"Moves for character '{character.Name}' are empty.");
            CollectionAssert.IsNotEmpty(character.AttributeRows, $"Attributes Rows for character '{character.Name}' are empty.");
        }
    }
}

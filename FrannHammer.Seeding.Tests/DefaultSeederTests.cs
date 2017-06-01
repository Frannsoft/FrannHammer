using System.Collections.Generic;
using System.Linq;
using FrannHammer.Api.Services;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping;
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
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Images;
using FrannHammer.WebScraping.Movements;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using Moq;
using NUnit.Framework;
using Characters = FrannHammer.WebScraping.Domain.Characters;

namespace FrannHammer.Seeding.Tests
{
    [TestFixture]
    public class DefaultSeederTests
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
        private ThrowMoveScraper _throwMoveScraper;
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
            _throwMoveScraper = new ThrowMoveScraper(_moveScrapingServices);
            _characterMoveScraper = new DefaultCharacterMoveScraper(new List<IMoveScraper> {
                _groundMoveScraper, _aerialMoveScraper, _specialMoveScraper, _throwMoveScraper
                });

            var attributeScrapers = new List<IAttributeScraper>
            {
                new AirSpeedScraper(_attributeScrapingServices),
                new AirDodgeScraper(_attributeScrapingServices)
            };
            _movementScraper = new DefaultMovementScraper(_movementScrapingServices);

            _characterDataScrapingServices = new DefaultCharacterDataScrapingServices(_imageScrapingService, _movementScraper,
                attributeScrapers, _characterMoveScraper, _webServices);

            _characterDataScraper = new DefaultCharacterDataScraper(_characterDataScrapingServices);
        }

        [Test]
        public void CanSeedCharacterData()
        {
            //fake data stores
            var characters = new List<ICharacter>();
            var movements = new List<IMovement>();
            var moves = new List<IMove>();
            var characterAttributes = new List<ICharacterAttributeRow>();

            //mock repos
            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.Add(It.IsAny<ICharacter>())).Callback<ICharacter>(c =>
            {
                characters.Add(c);
            });
            characterRepositoryMock.Setup(c => c.GetAll()).Returns(() => characters);

            var movementRepositoryMock = new Mock<IRepository<IMovement>>();
            movementRepositoryMock.Setup(c => c.AddMany(It.IsAny<IEnumerable<IMovement>>()))
                .Callback<IEnumerable<IMovement>>(c =>
            {
                movements.AddRange(c);
            });
            movementRepositoryMock.Setup(c => c.GetAll()).Returns(() => movements);

            var movesRepositoryMock = new Mock<IRepository<IMove>>();
            movesRepositoryMock.Setup(c => c.AddMany(It.IsAny<IEnumerable<IMove>>()))
                .Callback<IEnumerable<IMove>>(c =>
            {
                moves.AddRange(c);
            });
            movesRepositoryMock.Setup(c => c.GetAll()).Returns(() => moves);

            var characterAttributeRepositoryMock = new Mock<IRepository<ICharacterAttributeRow>>();
            characterAttributeRepositoryMock.Setup(c => c.AddMany(It.IsAny<IEnumerable<ICharacterAttributeRow>>()))
                .Callback<IEnumerable<ICharacterAttributeRow>>(c =>
            {
                characterAttributes.AddRange(c);
            });
            characterAttributeRepositoryMock.Setup(c => c.GetAll()).Returns(() => characterAttributes);

            //real api services using mocked repos
            var movementService = new DefaultMovementService(movementRepositoryMock.Object);
            var moveService = new DefaultMoveService(movesRepositoryMock.Object, new Mock<IQueryMappingService>().Object);
            var characterAttributeService = new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object);
            var dtoProvider = new DefaultDtoProvider();
            var characterService = new DefaultCharacterService(characterRepositoryMock.Object, dtoProvider, movementService, characterAttributeService, moveService);


            //real scraping from web to get data
            var greninja = Characters.Greninja;
            _characterDataScraper.PopulateCharacterFromWeb(greninja);

            //insert data into mock repos using api services
            var seeder = new DefaultSeeder(_characterDataScraper);
            seeder.SeedCharacterData(greninja, characterService, movementService,
                moveService, characterAttributeService);

            //assert data can be retrieved
            Assert.That(characterRepositoryMock.Object.GetAll().Count(), Is.EqualTo(1));
            Assert.That(movesRepositoryMock.Object.GetAll().Count(), Is.GreaterThan(0));
            Assert.That(movementRepositoryMock.Object.GetAll().Count(), Is.GreaterThan(0));
            Assert.That(characterAttributeRepositoryMock.Object.GetAll().Count(), Is.GreaterThan(0));
        }
    }
}

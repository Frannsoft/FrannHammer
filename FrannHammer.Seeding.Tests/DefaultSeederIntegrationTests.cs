﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrannHammer.Api.Services;
using FrannHammer.DataAccess.MongoDb;
using FrannHammer.Domain;
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
using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.HtmlParsing;
using FrannHammer.WebScraping.Images;
using FrannHammer.WebScraping.Movements;
using FrannHammer.WebScraping.Moves;
using FrannHammer.WebScraping.PageDownloading;
using FrannHammer.WebScraping.WebClients;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NUnit.Framework;

namespace FrannHammer.Seeding.Tests
{
    [TestFixture]
    public class DefaultSeederIntegrationTests
    {
        protected IMongoDatabase MongoDatabase { get; private set; }

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            BsonClassMap.RegisterClassMap<MongoModel>(m =>
            {
                m.AutoMap();
            });

            InitializeClassMap(typeof(Character));
            InitializeClassMap(typeof(Movement));
            InitializeClassMap(typeof(Move));
            InitializeClassMap(typeof(DefaultCharacterAttributeRow));

            var mongoClient = new MongoClient(new MongoUrl("mongodb://testuser:password@ds115411.mlab.com:15411/integrationtestfranndotexe"));
            MongoDatabase = mongoClient.GetDatabase("integrationtestfranndotexe");
        }

        private static void InitializeClassMap(Type modelType)
        {
            var classMap = new BsonClassMap(modelType);
            var properties =
                modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(p => p.GetCustomAttribute<FriendlyNameAttribute>(false) != null)
                    .ToList();

            Assert.That(properties.Count > 0);
            classMap.AutoMap();

            foreach (var prop in properties)
            {
                classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
            }

            BsonClassMap.RegisterClassMap(classMap);
        }

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
                new AirSpeedScraper(_attributeScrapingServices),
                new AirDodgeScraper(_attributeScrapingServices)
            };
            _movementScraper = new DefaultMovementScraper(_movementScrapingServices);

            _characterDataScrapingServices = new DefaultCharacterDataScrapingServices(_imageScrapingService, _movementScraper,
                attributeScrapers, _characterMoveScraper, _webServices);

            _characterDataScraper = new DefaultCharacterDataScraper(_characterDataScrapingServices);
        }


        [Test]
        [Explicit("Actually pushes data to the test db")]
        public void CanPushDataToActualMongoDb()
        {
            var characterRepository = new MongoDbRepository<ICharacter>(MongoDatabase);
            var movementRepository = new MongoDbRepository<IMovement>(MongoDatabase);
            var moveRepository = new MongoDbRepository<IMove>(MongoDatabase);
            var characterAttributeRepository = new MongoDbRepository<ICharacterAttributeRow>(MongoDatabase);

            //real api services using mocked repos
            var characterService = new DefaultCharacterService(characterRepository);
            var movementService = new DefaultMovementService(movementRepository);
            var moveService = new DefaultMoveService(moveRepository);
            var characterAttributeService = new DefaultCharacterAttributeService(characterAttributeRepository);

            //real scraping from web to get data
            var greninja = Characters.Greninja;
            _characterDataScraper.PopulateCharacterFromWeb(greninja);

            //insert data into mock repos using api services
            var seeder = new DefaultSeeder(_characterDataScraper);
            seeder.SeedCharacterData(greninja, characterService, movementService,
                moveService, characterAttributeService);

            //assert data can be retrieved
            Assert.That(characterRepository.GetAll().Count(), Is.EqualTo(1));
            Assert.That(moveRepository.GetAll().Count(), Is.GreaterThan(0));
            Assert.That(movementRepository.GetAll().Count(), Is.GreaterThan(0));
            Assert.That(characterAttributeRepository.GetAll().Count(), Is.GreaterThan(0));
        }
    }
}

//TODO - cleanup all dependencies.
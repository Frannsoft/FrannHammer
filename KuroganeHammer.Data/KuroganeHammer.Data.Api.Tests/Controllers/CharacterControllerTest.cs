using System;
using System.Data.Common;
using System.Linq;
using System.Web.Http.Results;
using Effort;
using KuroganeHammer.Data.Api.Controllers;
using NUnit.Framework;
using KuroganeHammer.Data.Api.Models;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTest
    {
        //private IQueryable<Character> _characters;
        //private IQueryable<Movement> _movements;
        //private IQueryable<Move> _moves;
        //private Mock<ApplicationDbContext> _contextMock;

        private DbConnection _connection;
        private ApplicationDbContext _dbContext;
        private CharactersController _charactersController;
        private MovesController _movesController;
        private MovementsController _movementsController;

        [SetUp]
        public void Setup()
        {
            _connection = DbConnectionFactory.CreateTransient();
            _dbContext = new ApplicationDbContext(_connection);
            _charactersController = new CharactersController(_dbContext);
            _movesController = new MovesController(_dbContext);
            _movementsController = new MovementsController(_dbContext);

            //_characters = new List<Character>
            //{
            //    new Character
            //    {
            //        ColorTheme = "#323423",
            //        Description = "desc",
            //        Id = 1,
            //        LastModified = DateTime.Now,
            //        MainImageUrl = "http://img.com/i.png",
            //        Name = "falco",
            //        Style = "prefers the air",
            //        ThumbnailUrl = "http://img.net/ii.png"
            //    }
            //}.AsQueryable();

            //_movements = new List<Movement>
            //{
            //    new Movement
            //    {
            //        Id = 1,
            //        LastModified = DateTime.Now,
            //        Name = "jab 1",
            //        OwnerId = 1,
            //        Value = "3 frames"
            //    }
            //}.AsQueryable();

            //_moves = new List<Move>
            //{
            //    new Move
            //    {
            //        Angle = "40",
            //        AutoCancel = "3",
            //        BaseDamage = "53",
            //        BaseKnockBackSetKnockback = "60/25",
            //        FirstActionableFrame = 2,
            //        HitboxActive = "23-25",
            //        Id = 1,
            //        KnockbackGrowth = "89",
            //        LandingLag = "11",
            //        LastModified = DateTime.Now,
            //        Name = "falco phantasm",
            //        OwnerId = 1,
            //        TotalHitboxActiveLength = 30,
            //        Type = MoveType.Special
            //    }
            //}.AsQueryable();

            //_contextMock = new Mock<ApplicationDbContext>();
        }

        //private Mock<IDbSet<T>> SetupMockDbSet<T>(IQueryable<T> queryableToMock, Func<object[], T> findFunc)
        //    where T : class
        //{
        //    var mockSet = new Mock<IDbSet<T>>();
        //    mockSet.Setup(m => m.Provider).Returns(queryableToMock.Provider);
        //    mockSet.Setup(m => m.Expression).Returns(queryableToMock.Expression);
        //    mockSet.Setup(m => m.ElementType).Returns(queryableToMock.ElementType);
        //    mockSet.Setup(m => m.GetEnumerator()).Returns(queryableToMock.GetEnumerator());
        //    mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns(findFunc);

        //    return mockSet;
        //}

        [Test]
        public void ShouldGetAllCharacters()
        {
            var newCharacter = new Character
            {
                ColorTheme = "#32523",
                Description = "desc",
                Id = 0,
                LastModified = DateTime.Now,
                Name = "mewtwo",
                Style = "phaser"
            };

            _charactersController.PostCharacter(newCharacter);

            var results = _charactersController.GetCharacters();

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetAllMovementsForCharacter()
        {
            var newCharacter = new Character
            {
                ColorTheme = "#32523",
                Description = "desc",
                Id = 0,
                LastModified = DateTime.Now,
                Name = "mewtwo",
                Style = "phaser"
            };

            _charactersController.PostCharacter(newCharacter);

            var movement = new Movement
            {
                Id = 1,
                LastModified = DateTime.Now,
                Name = "jab 1",
                OwnerId = 0,
                Value = "3 frames"
            };

            _movementsController.PostMovement(movement);

            var results = _charactersController.GetMovementsForCharacter(0);

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().Name, Is.EqualTo("jab 1"));
        }

        [Test]
        public void ShouldGetAllMovesForCharacter()
        {
            var newCharacter = new Character
            {
                ColorTheme = "#32523",
                Description = "desc",
                Id = 0,
                LastModified = DateTime.Now,
                Name = "mewtwo",
                Style = "phaser"
            };

            var move = new Move
            {
                Angle = "40",
                AutoCancel = "3",
                BaseDamage = "53",
                BaseKnockBackSetKnockback = "60/25",
                FirstActionableFrame = 2,
                HitboxActive = "23-25",
                Id = 1,
                KnockbackGrowth = "89",
                LandingLag = "11",
                LastModified = DateTime.Now,
                Name = "falco phantasm",
                OwnerId = 0,
                TotalHitboxActiveLength = 30,
                Type = MoveType.Special
            };

            _movesController.PostMove(move);

            _charactersController.PostCharacter(newCharacter);

            var results = _charactersController.GetMovesForCharacter(0);

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().Name, Is.EqualTo("falco phantasm"));
        }

        [Test]
        public void ShouldAddCharacter()
        {
            var newCharacter = new Character
            {
                ColorTheme = "#32523",
                Description = "desc",
                Id = 0,
                LastModified = DateTime.Now,
                Name = "mewtwo",
                Style = "phaser"
            };
            var result = _charactersController.PostCharacter(newCharacter) as CreatedAtRouteNegotiatedContentResult<Character>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.Not.Null);
            Assert.That(result.Content.Name, Is.EqualTo("mewtwo"));
        }

        [Test]
        public void ShouldUpdateCharacter()
        {
            var character = new Character
            {

                ColorTheme = "#32523",
                Description = "desc",
                Id = 0,
                LastModified = DateTime.Now,
                Name = "mewtwo",
                Style = "phaser"
            };

            //arrange
            _charactersController.PostCharacter(character);

            //act
            _charactersController.PutCharacter(0, new Character
            {
                Name = "falco prefers the air",
                Id = 0
            });

            var updatedCharacter = _charactersController.GetCharacter(0) as OkNegotiatedContentResult<Character>;

            //assert
            Assert.That(updatedCharacter.Content.Name, Is.EqualTo("falco prefers the air"));
        }

        [Test]
        public void Effort_ShouldAddCharacter()
        {
            var character = new Character
            {

                ColorTheme = "#32523",
                Description = "desc",
                Id = 0,
                LastModified = DateTime.Now,
                Name = "mewtwo",
                Style = "phaser"
            };

            var result = _charactersController.PostCharacter(character) as CreatedAtRouteNegotiatedContentResult<Character>;

            Assert.That(result.Content.Name, Is.EqualTo("mewtwo"));
        }
    }
}

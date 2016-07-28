using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Dynamic;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using Effort.DataLoaders;
using FrannHammer.Services;
using Newtonsoft.Json;

namespace FrannHammer.Api.Tests.Controllers
{
    //[TestFixture]
    //public class CharacterControllerTests2 : BaseTest
    //{
    //    private CharactersController _controller;
    //    private IMetadataService _service;
    //    //private Mock<IApplicationDbContext> _mockContext;
    //    private DbConnection _connection;
    //    protected ApplicationDbContext Context;

    //    [TestFixtureSetUp]
    //    public void TestFixtureSetUp()
    //    {
    //        //var entities = new List<Character>
    //        //{
    //        //    new Character
    //        //    {
    //        //        Id = 1,
    //        //        Name = "Pikachu"
    //        //    }
    //        //}.AsQueryable();

    //        //_mockContext = new Mock<IApplicationDbContext>();
    //        //var mockDbSet = new Mock<DbSet<Character>>();

    //        ////_service = Init<MetadataService, Character>(chars, mockDbSet);

    //        //mockDbSet.As<IQueryable<Character>>().Setup(t => t.Provider).Returns(entities.Provider);
    //        //mockDbSet.As<IQueryable<Character>>().Setup(t => t.Expression).Returns(entities.Expression);
    //        //mockDbSet.As<IQueryable<Character>>().Setup(t => t.ElementType).Returns(entities.ElementType);
    //        //mockDbSet.As<IQueryable<Character>>().Setup(t => t.GetEnumerator()).Returns(entities.GetEnumerator());
    //        //mockDbSet.Setup(t => t.Find(It.IsAny<int>()))
    //        //    .Returns<object[]>(ids => entities.FirstOrDefault(t => t.Id == (int)ids[0]));


    //        var path = AppDomain.CurrentDomain.BaseDirectory;
    //        IDataLoader loader = new CsvDataLoader($"{path}fakeDb\\");

    //        _connection = Effort.DbConnectionFactory.CreateTransient(loader);
    //        Context = new ApplicationDbContext(_connection);

    //        //_mockContext.Setup(c => c.Set<Character>()).Returns(mockDbSet.Object);
    //        _service = new MetadataService(Context);

    //        _controller = new CharactersController(_service);
    //        Startup.ConfigureAutoMapping();
    //    }

    //    protected dynamic ExecuteAndReturnDynamic(Func<IHttpActionResult> op)
    //    {
    //        var response = op();
    //        var retVal = response as OkNegotiatedContentResult<ExpandoObject>;

    //        Assert.That(retVal, Is.Not.Null);

    //        // ReSharper disable once PossibleNullReferenceException
    //        Assert.That(retVal.Content, Is.Not.Null);

    //        return retVal.Content;
    //    }

    //    protected T ExecuteAndReturnContent<T>(Func<IHttpActionResult> op)
    //    {
    //        var response = op();
    //        var retVal = response as OkNegotiatedContentResult<T>;

    //        Assert.That(retVal, Is.Not.Null);

    //        // ReSharper disable once PossibleNullReferenceException
    //        Assert.That(retVal.Content, Is.Not.Null);

    //        return retVal.Content;
    //    }

    //    [Test]
    //    public void CanGetCharacterByName2()
    //    {
    //        const string expectedName = "Pikachu";
    //        var character = ExecuteAndReturnDynamic(() => _controller.GetCharacterByName(expectedName, "name"));

    //        Assert.That(character, Is.Not.Null);
    //        Assert.That(character.Name, Is.EqualTo(expectedName));
    //    }
    //}

    [TestFixture]
    public class CharacterControllerTests : EffortBaseTest
    {
        private CharactersController _controller;
        private IMetadataService _service;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _service = new MetadataService(Context);
            _controller = new CharactersController(_service);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void CanGetCharacterById()
        {
            var character = ExecuteAndReturnDynamic(() => _controller.GetCharacter(1));
            Assert.That(character, Is.Not.Null);
        }

        [Test]
        public void CanGetCharacterByName()
        {
            const string expectedName = "Pikachu";
            var character = ExecuteAndReturnDynamic(() => _controller.GetCharacterByName(expectedName));

            Assert.That(character, Is.Not.Null);
            Assert.That(character.Name, Is.EqualTo(expectedName));
        }

        //[Test]
        //public void NotFoundResultWhenNoCharacterFoundById()
        //{
        //    var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacter(0));
        //    Assert.That(result, Is.Not.Null);
        //}

        //[Test]
        //public void NotFoundResultWhenNoCharacterFoundByName()
        //{
        //    const string expectedName = "dummyvalue";
        //    var result = ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacterByName(expectedName));

        //    Assert.That(result, Is.Not.Null);
        //}

        //[Test]
        //public void BadRequestReturned_WhenEmptyNameForFoundByName()
        //{
        //    ExecuteAndReturn<BadRequestErrorMessageResult>(() => _controller.GetCharacterByName(string.Empty));
        //}

        //[Test]
        //public void BadRequestReturned_WhenNullNameForFoundByName()
        //{
        //    ExecuteAndReturn<BadRequestErrorMessageResult>(() => _controller.GetCharacterByName(null));
        //}

        [Test]
        public void ShouldGetAllCharacterThrows()
        {
            var throws = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetThrowsForCharacter(1))
                .ToList();

            Assert.That(throws, Is.Not.Empty);
            CollectionAssert.AllItemsAreNotNull(throws);
            CollectionAssert.AllItemsAreUnique(throws);
        }

        [Test]
        public void ShouldGetAllCharacters()
        {
            var characters = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacters())
                .ToList();

            CollectionAssert.AllItemsAreNotNull(characters);
            CollectionAssert.AllItemsAreUnique(characters);
        }

        [Test]
        public void ShouldGetAllMovementsForCharacter()
        {
            var movements = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetMovementsForCharacter(1))
                .ToList();
            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.AllItemsAreUnique(movements);
        }

        [Test]
        public void ShouldGetAllMovesForCharacter()
        {
            var moves = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetMovesForCharacter(1))
                .ToList();
            CollectionAssert.AllItemsAreNotNull(moves);
            CollectionAssert.AllItemsAreUnique(moves);
        }

        [Test]
        public void ShouldAddCharacter()
        {
            var newCharacter = TestObjects.Character();
            var result = ExecuteAndReturnCreatedAtRouteContent<CharacterDto>(() => _controller.PostCharacter(newCharacter));

            var latestCharacter = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacters()).ToList().Last();
            
            Assert.AreEqual(result.ColorTheme, latestCharacter.ColorTheme);
            Assert.AreEqual(result.Description, latestCharacter.Description);
            Assert.AreEqual(result.DisplayName, latestCharacter.DisplayName);
            Assert.AreEqual(result.Id, latestCharacter.Id);
            Assert.AreEqual(result.MainImageUrl, latestCharacter.MainImageUrl);
            Assert.AreEqual(result.Name, latestCharacter.Name);
            Assert.AreEqual(result.Style, latestCharacter.Style);
            Assert.AreEqual(result.ThumbnailUrl, latestCharacter.ThumbnailUrl);
        }

        [Test]
        public void ShouldUpdateCharacter()
        {
            const string expectedName = "mewtwo";
            var result = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacters()).First();

            var json = JsonConvert.SerializeObject(result);
            var character = JsonConvert.DeserializeObject<CharacterDto>(json);

            //act
            if (character != null)
            {
                character.Name = expectedName;
                ExecuteAndReturn<StatusCodeResult>(() => _controller.PutCharacter(character.Id, character));
            }

            var updatedCharacter = ExecuteAndReturnDynamic(() => _controller.GetCharacter(character.Id));

            //assert
            Assert.That(updatedCharacter.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void ShouldDeleteCharacter()
        {
            var character = TestObjects.Character();
            ExecuteAndReturnCreatedAtRouteContent<CharacterDto>(() => _controller.PostCharacter(character));
            _controller.DeleteCharacter(character.Id);
            ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacter(character.Id));
        }
    }
}

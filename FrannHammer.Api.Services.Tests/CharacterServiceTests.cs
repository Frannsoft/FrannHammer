using System;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class CharacterServiceTests : GeneralServiceTests<ICharacter, DefaultCharacterService>
    {

        [Test]
        public void ThrowsArgumentNullExceptionForNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultCharacterService(null);
            });
        }
        //[SetUp]
        //public override void SetUp()
        //{
        //    Fixture.Customizations.Add(
        //        new TypeRelay(
        //            typeof(ICharacter),
        //            typeof(Character)));
        //}

        //[Test]
        //public void AddSingleCharacter()
        //{
        //    var fakeCharacters = Fixture.CreateMany<ICharacter>().ToList();

        //    var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
        //    characterRepositoryMock.Setup(c => c.GetAll()).Returns(() => fakeCharacters);
        //    characterRepositoryMock.Setup(c => c.Add(It.IsAny<ICharacter>())).Callback<ICharacter>(c =>
        //    {
        //        fakeCharacters.Add(c);
        //    });
        //    var service = new DefaultCharacterService(characterRepositoryMock.Object);

        //    int previousCount = service.GetAll().Count();

        //    var newCharacter = Fixture.Create<ICharacter>();
        //    service.Add(newCharacter);

        //    int newCount = service.GetAll().Count();

        //    Assert.That(newCount, Is.EqualTo(previousCount + 1));
        //}

        //[Test]
        //public void ReturnsNullForNoCharacterFoundById()
        //{
        //    var fakeCharacters = Fixture.CreateMany<ICharacter>().ToList();

        //    var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
        //    characterRepositoryMock.Setup(c => c.Get(It.IsAny<string>())).Returns<string>(id => fakeCharacters.FirstOrDefault(c => c.Id == id.ToString()));

        //    var service = new DefaultCharacterService(characterRepositoryMock.Object);

        //    var character = service.Get("0");

        //    Assert.That(character, Is.Null);
        //}

        //[Test]
        //public void Error_RejectsNullRepositoryInConstructor()
        //{
        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        // ReSharper disable once ObjectCreationAsStatement
        //        new DefaultCharacterService(null);
        //    });
        //}

        //[Test]
        //public void Error_RejectsNullCharacterForAddition()
        //{
        //    var characterRepositoryMock = new Mock<IRepository<ICharacter>>();

        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        var service = new DefaultCharacterService(characterRepositoryMock.Object);

        //        service.Add(null);
        //    });
        //}
    }
}

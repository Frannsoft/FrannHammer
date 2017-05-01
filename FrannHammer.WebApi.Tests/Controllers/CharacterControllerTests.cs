using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using NUnit.Framework;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTests : BaseControllerTests
    {
        [SetUp]
        public override void SetUp()
        {
            Fixture.Customizations.Add(
                new TypeRelay(
                    typeof(ICharacter),
                    typeof(Character)));
        }

        [Test]
        public void ConstructorRejectsNullCharacterAttributeService()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharacterController(null);
            });
        }

        [Test]
        public void GetCharacterDisplayName()
        {
            var testCharacter = Fixture.Create<ICharacter>();
            var characterServiceMock = new Mock<ICharacterService>();
            characterServiceMock.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(() => testCharacter);
            var controller = new CharacterController(characterServiceMock.Object);

            var response = controller.GetCharacter(testCharacter.Id) as OkNegotiatedContentResult<ICharacter>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var character = response.Content;

            Assert.That(character.DisplayName, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.EqualTo(testCharacter.DisplayName), $"Character name was not equal to {testCharacter.DisplayName}");
        }

        [Test]
        public void Error_ReturnsNotFoundResultWhenCharacterDoesNotExist()
        {
            var testCharacter = Fixture.Create<ICharacter>();

            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.Get(It.IsInRange("0", "1", Range.Inclusive))).Returns(() => testCharacter);

            var controller = new CharacterController(new DefaultCharacterService(characterRepositoryMock.Object));
            var response = controller.GetCharacter("-1") as NotFoundResult;

            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void GetReturnsExpectedCharacterById()
        {
            var testCharacter1 = Fixture.Create<ICharacter>();
            var testCharacter2 = Fixture.Create<ICharacter>();

            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.Get(It.IsInRange("0", testCharacter1.Id, Range.Inclusive))).Returns(() => testCharacter1);
            characterRepositoryMock.Setup(c => c.Get(It.IsInRange("2", "3", Range.Inclusive))).Returns(() => testCharacter2);

            var controller = new CharacterController(new DefaultCharacterService(characterRepositoryMock.Object));
            var response = controller.GetCharacter(testCharacter1.Id) as OkNegotiatedContentResult<ICharacter>;

            // ReSharper disable once PossibleNullReferenceException
            var character = response.Content;

            Assert.That(character.Name, Is.Not.Empty);
            Assert.That(character.Name, Is.EqualTo(testCharacter1.Name), $"Character name was not equal to {testCharacter1.Name}");
        }

        [Test]
        public void GetAllReturnsAllCharacters()
        {
            var fakeCharacters = Fixture.CreateMany<ICharacter>().ToList();

            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.GetAll()).Returns(() => fakeCharacters);

            var controller = new CharacterController(new DefaultCharacterService(characterRepositoryMock.Object));
            var response = controller.GetAllCharacters() as OkNegotiatedContentResult<IEnumerable<ICharacter>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var characters = response.Content.ToList();

            Assert.That(characters.Count, Is.EqualTo(fakeCharacters.Count));
        }
    }
}

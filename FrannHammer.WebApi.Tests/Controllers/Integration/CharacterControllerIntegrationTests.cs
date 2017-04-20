using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace FrannHammer.WebApi.Tests.Controllers.Integration
{
    [TestFixture]
    public class CharacterControllerIntegrationTests
    {
        [Test]
        public void Error_ReturnsNotFoundResultWhenCharacterDoesNotExist()
        {
            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.Get(It.IsInRange(0, 1, Range.Inclusive))).Returns(() => new
                Character
            {
                Name = "test"
            });

            var controller = new CharacterController(new DefaultCharacterService(characterRepositoryMock.Object));
            var response = controller.GetCharacter(-1) as NotFoundResult;

            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void GetReturnsExpectedCharacterById()
        {
            const string characterName = "test";

            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.Get(It.IsInRange(0, 1, Range.Inclusive))).Returns(() => new
                Character
            {
                ColorTheme = "#fff",
                Name = characterName
            });
            characterRepositoryMock.Setup(c => c.Get(It.IsInRange(2, 3, Range.Inclusive))).Returns(() => new
                Character
            {
                Name = "test2"
            });

            var controller = new CharacterController(new DefaultCharacterService(characterRepositoryMock.Object));
            var response = controller.GetCharacter(1) as OkNegotiatedContentResult<ICharacter>;

            // ReSharper disable once PossibleNullReferenceException
            var character = response.Content;

            Assert.That(character.Name, Is.Not.Empty);
            Assert.That(character.Name, Is.EqualTo(characterName), $"Character name was not equal to {characterName}");
        }

        [Test]
        public void GetAllReturnsAllCharacters()
        {
            var characterRepositoryMock = new Mock<IRepository<ICharacter>>();
            characterRepositoryMock.Setup(c => c.GetAll()).Returns(() =>
                new List<ICharacter>
                {
                    new Character {Name = "one"},
                    new Character {Name = "two"}
                }
            );

            var controller = new CharacterController(new DefaultCharacterService(characterRepositoryMock.Object));
            var response = controller.GetAllCharacters() as OkNegotiatedContentResult<IEnumerable<ICharacter>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var characters = response.Content.ToList();

            Assert.That(characters.Count, Is.EqualTo(2));
        }
    }
}

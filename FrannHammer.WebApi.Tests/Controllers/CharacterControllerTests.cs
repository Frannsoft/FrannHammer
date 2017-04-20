using System;
using System.Web.Http.Results;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebScraping.Domain;
using NUnit.Framework;
using Moq;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTests
    {
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
            var characterServiceMock = new Mock<ICharacterService>();
            characterServiceMock.Setup(c => c.Get(It.IsAny<int>(), It.IsAny<string>())).Returns(() => new Character
            {
                DisplayName = Characters.Greninja.DisplayName
            });
            var controller = new CharacterController(characterServiceMock.Object);

            var response = controller.GetCharacter(0) as OkNegotiatedContentResult<ICharacter>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var character = response.Content;

            Assert.That(character.DisplayName, Is.Not.Empty);
            Assert.That(character.DisplayName, Is.EqualTo(Characters.Greninja.DisplayName), $"Character name was not equal to {Characters.Greninja.DisplayName}");
        }
    }
}

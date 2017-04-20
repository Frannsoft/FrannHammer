using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Services;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace FrannHammer.WebApi.Tests.Controllers.Integration
{
    [TestFixture]
    public class CharacterAttributeControllerIntegrationTests
    {
        [Test]
        public void Error_ReturnsNotFoundResultWhenAttributeDoesNotExist()
        {
            var characterAttributeRepositoryMock = new Mock<IRepository<IAttribute>>();
            characterAttributeRepositoryMock.Setup(c => c.Get(It.IsInRange(0, 1, Range.Inclusive))).Returns(() => new CharacterAttribute
            {
                Name = "test"
            });

            var controller =
                new CharacterAttributeController(
                    new DefaultCharacterAttributeService(characterAttributeRepositoryMock.Object));

            var response = controller.GetCharacterAttribute(-1) as NotFoundResult;

            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void GetACharacterAttributeName()
        {
            var characterAttributeServiceMock = new Mock<ICharacterAttributeService>();
            characterAttributeServiceMock.Setup(c => c.Get(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => new CharacterAttribute
                {
                    Name = "testname"
                });

            var controller = new CharacterAttributeController(characterAttributeServiceMock.Object);

            var response = controller.GetCharacterAttribute(0) as OkNegotiatedContentResult<IAttribute>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var attribute = response.Content;

            Assert.That(attribute.Name, Is.Not.Empty);
            Assert.That(attribute.Name, Is.EqualTo("testname"), "Character name was not equal to testname");
        }

        [Test]
        public void GetManyCharacterAttributes()
        {
            var characterAttributeServiceMock = new Mock<ICharacterAttributeService>();
            characterAttributeServiceMock.Setup(c => c.GetAll(It.IsAny<string>()))
                .Returns(() => new List<IAttribute>
                {
                    new CharacterAttribute
                    {
                        Name = "testname"
                    },
                    new CharacterAttribute
                    {
                        Name = "testname2"
                    }
                });

            var controller = new CharacterAttributeController(characterAttributeServiceMock.Object);

            var response = controller.GetCharacterAttributes() as OkNegotiatedContentResult<IEnumerable<IAttribute>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var attributes = response.Content.ToList();

            CollectionAssert.AllItemsAreUnique(attributes);
            CollectionAssert.IsNotEmpty(attributes);

            attributes.ForEach(attribute =>
            {
                Assert.That(attribute.Name, Is.Not.Empty);
                Assert.That(attribute.Name, Is.Not.Empty);
            });
        }
    }
}

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
using Moq;
using NUnit.Framework;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class MoveControllerTests
    {
        [Test]
        public void ConstructorRejectsNullCharacterAttributeService()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MoveController(null);
            });
        }

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
        public void GetAMoveName()
        {
            const string expectedName = "testName";
            var moveServiceMock = new Mock<IMoveService>();
            moveServiceMock.Setup(c => c.Get(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => new Move
                {
                    Name = expectedName
                });

            var controller = new MoveController(moveServiceMock.Object);

            var response = controller.GetMove(0) as OkNegotiatedContentResult<IMove>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var move = response.Content;

            Assert.That(move.Name, Is.Not.Empty);
            Assert.That(move.Name, Is.EqualTo(expectedName), $"move name was not equal to {expectedName}");
        }

        [Test]
        public void GetManyMoves()
        {
            var moveServiceMock = new Mock<IMoveService>();
            moveServiceMock.Setup(c => c.GetAll(It.IsAny<string>()))
                .Returns(() => new List<IMove>
                {
                    new Move
                    {
                        Name = "testname"
                    },
                    new Move
                    {
                        Name = "testname2"
                    }
                });

            var controller = new MoveController(moveServiceMock.Object);

            var response = controller.GetAllMoves() as OkNegotiatedContentResult<IEnumerable<IMove>>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            var moves = response.Content.ToList();

            CollectionAssert.AllItemsAreUnique(moves);
            CollectionAssert.IsNotEmpty(moves);

            moves.ForEach(attribute =>
            {
                Assert.That(attribute.Name, Is.Not.Empty);
                Assert.That(attribute.Name, Is.Not.Empty);
            });
        }
    }
}

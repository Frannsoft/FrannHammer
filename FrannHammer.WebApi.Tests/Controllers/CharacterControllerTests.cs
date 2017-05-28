using System;
using FrannHammer.Api.Services;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTests : BaseControllerTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullCharacterServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharacterController(null,
                    new DefaultMoveService(new Mock<IRepository<IMove>>().Object, new Mock<IQueryMappingService>().Object),
                    new DefaultMovementService(new Mock<IRepository<IMovement>>().Object),
                    new DefaultCharacterAttributeService(new Mock<IRepository<ICharacterAttributeRow>>().Object),
                    new DefaultDtoProvider());
            });
        }

        [Test]
        public void ThrowsArgumentNullExceptionForNullMoveServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharacterController(
                    new DefaultCharacterService(new Mock<IRepository<ICharacter>>().Object),
                    null,
                    new DefaultMovementService(new Mock<IRepository<IMovement>>().Object),
                    new DefaultCharacterAttributeService(new Mock<IRepository<ICharacterAttributeRow>>().Object),
                    new DefaultDtoProvider());
            });
        }

        [Test]
        public void ThrowsArgumentNullExceptionForNullMovementServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharacterController(new DefaultCharacterService(new Mock<IRepository<ICharacter>>().Object),
                    new DefaultMoveService(new Mock<IRepository<IMove>>().Object, new Mock<IQueryMappingService>().Object),
                   null,
                    new DefaultCharacterAttributeService(new Mock<IRepository<ICharacterAttributeRow>>().Object),
                    new DefaultDtoProvider());
            });
        }
    }
}

using System;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class DefaultCharacterServiceTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultCharacterService(null,
                    new DefaultDtoProvider(),
                    new DefaultMovementService(new Mock<IRepository<IMovement>>().Object, new Mock<IQueryMappingService>().Object),
                    new DefaultCharacterAttributeService(new Mock<IRepository<ICharacterAttributeRow>>().Object),
                    new DefaultMoveService(new Mock<IRepository<IMove>>().Object, new QueryMappingService(new Mock<IAttributeStrategy>().Object)));
            });
        }
    }
}

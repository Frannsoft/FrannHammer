using System;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class DefaultMovementServiceTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultMovementService(null, new Mock<IQueryMappingService>().Object);
            });
        }
    }
}
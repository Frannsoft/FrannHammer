using System;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class MovementServiceTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultMovementService(null);
            });
        }
    }
}
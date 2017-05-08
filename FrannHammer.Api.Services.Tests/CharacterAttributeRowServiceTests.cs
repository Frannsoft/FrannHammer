using System;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class CharacterAttributeRowServiceTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullRepositoryInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new DefaultCharacterAttributeService(null);
            });
        }
    }
}
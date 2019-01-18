using System;
using FrannHammer.NetCore.WebApi.Controllers;
using NUnit.Framework;

namespace FrannHammer.NetCore.WebApi.Tests.Controllers
{
    [TestFixture]
    public class CharacterAttributeControllerTests : BaseControllerTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullCharacterAttributeRowServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharacterAttributeController(null, null);
            });
        }
    }
}

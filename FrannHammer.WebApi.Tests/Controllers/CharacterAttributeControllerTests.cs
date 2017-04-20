using System;
using FrannHammer.WebApi.Controllers;
using NUnit.Framework;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class CharacterAttributeControllerTests
    {
        [Test]
        public void ConstructorRejectsNullCharacterAttributeService()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharacterAttributeController(null);
            });
        }
    }
}

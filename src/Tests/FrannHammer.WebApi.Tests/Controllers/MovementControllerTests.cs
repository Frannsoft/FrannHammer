using FrannHammer.NetCore.WebApi.Controllers;
using NUnit.Framework;
using System;

namespace FrannHammer.NetCore.WebApi.Tests.Controllers
{
    [TestFixture]
    public class MovementControllerTests : BaseControllerTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullMovementServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MovementController(null, null);
            });
        }
    }
}

using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Controllers;
using NUnit.Framework;
using System;

namespace FrannHammer.NetCore.WebApi.Tests.Controllers
{
    [TestFixture]
    public class MoveControllerTests : BaseControllerTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            MoveParseClassMap.RegisterType<IMove, Move>();
        }

        [TearDown]
        public void TearDown()
        {
            MoveParseClassMap.ClearAllRegisteredTypes();
        }

        [Test]
        public void ThrowsArgumentNullExceptionForNullMoveServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MoveController(null, null);
            });
        }
    }
}

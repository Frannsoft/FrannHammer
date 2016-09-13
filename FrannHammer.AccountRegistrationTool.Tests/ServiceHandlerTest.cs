using System;
using FrannHammer.AccountRegistrationTool.Models;
using NUnit.Framework;

namespace FrannHammer.AccountRegistrationTool.Tests
{
    [TestFixture]
    public class ServiceHandlerTest
    {
        private const string FakeUri = "http://localhost/";

        [Test]
        public void ShouldThrowIfRegisterIsCalledBeforeAuthenticated()
        {
            var handler = new ServiceHandler(FakeUri);
            var model = new RegisterUserModel();

            Assert.That(() => handler.RegisterNewUser(model), Throws.InvalidOperationException);
        }

        [Test]
        public void ShouldThrowArgumentExceptionIfModelNullWhenRegistering()
        {
            var handler = new ServiceHandler(FakeUri);

            Assert.Throws<ArgumentNullException>(() => handler.RegisterNewUser(null));
        }
    }
}

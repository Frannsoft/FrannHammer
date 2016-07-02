using System;
using FrannHammer.AccountRegistrationTool.Models;
using NUnit.Framework;

namespace FrannHammer.AccountRegistrationTool.Tests
{
    [TestFixture]
    public class ServiceHandlerTest
    {
        private const string _fakeUri = "http://localhost/";

        [Test]
        public void ShouldThrowIfRegisterIsCalledBeforeAuthenticated()
        {
            var handler = new ServiceHandler(_fakeUri);
            var model = new RegisterUserModel();

            Assert.That(() => handler.RegisterNewUser(model), Throws.InvalidOperationException);
        }

        [Test]
        public void ShouldThrowArgumentExceptionIfModelNullWhenRegistering()
        {
            var handler = new ServiceHandler(_fakeUri);

            Assert.Throws<ArgumentNullException>(() => handler.RegisterNewUser(null));
        }
    }
}

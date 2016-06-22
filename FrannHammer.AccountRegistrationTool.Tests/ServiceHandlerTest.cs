using System;
using FrannHammer.AccountRegistrationTool.Models;
using FrannHammer.Core.Models;
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

        //TODO: need to mock the db and server so this isn't a full integration test
        [Test]
        [Explicit("Full integration test currently")]
        public void ShouldCreateUser()
        {
            var handler = new ServiceHandler("http://localhost/KHApi/api/");

            var model = new RegisterUserModel
            {
                BindingModel = new RegisterBindingModel
                {
                    UserName = "username44444432222",
                    Password = "Password123!",
                    ConfirmPassword = "Password123!",
                    Email = "viaapp24444432222@email.com"
                },
                Roles = new Roles(new[] {"Basic"})
            };


            Assert.That(() => handler.RegisterNewUser(model), Throws.Nothing);
        }
    }
}

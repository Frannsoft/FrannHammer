using System.Collections.Generic;
using AccountRegistrationTool.ViewModels;
using NUnit.Framework;

namespace AccountRegistrationTool.Tests
{
    [TestFixture]
    public class MainVmTest
    {
        private string _username = "username";
        private string _email = "email@email.com";
        private string _confirmEmail = "email@email.com";
        private string _password = "password";
        private string _confirmpassword = "password";
        private Dictionary<string, bool> _basicroles;
        private Dictionary<string, bool> _adminroles;


        [SetUp]
        public void SetUp()
        {
            _basicroles = new Dictionary<string, bool> { { "Basic", true } };
            _adminroles = new Dictionary<string, bool> { { "Basic", true }, { "Admin", true } };
        }

        [Test]
        public void ShouldNotAttemptToRegisterIfUserNameNotFilledIn()
        {
            var vm = new MainVm();

            Assert.That(() => vm.Register(string.Empty, _email, _confirmEmail, _password, _confirmpassword, _basicroles),
                Throws.Exception);
        }

        [Test]
        public void ShouldNotAttemptToRegisterIfEmailNotFilledIn()
        {
            var vm = new MainVm();

            Assert.That(() => vm.Register(_username, string.Empty, _confirmEmail, _password, _confirmpassword, _basicroles),
                Throws.Exception);
        }

        [Test]
        public void ShouldNotAttemptToRegisterIfConfirmEmailNotFilledIn()
        {
            var vm = new MainVm();

            Assert.That(() => vm.Register(_username, _email, string.Empty, _password, _confirmpassword, _basicroles),
                Throws.Exception);
        }

        [Test]
        public void ShouldNotAttemptToRegisterIfPasswordNotFilledIn()
        {
            var vm = new MainVm();

            Assert.That(() => vm.Register(_username, _email, _confirmEmail, string.Empty, _confirmpassword, _basicroles),
                Throws.Exception);
        }

        [Test]
        public void ShouldNotAttemptToRegisterIfConfirmPasswordNotFilledIn()
        {
            var vm = new MainVm();

            Assert.That(() => vm.Register(_username, _email, _confirmEmail, _password, string.Empty, _basicroles),
                Throws.Exception);
        }

        [Test]
        public void ShouldNotAttemptToRegisterIfRolesNotFilledIn()
        {
            var vm = new MainVm();

            Assert.That(() => vm.Register(_username, _email, _confirmEmail, _password, string.Empty, new Dictionary<string, bool>()),
                Throws.Exception);
        }
    }
}

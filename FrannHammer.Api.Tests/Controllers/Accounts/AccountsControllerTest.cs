using System.Net;
using System.Threading.Tasks;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.Accounts
{
    [TestFixture]
    public class AccountsControllerTest : BaseControllerTest
    {
        private string _uriBase = "/api/account";
        private string _uri = string.Empty;

        [Test]
        [Ignore("Not mocking the DB in this scenario currently so this is too annoying to run.")]
        public async Task ShouldAddNewUser()
        {
            _uri = _uriBase + "/register";

            var model = new RegisterBindingModel
            {
                UserName = "user",
                Email = "user@email.com",
                Password = "password!1",
                ConfirmPassword = "password!1"
            };

            var response = await PostAsync(_uri, model);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}

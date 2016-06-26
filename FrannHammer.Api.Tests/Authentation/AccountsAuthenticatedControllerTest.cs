using System.Net;
using System.Threading.Tasks;
using FrannHammer.Api.Tests.Controllers;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Authentation
{
    public class AccountsAuthenticationControllerTest : BaseAuthenticationTest
    {
        private const string UriBase = "api/account/";

        [Test]
        [TestCase(UriBase + "users")]
        public async Task ShouldGetUnauthorizedWithoutLogin_GetUsers(string uri)
        {
            var response = await GetAsync(uri);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        //[Test]
        //[TestCase(UriBase + "user/1")]
        //public async Task ShouldGetUnauthorizedWithoutLogin_GetUser(string uri)
        //{
        //    var response = await GetAsync(uri);
        //    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        //}

        [Test]
        [TestCase(UriBase + "userinfo")]
        public async Task ShouldGetUnauthorizedWithoutLogin_GetUserInfo(string uri)
        {
            var response = await GetAsync(uri);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(UriBase + "logout")]
        public async Task ShouldGetUnauthorizedWithoutLogin_Logout(string uri)
        {
            var response = await PostAsync(uri, default(object));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(UriBase + "manageinfo")]
        public async Task ShouldGetUnauthorizedWithoutLogin_GetManageInfo(string uri)
        {
            var response = await GetAsync(uri + "?returnUrl=%2f&generateState=true");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(UriBase + "changepassword")]
        public async Task ShouldGetUnauthorizedWithoutLogin_ChangePassword(string uri)
        {
            var response = await PostAsync(uri, default(ChangePasswordBindingModel));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(UriBase + "removelogin")]
        public async Task ShouldGetUnauthorizedWithoutLogin_RemoveLogin(string uri)
        {
            var response = await PostAsync(uri, default(RemoveLoginBindingModel));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}

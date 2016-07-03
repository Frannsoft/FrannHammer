﻿using System.Net;
using System.Threading.Tasks;
using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Authentation
{
    [TestFixture]
    public class AccountsAuthenticationControllerTest : BaseAuthenticationTest
    {
        private const string UriBase = "api/account/";

        [Test]
        public async Task CanLogin() => await Login();

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

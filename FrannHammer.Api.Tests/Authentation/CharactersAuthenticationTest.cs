﻿using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Authentation
{
    [TestFixture]
    public class CharactersAuthenticationTest : BaseAuthenticationTest
    {
        private const string BaseUri = "/api/characters";

        //[Test]
        //[TestCase(BaseUri)]
        //[TestCase(BaseUri + "/2")]
        //[TestCase(BaseUri + "/4/movements")]
        //[TestCase(BaseUri + "/10/moves")]
        //public async Task ShouldGetUnauthorizedWithoutLogin_GET(string uri)
        //{
        //    var response = await GetAsync(uri);

        //    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        //}

        [Test]
        [TestCase(BaseUri)]
        public async Task ShouldGetUnauthorizedWithoutLogin_POST(string uri)
        {
            var character = TestObjects.Character();

            var response = await PostAsync(uri, character);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri + "/4")]
        public async Task ShouldGetUnauthorizedWithoutLogin_PUT(string uri)
        {
            var character = TestObjects.Character();

            var response = await PutAsync(uri, character);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri + "/1")]
        public async Task ShouldGetUnauthorizedWithoutLogin_DELETE(string uri)
        {
            var response = await DeleteAsync(uri);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}
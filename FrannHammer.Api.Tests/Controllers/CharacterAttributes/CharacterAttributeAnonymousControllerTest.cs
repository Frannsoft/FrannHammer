﻿using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.CharacterAttributes
{
    [TestFixture]
    public class CharacterAttributeAnonymousControllerTest : BaseControllerTest
    {
        private const string BaseUri = "/api/characterattributes";

        //[Test]
        //[TestCase(BaseUri)]
        //[TestCase(BaseUri + "/2")]
        //public async Task ShouldGetUnauthorizedWithoutLogin_GET(string uri)
        //{
        //    var response = await GetAsync(uri);

        //    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        //}

        [Test]
        [TestCase(BaseUri)]
        [Ignore("Still working to setup Owin self hosting")]
        public async Task ShouldGetUnauthorizedWithoutLogin_POST(string uri)
        {
            var characterAttribute = TestObjects.CharacterAttribute();

            var response = await PostAsync(uri, characterAttribute);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri + "/4")]
        [Ignore("Still working to setup Owin self hosting")]
        public async Task ShouldGetUnauthorizedWithoutLogin_PUT(string uri)
        {
            var characterAttribute = TestObjects.CharacterAttribute();

            var response = await PutAsync(uri, characterAttribute);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri + "/1")]
        [Ignore("Still working to setup Owin self hosting")]
        public async Task ShouldGetUnauthorizedWithoutLogin_DELETE(string uri)
        {
            var response = await DeleteAsync(uri);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}

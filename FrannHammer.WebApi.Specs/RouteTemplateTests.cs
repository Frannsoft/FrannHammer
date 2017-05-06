using System.Net;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace FrannHammer.WebApi.MongoDb.Integration.Tests
{
    [TestFixture]
    public class RouteTemplateTests
    {
        private TestServer _testServer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testServer = TestServer.Create<Startup>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _testServer.Dispose();
        }

        [Test]
        public void CanGetAllCharacters()
        {
            var result = _testServer.HttpClient.GetAsync("api/characters").Result;
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CanGetAllMoves()
        {
            var result = _testServer.HttpClient.GetAsync("api/moves").Result;
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}

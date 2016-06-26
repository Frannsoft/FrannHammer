using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Authentation
{
    [TestFixture]
    public class MoveAuthControllerTest : BaseAuthenticationTest
    {
        private const string BaseUri = "/api/moves";

        [Test]
        [TestCase(BaseUri)]
        public async Task ShouldGetUnauthorizedWithoutLogin_POST(string uri)
        {
            var move = TestObjects.Move();

            var response = await PostAsync(uri, move);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri + "/4")]
        public async Task ShouldGetUnauthorizedWithoutLogin_PUT(string uri)
        {
            var move = TestObjects.Move();

            var response = await PutAsync(uri, move);
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

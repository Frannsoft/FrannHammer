using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Controllers.Moves
{
    [TestFixture]
    public class MoveAnonymousControllerTest : BaseControllerTest
    {
        private const string BaseUri = "/api/moves";

        [Test]
        [TestCase(BaseUri)]
        [TestCase(BaseUri + "/2")]
        [TestCase(BaseUri + "/4/movements")]
        [TestCase(BaseUri + "/10/moves")]
        public async Task ShouldGetUnauthorizedWithoutLogin_GET(string uri)
        {
            var response = await GetAsync(uri);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

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

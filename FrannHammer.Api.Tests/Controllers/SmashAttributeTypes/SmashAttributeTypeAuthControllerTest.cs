using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.SmashAttributeTypes
{
    [TestFixture]
    public class SmashAttributeTypeAuthControllerTest : BaseAuthenticationTest
    {
        private const string BaseUri = "/api/smashattributetypes";

        [Test]
        [TestCase(BaseUri)]
        public async Task ShouldGetUnauthorizedWithoutLogin_POST(string uri)
        {
            var smashAttributeType = TestObjects.SmashAttributeType();

            var response = await PostAsync(uri, smashAttributeType);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        [TestCase(BaseUri + "/4")]
        public async Task ShouldGetUnauthorizedWithoutLogin_PUT(string uri)
        {
            var smashAttributeType = TestObjects.SmashAttributeType();

            var response = await PutAsync(uri, smashAttributeType);
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

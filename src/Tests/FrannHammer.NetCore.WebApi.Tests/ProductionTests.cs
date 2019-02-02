using System.Linq;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using static FrannHammer.Tests.Utility.Categories;

namespace FrannHammer.WebApi.Tests
{
    [TestFixture]
    [Category(Production)]
    public class ProductionTests
    {
        [Test]
        public void PingFromExternalSource_CORSHeaderIsPresentOnResponse()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://beta-api-kuroganehammer.azurewebsites.net/api/characters").Result;
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to successfully ping beta server.");

                var corsHeader = response.Headers.GetValues("access-control-allow-origin").FirstOrDefault();
                Assert.That(corsHeader, Is.Not.Null, $"{nameof(corsHeader)}");

                Assert.That(corsHeader, Is.EqualTo("*"));
            }
        }
    }
}

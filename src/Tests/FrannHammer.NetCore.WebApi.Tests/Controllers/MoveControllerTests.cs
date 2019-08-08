using FrannHammer.NetCore.WebApi.Controllers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FrannHammer.NetCore.WebApi.Tests.Controllers
{
    [TestFixture]
    public class MoveControllerTests : BaseControllerTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullMoveServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new MoveController(null, null);
            });
        }

        [Test]
        public async Task InvalidUrlThrowsResourceNotFoundException()
        {
            var response = await TestServer.GetAsync($"api/moves/ryu");

            Assert.That(response.StatusCode == HttpStatusCode.NotFound);

            string responseMessage = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).Message.ToString();
            Assert.That(responseMessage, Is.EqualTo($"Resource of type 'IMove' not found."));
        }
    }
}

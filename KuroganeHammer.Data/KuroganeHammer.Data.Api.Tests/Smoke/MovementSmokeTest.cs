using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Smoke
{
    [TestFixture]
    public class MovementSmokeTest : BaseSmokeTest
    {
        private List<Movement> _movements;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _movements = LoggedInBasicClient.GetAsync(Baseuri + MovementsRoute).Result.Content.ReadAsAsync<List<Movement>>().Result;
        }

        [Test]
        public void ShouldGetAllMovements()
        {
            CollectionAssert.AllItemsAreNotNull(_movements);
            CollectionAssert.AllItemsAreUnique(_movements);
        }

        [Test]
        public async Task ShouldGetMovement()
        {
            var result = await LoggedInBasicClient.GetAsync(Baseuri + MovementsRoute + "/" + _movements[0].Id);

            Assert.IsTrue(result.IsSuccessStatusCode);

            var movement = result.Content.ReadAsAsync<Movement>().Result;
            Assert.That(movement != null);
            Assert.That(movement.Name.Length > 0);
        }

        [Test]
        public async Task ShouldNotGetAllMovementsDueToNoAuth()
        {
            var result = await AnonymousClient.GetAsync(Baseuri + MovementsRoute);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}

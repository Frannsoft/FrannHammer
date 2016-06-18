using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Linq;
using KuroganeHammer.Data.Api.DTOs;
using KuroganeHammer.Data.Core.Models;

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
        [TestCase("Weight")]
        public async Task ShouldGetAllMovementsByName(string name)
        {
            var results = await LoggedInBasicClient.GetAsync(Baseuri + MovementsRoute + "/byname" + "?name=" + name);

            Assert.IsTrue(results.IsSuccessStatusCode);

            var movements = await results.Content.ReadAsAsync<List<MovementDto>>();

            Assert.That(movements != null);
            Assert.That(movements.Count > 0);
            Assert.That(movements.All(m => m.Name.Equals(name)));
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

        //[Test]
        //public async Task ShouldNotGetAllMovementsDueToNoAuth()
        //{
        //    var result = await AnonymousClient.GetAsync(Baseuri + MovementsRoute);
        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        //}
    }
}

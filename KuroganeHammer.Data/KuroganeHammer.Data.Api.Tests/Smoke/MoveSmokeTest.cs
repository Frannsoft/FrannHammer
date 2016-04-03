using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;
using System.Net.Http;

namespace KuroganeHammer.Data.Api.Tests.Smoke
{
    [TestFixture]
    public class MoveSmokeTest : BaseSmokeTest
    {
        private List<Move> _moves;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _moves = LoggedInBasicClient.GetAsync(Baseuri + MovesRoute).Result.Content.ReadAsAsync<List<Move>>().Result;
        }

        [Test]
        public void ShouldGetAllMoves()
        {
            CollectionAssert.AllItemsAreNotNull(_moves);
            CollectionAssert.AllItemsAreUnique(_moves);
        }

        [Test]
        public async Task ShouldGetMove()
        {
            var result = await LoggedInBasicClient.GetAsync(Baseuri + MovesRoute + "/" + _moves[0].Id);
            Assert.IsTrue(result.IsSuccessStatusCode);

            var move = result.Content.ReadAsAsync<Move>().Result;
            Assert.That(move != null);
            Assert.That(move.Name.Length > 0);
        }

        [Test]
        public async Task ShouldNotGetAllMovesDueToNoAuth()
        {
            var result = await AnonymousClient.GetAsync(Baseuri + MovesRoute);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}

﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Api.DTOs;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Smoke
{
    [TestFixture]
    public class MoveSmokeTest : BaseSmokeTest
    {
        private List<Move> _moves;

        [SetUp]
        [Ignore("Still working to setup Owin self hosting")]
        public override void SetUp()
        {
            base.SetUp();

            var response = LoggedInBasicClient.GetAsync(Baseuri + MovesRoute).Result;
            response.EnsureSuccessStatusCode();

            _moves = response.Content.ReadAsAsync<List<Move>>().Result;
        }

        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public void ShouldGetAllMoves()
        {
            CollectionAssert.AllItemsAreNotNull(_moves);
            CollectionAssert.AllItemsAreUnique(_moves);
        }

        [Test]
        [TestCase("Jab 2")]
        [Ignore("Still working to setup Owin self hosting")]
        public async Task ShouldGetAllMovesByName(string name)
        {
            var results = await LoggedInBasicClient.GetAsync(Baseuri + MovesRoute + "/byname" + "?name=" + name);

            Assert.IsTrue(results.IsSuccessStatusCode);

            var moves = await results.Content.ReadAsAsync<List<MoveDto>>();

            Assert.That(moves != null);
            Assert.That(moves.Count > 0);
            Assert.That(moves.All(m => m.Name.Equals(name)));
        }

        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public async Task ShouldGetMove()
        {
            var result = await LoggedInBasicClient.GetAsync(Baseuri + MovesRoute + "/" + _moves[0].Id);
            Assert.IsTrue(result.IsSuccessStatusCode);

            var move = result.Content.ReadAsAsync<Move>().Result;
            Assert.That(move != null);
            Assert.That(move.Name.Length > 0);
        }

        //[Test]
        //public async Task ShouldNotGetAllMovesDueToNoAuth()
        //{
        //    var result = await AnonymousClient.GetAsync(Baseuri + MovesRoute);
        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        //}
    }
}

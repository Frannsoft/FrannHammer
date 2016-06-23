﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Smoke
{
    [TestFixture]
    public class SmashAttributeTypeSmokeTest : BaseSmokeTest
    {
        private List<SmashAttributeType> _attributeTypes;

        [SetUp]
        [Ignore("Still working to setup Owin self hosting")]
        public override void SetUp()
        {
            base.SetUp();
            _attributeTypes = LoggedInBasicClient.GetAsync(Baseuri + SmashAttributeTypeRoute).Result.Content.ReadAsAsync<List<SmashAttributeType>>().Result;
        }

        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public void ShouldGetAllSmashAttributeTypes()
        {
            CollectionAssert.AllItemsAreNotNull(_attributeTypes);
            CollectionAssert.AllItemsAreUnique(_attributeTypes);
        }

        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public async Task ShouldGetSmashAttributeType()
        {
            var result = await LoggedInBasicClient.GetAsync(Baseuri + SmashAttributeTypeRoute + "/" + _attributeTypes[0].Id);

            Assert.IsTrue(result.IsSuccessStatusCode);

            var attributeType = result.Content.ReadAsAsync<SmashAttributeType>().Result;
            Assert.That(attributeType != null);
            Assert.That(attributeType.Name.Length > 0);
        }

        //[Test]
        //public async Task ShouldNotGetAllSmashAttributeTypesDueToNoAuth()
        //{
        //    var result = await AnonymousClient.GetAsync(Baseuri + SmashAttributeTypeRoute);
        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        //}
    }
}

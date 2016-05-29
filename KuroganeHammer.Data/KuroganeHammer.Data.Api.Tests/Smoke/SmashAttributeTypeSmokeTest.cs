using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Smoke
{
    [TestFixture]
    public class SmashAttributeTypeSmokeTest : BaseSmokeTest
    {
        private List<SmashAttributeType> _attributeTypes;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _attributeTypes = LoggedInBasicClient.GetAsync(Baseuri + SmashAttributeTypeRoute).Result.Content.ReadAsAsync<List<SmashAttributeType>>().Result;
        }

        [Test]
        public void ShouldGetAllSmashAttributeTypes()
        {
            CollectionAssert.AllItemsAreNotNull(_attributeTypes);
            CollectionAssert.AllItemsAreUnique(_attributeTypes);
        }

        [Test]
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

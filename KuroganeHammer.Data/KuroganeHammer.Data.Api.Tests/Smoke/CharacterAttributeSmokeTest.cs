using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Smoke
{
    [TestFixture]
    public class CharacterAttributeSmokeTest : BaseSmokeTest
    {
        private List<CharacterAttribute> _attributes;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _attributes = LoggedInBasicClient.GetAsync(Baseuri + CharacterAttributeRoute).Result.Content.ReadAsAsync<List<CharacterAttribute>>().Result;
        }

        [Test]
        public void ShouldGetAllSmashAttributeTypes()
        {
            CollectionAssert.AllItemsAreNotNull(_attributes);
            CollectionAssert.AllItemsAreUnique(_attributes);
        }

        [Test]
        public async Task ShouldGetSmashAttributeType()
        {
            var result = await LoggedInBasicClient.GetAsync(Baseuri + CharacterAttributeRoute + "/" + _attributes[0].Id);

            Assert.IsTrue(result.IsSuccessStatusCode);

            var attributeType = result.Content.ReadAsAsync<CharacterAttribute>().Result;
            Assert.That(attributeType != null);
            Assert.That(attributeType.Name.Length > 0);
        }

        [Test]
        public async Task ShouldNotGetAllSmashAttributeTypesDueToNoAuth()
        {
            var result = await AnonymousClient.GetAsync(Baseuri + CharacterAttributeRoute);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}

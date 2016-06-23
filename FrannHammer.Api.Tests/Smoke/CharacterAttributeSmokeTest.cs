using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Api.Tests.Asserts;
using FrannHammer.Core.DTOs;
using FrannHammer.Core.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Smoke
{
    [TestFixture]
    public class CharacterAttributeSmokeTest : BaseSmokeTest
    {
        private List<CharacterAttribute> _attributes;

        [SetUp]
        [Ignore("Still working to setup Owin self hosting")]
        public override void SetUp()
        {
            base.SetUp();
            _attributes = LoggedInBasicClient.GetAsync(Baseuri + CharacterAttributeRoute).Result.Content.ReadAsAsync<List<CharacterAttribute>>().Result;
        }

        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public void ShouldGetAllSmashAttributeTypes()
        {
            CollectionAssert.AllItemsAreNotNull(_attributes);
            CollectionAssert.AllItemsAreUnique(_attributes);
            CollectionPropertyAssert.AllPropertiesNotNullInCollection(_attributes, "CharacterAttributeType", "SmashAttributeType");
        }

        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public async Task ShouldGetCharacterAttribute()
        {
            var result = await LoggedInBasicClient.GetAsync(Baseuri + CharacterAttributeRoute + "/" + _attributes[0].Id);

            Assert.IsTrue(result.IsSuccessStatusCode);

            var attributeType = result.Content.ReadAsAsync<CharacterAttribute>().Result;
            Assert.That(attributeType != null);
            Assert.That(attributeType.Name.Length > 0);
        }

        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public async Task ShouldGetAllCharacterAttributesofSmashAttributeType()
        {
            var attributeTypes =
                LoggedInBasicClient.GetAsync(Baseuri + SmashAttributeTypeRoute)
                    .Result.Content.ReadAsAsync<List<SmashAttributeType>>()
                    .Result;

            var result =
                await
                    LoggedInBasicClient.GetAsync(Baseuri + SmashAttributeTypeRoute + "/" + attributeTypes[3].Id + "/" +
                                                 "characterattributes");

            Assert.IsTrue(result.IsSuccessStatusCode);

            var json = await result.Content.ReadAsStringAsync();
            var characterAttributeRows = JsonConvert.DeserializeObject<List<CharacterAttributeRowDto>>(json);
            //var characterAttributeRows = await result.Content.ReadAsAsync<List<CharacterAttributeRowDto>>();

            CollectionAssert.AllItemsAreNotNull(characterAttributeRows);
            CollectionAssert.AllItemsAreUnique(characterAttributeRows);
        }

        //[Test]
        //public async Task ShouldNotGetAllCharacterAttributesDueToNoAuth()
        //{
        //    var result = await AnonymousClient.GetAsync(Baseuri + CharacterAttributeRoute);
        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        //}
    }
}

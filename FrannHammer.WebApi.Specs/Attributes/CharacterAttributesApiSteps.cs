using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs.Attributes
{
    [Binding]
    public class CharacterAttributesApiSteps : BaseSteps
    {
        private static void AssertCharacterAttributeRowIsValid(ICharacterAttributeRow characterAttributeRow)
        {
            Assert.That(characterAttributeRow, Is.Not.Null);
            Assert.That(characterAttributeRow.Id, Is.Not.Null);
            Assert.That(characterAttributeRow.Name, Is.Not.Null);
            Assert.That(characterAttributeRow.CharacterName, Is.Not.Null);

            var attributeValues = characterAttributeRow.Values.ToList();
            attributeValues.ForEach(value =>
            {
                Assert.That(value.Id, Is.Not.Null, $"{nameof(IAttribute.Id)} is null.");
                Assert.That(value.Id, Is.Not.Null, $"{nameof(IAttribute.Name)} is null.");
                Assert.That(value.Id, Is.Not.Null, $"{nameof(IAttribute.Owner)} is null.");
                Assert.That(value.Id, Is.Not.Null, $"{nameof(IAttribute.Value)} is null.");
            });
        }

        [When(@"I request all character attribute rows")]
        public void WhenIRequestAllCharacterAttributeRows()
        {
            var requestResult = TestServer.HttpClient.GetAsync("/api/characterattributes").Result;
            ScenarioContext.Current[RequestResultKey] = requestResult;
        }

        [When(@"I request one specific character attribute row by id (.*)")]
        public void WhenIRequestOneSpecificCharacterAttributeRowById(string id)
        {
            var requestResult = TestServer.HttpClient.GetAsync("/api/characterattributes/" + id).Result;
            ScenarioContext.Current.Set(requestResult, RequestResultKey);
        }

        [Then(@"The result should be a list of all character attribute row entries")]
        public void ThenTheResultShouldBeAListOfAllCharacterAttributeRowEntries()
        {
            var requestResult = ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey);
            var characterMetadata = requestResult.Content.ReadAsAsync<IEnumerable<DefaultCharacterAttributeRow>>().Result.ToList();

            CollectionAssert.AllItemsAreNotNull(characterMetadata);
            CollectionAssert.AllItemsAreUnique(characterMetadata);
            Assert.That(characterMetadata.Count, Is.EqualTo(58));
            characterMetadata.ForEach(AssertCharacterAttributeRowIsValid);
        }

        [Then(@"The result should be just that character attribute row")]
        public void ThenTheResultShouldBeJustThatCharacterAttributeRow()
        {
            var requestResult = ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey);
            var characterMetadata = requestResult.Content.ReadAsAsync<DefaultCharacterAttributeRow>().Result;
            AssertCharacterAttributeRowIsValid(characterMetadata);
        }
    }
}

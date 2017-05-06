using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs.Attributes
{
    [Binding]
    [Scope(Feature = "CharacterAttributesApi")]
    public class CharacterAttributesApiSteps : BaseSteps
    {
        private static void AssertCharacterAttributeRowIsValid(ICharacterAttributeRow characterAttributeRow)
        {
            Assert.That(characterAttributeRow, Is.Not.Null, $"{nameof(characterAttributeRow)}");
            Assert.That(characterAttributeRow.Id, Is.Not.Null, $"{nameof(characterAttributeRow.Id)}");
            Assert.That(characterAttributeRow.Name, Is.Not.Null, $"{nameof(characterAttributeRow.Name)}");
            Assert.That(characterAttributeRow.CharacterName, Is.Not.Null, $"{nameof(characterAttributeRow.CharacterName)}");

            var attributeValues = characterAttributeRow.Values.ToList();
            attributeValues.ForEach(value =>
            {
                Assert.That(value.Id, Is.Not.Null, $"{nameof(IAttribute.Id)}");
                Assert.That(value.Name, Is.Not.Null, $"{nameof(IAttribute.Name)}");
                Assert.That(value.Owner, Is.Not.Null, $"{nameof(IAttribute.Owner)}");
                Assert.That(value.Value, Is.Not.Null, $"{nameof(IAttribute.Value)}");
            });
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            CreateTestServer();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            DisposeOfTestServer();
        }

        [Then(@"The result should be a list of all character attribute row entries")]
        public void ThenTheResultShouldBeAListOfAllCharacterAttributeRowEntries()
        {
            var characterMetadata = ApiClient
                .DeserializeResponse<IEnumerable<DefaultCharacterAttributeRow>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey).Content)
                .ToList();

            CollectionAssert.AllItemsAreNotNull(characterMetadata);
            CollectionAssert.AllItemsAreUnique(characterMetadata);
            Assert.That(characterMetadata.Count, Is.EqualTo(58));
            characterMetadata.ForEach(AssertCharacterAttributeRowIsValid);
        }

        [Then(@"The result should be just that character attribute row")]
        public void ThenTheResultShouldBeJustThatCharacterAttributeRow()
        {
            var characterMetadata = ApiClient
                .DeserializeResponse<DefaultCharacterAttributeRow>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey).Content);

            AssertCharacterAttributeRowIsValid(characterMetadata);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.WebApi.Models;
using NUnit.Framework;
using TechTalk.SpecFlow;
using static FrannHammer.WebApi.Specs.ResourceAsserts;

namespace FrannHammer.WebApi.Specs.Attributes
{
    [Binding]
    [Scope(Feature = "CharacterAttributesApi")]
    public class CharacterAttributesApiSteps : BaseSteps
    {
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
            var characterAttributeRows = ApiClient
                .DeserializeResponse<IEnumerable<CharacterAttributeRowResource>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            CollectionAssert.AllItemsAreNotNull(characterAttributeRows);
            CollectionAssert.AllItemsAreUnique(characterAttributeRows);
            characterAttributeRows.ForEach(AssertCharacterAttributeRowIsValid);
        }

        [Then(@"The result should be just that character attribute row")]
        public void ThenTheResultShouldBeJustThatCharacterAttributeRow()
        {
            var characterMetadata = ApiClient
                .DeserializeResponse<CharacterAttributeRowResource>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey));

            AssertCharacterAttributeRowIsValid(characterMetadata);
        }

        [Then(@"The result should be a list of all character attribute types")]
        public void ThenTheResultShouldBeAListOfAllCharacterAttributeTypes()
        {
            var typesCollection = ApiClient
                .DeserializeResponse<IEnumerable<CharacterAttributeNameResource>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            typesCollection.ForEach(AssertCharacterAttributeIsValid);
        }
    }
}

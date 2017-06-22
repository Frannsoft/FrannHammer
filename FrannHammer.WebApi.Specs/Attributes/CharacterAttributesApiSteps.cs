using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Models;
using NUnit.Framework;
using TechTalk.SpecFlow;
using static FrannHammer.WebApi.Specs.ExpectedLinkRelConstants;

namespace FrannHammer.WebApi.Specs.Attributes
{
    [Binding]
    [Scope(Feature = "CharacterAttributesApi")]
    public class CharacterAttributesApiSteps : BaseSteps
    {
        private static void AssertCharacterAttributeRowIsValid(CharacterAttributeRowResource characterAttributeRow)
        {
            Assert.That(characterAttributeRow, Is.Not.Null, $"{nameof(characterAttributeRow)}");
            Assert.That(characterAttributeRow.InstanceId, Is.Not.Null, $"{nameof(characterAttributeRow.InstanceId)}");
            Assert.That(characterAttributeRow.Name, Is.Not.Null, $"{nameof(characterAttributeRow.Name)}");
            Assert.That(characterAttributeRow.Owner, Is.Not.Null, $"{nameof(characterAttributeRow.Owner)}");
            Assert.That(characterAttributeRow.Links.Any(l => l.Rel.Equals(CharacterLinkName, StringComparison.OrdinalIgnoreCase)), 
                $"Unable to find '{CharacterLinkName}' link.");

            var attributeValues = characterAttributeRow.Values.ToList();
            attributeValues.ForEach(value =>
            {
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
    }
}

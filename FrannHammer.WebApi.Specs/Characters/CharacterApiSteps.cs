using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.Domain.PropertyParsers;
using FrannHammer.WebApi.Models;
using FrannHammer.WebScraping;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs.Characters
{
    [Binding]
    [Scope(Feature = "CharactersApi")]
    public class CharacterApiSteps : BaseSteps
    {
        private static void AssertCharacterIsValid(CharacterResource characterResource)
        {
            Assert.That(characterResource, Is.Not.Null, $"{nameof(characterResource)}");
            Assert.That(characterResource.ThumbnailUrl, Is.Not.Null, $"{nameof(characterResource.ThumbnailUrl)}");
            Assert.That(characterResource.DisplayName, Is.Not.Null, $"{nameof(characterResource.DisplayName)}");
            Assert.That(characterResource.ColorTheme, Is.Not.Null, $"{nameof(characterResource.ColorTheme)}");
            Assert.That(characterResource.FullUrl, Is.Not.Null, $"{nameof(characterResource.FullUrl)}");
            Assert.That(characterResource.InstanceId, Is.Not.Null, $"{nameof(characterResource.InstanceId)}");
            Assert.That(characterResource.MainImageUrl, Is.Not.Null, $"{nameof(characterResource.MainImageUrl)}");
            Assert.That(characterResource.Name, Is.Not.Null, $"{nameof(characterResource.Name)}");
            AssertHalLinksArePresent(characterResource);
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

        [Then(@"the result should be a list of all character metadata")]
        public void ThenTheResultShouldBeAListOfAllCharacterMetadata()
        {
            var characterMetadata = ApiClient
                .DeserializeResponse<IEnumerable<CharacterResource>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            CollectionAssert.AllItemsAreNotNull(characterMetadata);
            CollectionAssert.AllItemsAreUnique(characterMetadata);
            Assert.That(characterMetadata.Count, Is.EqualTo(58));
            characterMetadata.ForEach(AssertCharacterIsValid);
        }

        [Then(@"the result should be a list containing just that characters metadata")]
        public void ThenTheResultShouldBeAListContainingJustThatCharactersMetadata()
        {
            var characterMetadata = ApiClient
                .DeserializeResponse<IEnumerable<CharacterResource>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            Assert.That(characterMetadata.Count, Is.EqualTo(1));
            characterMetadata.ForEach(AssertCharacterIsValid);
        }

        [Then(@"the result should be just that characters metadata")]
        public void ThenTheResultShouldBeJustThatCharactersMetadata()
        {
            var characterMetadata = ApiClient
               .DeserializeResponse<CharacterResource>(
                   ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey));

            AssertCharacterIsValid(characterMetadata);
        }

        [Then(@"the result should be a list containing just that characters throw data")]
        public void ThenTheResultShouldBeJustThatCharactersThrowData()
        {
            var characterThrowData = ApiClient
                .DeserializeResponse<IEnumerable<Move>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            Assert.That(characterThrowData.Count, Is.GreaterThan(0), $"{nameof(characterThrowData.Count)}");
            characterThrowData.ForEach(charThrow =>
            {
                Assert.That(charThrow.OwnerId.ToString() == expectedOwnerName ||
                           charThrow.Owner == expectedOwnerName, $"{nameof(charThrow.OwnerId)}");
                Assert.That(charThrow.MoveType, Is.EqualTo(MoveType.Throw.GetEnumDescription()), $"{nameof(charThrow.MoveType)}");
            });
        }

        [Then(@"the result should be a list containing just that characters move data")]
        public void ThenTheResultShouldBeAListContainingJustThatCharactersMoveData()
        {
            var characterMoveData = ApiClient
                .DeserializeResponse<IEnumerable<Move>>(
                     ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            characterMoveData.ForEach(character =>
            {
                Assert.That(character.OwnerId.ToString() == expectedOwnerName ||
                            character.Owner == expectedOwnerName, $"{nameof(character.OwnerId)}");
            });
            characterMoveData.ForEach(AssertMoveIsValid);
        }

        [Then(@"the result should be a list containing just that characters movement data")]
        public void ThenTheResultShouldBeAListContainingJustThatCharactersMovementData()
        {
            var characterMovementData = ApiClient
                .DeserializeResponse<IEnumerable<Movement>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            characterMovementData.ForEach(movement =>
            {
                Assert.That(movement.OwnerId.ToString() == expectedOwnerName ||
                           movement.Owner == expectedOwnerName, $"{nameof(movement.OwnerId)}");
            });
            characterMovementData.ForEach(AssertMovementIsValid);
        }

        [Then(@"the result should be a list containing rows of attribute data for that character")]
        public void ThenTheResultShouldBeAListContainingRowsOfAttributeDataForThatCharacter()
        {
            var attributeRows = ApiClient.DeserializeResponse<IEnumerable<ICharacterAttributeRow>>(
              ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            attributeRows.ForEach(row =>
            {
                Assert.That(row.OwnerId.ToString() == expectedOwnerName ||
                          row.Owner == expectedOwnerName, $"{nameof(row.OwnerId)}");
                Assert.That(row.Values.Count(), Is.GreaterThan(0), $"{nameof(row.Values)}");

                row.Values.ToList().ForEach(rowValue =>
                {
                    Assert.That(rowValue.Name, Is.Not.Null, $"{nameof(rowValue.Name)}");
                    Assert.That(row.OwnerId.ToString() == expectedOwnerName ||
                          row.Owner == expectedOwnerName, $"{nameof(row.OwnerId)}");
                    Assert.That(rowValue.Value, Is.Not.Null, $"{nameof(rowValue.Value)}");
                });
            });
        }

        [Then(@"the result should be a list containing the metadata, movement data and attribute data for a specific character")]
        public void ThenTheResultShouldBeAListContainingTheMetadataMovementDataAndAttributeDataForASpecificCharacter()
        {
            var detailedData = ApiClient.DeserializeResponse<ICharacterDetailsDto>(
                ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey));

            var movementData = detailedData.Movements;
            var metadata = detailedData.Metadata;
            var attributeData = detailedData.AttributeRows;

            metadata.AssertIsValid();
            movementData.AssertIsValid();
            attributeData.AssertIsValid();
        }

        [Then(@"the result should be a list containing the parsed out move data for that character")]
        public void ThenTheResultShouldBeAListContainingTheParsedOutMoveDataForThatCharacter()
        {
            var detailedMoveData = ApiClient
                .DeserializeResponse<IEnumerable<ParsedMove>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                    .ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);


            Assert.That(detailedMoveData.Count, Is.GreaterThan(0), $"{nameof(detailedMoveData.Count)}");

            var expectedMoveProperties =
                typeof(MoveDataNameConstants).GetFields(BindingFlags.Public | BindingFlags.Instance);

            detailedMoveData.ForEach(moveData =>
            {
                Assert.That(moveData.OwnerId.ToString() == expectedOwnerName ||
                         moveData.Owner == expectedOwnerName, $"{nameof(moveData.OwnerId)}");

                foreach (var moveDataProperties in moveData.MoveData)
                {
                    foreach (var field in expectedMoveProperties)
                    {
                        Assert.That(moveDataProperties[field.Name] != null,
                            $"{nameof(moveData)} does not have property of name {nameof(field.Name)}");
                    }
                }
            });
        }

        [Then(@"the result should be a list containing just that characters gravity movement data")]
        public void ThenTheResultShouldBeAListContainingJustThatCharactersGravityMovementData()
        {
            var characterMovementData = ApiClient
               .DeserializeResponse<IEnumerable<Movement>>(
                   ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            characterMovementData.ForEach(movement =>
            {
                Assert.That(movement.Name.ToLower(), Is.EqualTo("gravity"), $"{nameof(movement.Name)}");
                Assert.That(movement.OwnerId.ToString() == expectedOwnerName ||
                           movement.Owner == expectedOwnerName, $"{nameof(movement.OwnerId)}");

                AssertMovementIsValid(movement);
            });
        }
    }
}

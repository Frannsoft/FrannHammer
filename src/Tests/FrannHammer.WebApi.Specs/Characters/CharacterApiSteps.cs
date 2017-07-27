using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Domain.PropertyParsers;
using FrannHammer.WebApi.Models;
using FrannHammer.WebScraping;
using NUnit.Framework;
using TechTalk.SpecFlow;
using static FrannHammer.WebApi.Specs.ResourceAsserts;

namespace FrannHammer.WebApi.Specs.Characters
{
    [Binding]
    [Scope(Feature = "CharactersApi")]
    public class CharacterApiSteps : BaseSteps
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
                .DeserializeResponse<IEnumerable<MoveResource>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            Assert.That(characterThrowData.Count, Is.GreaterThan(0), $"{nameof(characterThrowData.Count)}");
            characterThrowData.Where(t => t.Name.EndsWith(MoveType.Throw.GetEnumDescription(), StringComparison.OrdinalIgnoreCase)).ToList()
               .ForEach(charThrow =>
            {
                Assert.That(charThrow.OwnerId.ToString() == expectedOwnerName ||
                           charThrow.Owner == expectedOwnerName, $"{nameof(charThrow.OwnerId)}");
                Assert.That(charThrow.MoveType, Is.EqualTo(MoveType.Throw.GetEnumDescription()), $"{nameof(charThrow.MoveType)}");

                AssertThrowMoveIsValid(charThrow);
            });

            characterThrowData.Where(t => t.Name.EndsWith("grab", StringComparison.OrdinalIgnoreCase)).ToList()
             .ForEach(charThrow =>
             {
                 Assert.That(charThrow.OwnerId.ToString() == expectedOwnerName ||
                              charThrow.Owner == expectedOwnerName, $"{nameof(charThrow.OwnerId)}");
                 Assert.That(charThrow.MoveType, Is.EqualTo(MoveType.Throw.GetEnumDescription()), $"{nameof(charThrow.MoveType)}");

                 AssertGrabMoveIsValid(charThrow);
             });
        }

        [Then(@"the result should be a list containing just that characters move data")]
        public void ThenTheResultShouldBeAListContainingJustThatCharactersMoveData()
        {
            var characterMoveData = ApiClient
                .DeserializeResponse<IEnumerable<MoveResource>>(
                     ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            characterMoveData.ForEach(character =>
            {
                Assert.That(character.OwnerId.ToString() == expectedOwnerName ||
                            character.Owner == expectedOwnerName, $"{nameof(character.OwnerId)}");
            });
            characterMoveData.Where(m => m.MoveType == MoveType.Ground.ToString()).ToList().ForEach(AssertGroundMoveIsValid);
            characterMoveData.Where(m => m.MoveType == MoveType.Aerial.ToString()).ToList().ForEach(AssertAerialMoveIsValid);
            characterMoveData.Where(m => m.MoveType == MoveType.Special.ToString()).ToList().ForEach(AssertSpecialMoveIsValid);
        }

        [Then(@"the result should be a list containing just that characters movement data")]
        public void ThenTheResultShouldBeAListContainingJustThatCharactersMovementData()
        {
            var characterMovementData = ApiClient
                .DeserializeResponse<IEnumerable<MovementResource>>(
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

            Assert.That(attributeRows.Count, Is.GreaterThan(0), $"{nameof(attributeRows)}");
            attributeRows.ForEach(row =>
            {
                Assert.That(row.OwnerId.ToString() == expectedOwnerName ||
                          row.Owner == expectedOwnerName, $"{nameof(row.OwnerId)}");
                Assert.That(row.Values.Count(), Is.GreaterThan(0), $"{nameof(row.Values)}");

                row.Values.ToList().ForEach(rowValue =>
                {
                    Assert.That(rowValue.Name, Is.Not.Null, $"{nameof(rowValue.Name)}");
                    Assert.That(rowValue.OwnerId.ToString() == expectedOwnerName ||
                                rowValue.Owner == expectedOwnerName, $"{nameof(rowValue.OwnerId)}");
                    Assert.That(rowValue.Value, Is.Not.Null, $"{nameof(rowValue.Value)}");
                });
            });
        }

        [Then(@"the result should be just that characters attributes of that type")]
        public void ThenTheResultShouldBeJustThatCharactersAttributesOfThatType()
        {
            var attributeRows = ApiClient.DeserializeResponse<IEnumerable<ICharacterAttributeRow>>(
                ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            string expectedAttributeName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            attributeRows.ForEach(attributeRow =>
            {
                Assert.That(attributeRow, Is.Not.Null, $"{nameof(attributeRow)}");
                Assert.That(attributeRow.Name.ToLower(), Is.EqualTo(expectedAttributeName));
                Assert.That(attributeRow.Values.Count(), Is.GreaterThan(0), $"{nameof(attributeRow.Values)}");

                attributeRow.Values.ToList().ForEach(rowValue =>
                {
                    Assert.That(rowValue.Name, Is.Not.Null, $"{nameof(rowValue.Name)}");
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

                foreach (var moveDataProperties in moveData.MoveProperties)
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
               .DeserializeResponse<IEnumerable<MovementResource>>(
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

        [Then(@"the result should be a dictionary containing unique data for that character")]
        public void ThenTheResultShouldBeADictionaryContainingUniqueDataForThatCharacter()
        {
            var characterUniqueData = ApiClient
                .DeserializeResponse<IEnumerable<UniqueDataResource>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwner = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            Assert.That(characterUniqueData.Count, Is.GreaterThan(0), $"{nameof(characterUniqueData.Count)}");

            characterUniqueData.ForEach(data =>
            {
                Assert.That(data.OwnerId.ToString() == expectedOwner ||
                          data.Owner == expectedOwner, $"{nameof(IUniqueData.OwnerId)}");
                Assert.That(data.Value, Is.Not.Empty, $"{data.Name}");
                AssertUniqueDataIsValid(data);
            });
        }

        
    }
}

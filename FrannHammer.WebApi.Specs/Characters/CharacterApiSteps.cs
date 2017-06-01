using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.Domain.PropertyParsers;
using FrannHammer.WebScraping;
using FrannHammer.WebScraping.Domain.Contracts;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs.Characters
{
    [Binding]
    [Scope(Feature = "CharactersApi")]
    public class CharacterApiSteps : BaseSteps
    {
        private static void AssertCharacterIsValid(ICharacter character)
        {
            Assert.That(character, Is.Not.Null, $"{nameof(character)}");
            Assert.That(character.ThumbnailUrl, Is.Not.Null, $"{nameof(character.ThumbnailUrl)}");
            Assert.That(character.DisplayName, Is.Not.Null, $"{nameof(character.DisplayName)}");
            Assert.That(character.ColorTheme, Is.Not.Null, $"{nameof(character.ColorTheme)}");
            Assert.That(character.FullUrl, Is.Not.Null, $"{nameof(character.FullUrl)}");
            Assert.That(character.InstanceId, Is.Not.Null, $"{nameof(character.InstanceId)}");
            Assert.That(character.MainImageUrl, Is.Not.Null, $"{nameof(character.MainImageUrl)}");
            Assert.That(character.Name, Is.Not.Null, $"{nameof(character.Name)}");
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
                .DeserializeResponse<IEnumerable<Character>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
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
                .DeserializeResponse<IEnumerable<Character>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            Assert.That(characterMetadata.Count, Is.EqualTo(1));
            characterMetadata.ForEach(AssertCharacterIsValid);
        }

        [Then(@"the result should be just that characters metadata")]
        public void ThenTheResultShouldBeJustThatCharactersMetadata()
        {
            var characterMetadata = ApiClient
               .DeserializeResponse<Character>(
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
                Assert.That(charThrow.Owner, Is.EqualTo(expectedOwnerName), $"{nameof(charThrow.Owner)}");
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
                Assert.That(character.Owner, Is.EqualTo(expectedOwnerName), $"{nameof(character.Owner)}");
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

            characterMovementData.ForEach(character =>
            {
                Assert.That(character.Owner, Is.EqualTo(expectedOwnerName), $"{nameof(character.Owner)}");
            });
            characterMovementData.ForEach(AssertMovementIsValid);
        }

        [Then(@"the result should be a list containing rows of attribute data for that character")]
        public void ThenTheResultShouldBeAListContainingRowsOfAttributeDataForThatCharacter()
        {
            var attributeRows = ApiClient.DeserializeResponse<IEnumerable<ICharacterAttributeRow>>(
              ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey)).ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);

            int expectedOwnerId = CharacterIds.FindByName(expectedOwnerName);

            attributeRows.ForEach(row =>
            {
                Assert.That(row.Owner, Is.EqualTo(expectedOwnerName), $"{nameof(row.Owner)}");
                Assert.That(row.OwnerId, Is.EqualTo(expectedOwnerId), $"{nameof(row.OwnerId)}");
                Assert.That(row.Values.Count(), Is.GreaterThan(0), $"{nameof(row.Values)}");

                row.Values.ToList().ForEach(rowValue =>
                {
                    Assert.That(rowValue.Name, Is.Not.Null, $"{nameof(rowValue.Name)}");
                    Assert.That(rowValue.Owner, Is.EqualTo(expectedOwnerName), $"{nameof(rowValue.Owner)}");
                    Assert.That(rowValue.OwnerId, Is.EqualTo(expectedOwnerId), $"{nameof(rowValue.OwnerId)}");
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
                .DeserializeResponse<IEnumerable<IDictionary<string, IDictionary<string, string>>>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                    .ToList();

            string expectedOwnerName = ScenarioContext.Current.Get<string>(RouteTemplateValueToReplaceKey);
            int expectedOwnerId = CharacterIds.FindByName(expectedOwnerName);

            Assert.That(detailedMoveData.Count, Is.GreaterThan(0), $"{nameof(detailedMoveData.Count)}");

            var expectedMoveProperties =
                typeof(MoveDataNameConstants).GetFields(BindingFlags.Public | BindingFlags.Instance);

            detailedMoveData.ForEach(moveData =>
            {
                foreach (var moveDataProperties in moveData.Values)
                {
                    Assert.That(moveDataProperties[MoveDataNameConstants.OwnerKey], Is.EqualTo(expectedOwnerName),
                        $"{nameof(MoveDataNameConstants.OwnerKey)}");
                    Assert.That(moveDataProperties[MoveDataNameConstants.OwnerIdKey], Is.EqualTo(expectedOwnerId.ToString()),
                        $"{nameof(MoveDataNameConstants.OwnerIdKey)}");

                    foreach (var field in expectedMoveProperties)
                    {
                        Assert.That(moveDataProperties.ContainsKey(field.Name),
                            $"{nameof(moveData)} does not have key {nameof(field.Name)}");
                    }
                }
            });
        }
    }
}

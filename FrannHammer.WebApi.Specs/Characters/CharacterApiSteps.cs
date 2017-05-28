using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping;
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
            Assert.That(character.Id, Is.Not.Null, $"{nameof(character.Id)}");
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
    }

    public static class CharacterDetailsDtoAssertions
    {
        public static void AssertIsValid(this ICharacter character)
        {
            Assert.That(character.Name, Is.Not.Empty, $"{nameof(character.Name)}");
        }

        public static void AssertIsValid(this IEnumerable<IMovement> movements)
        {
            movements.ToList().ForEach(movement =>
            {
                Assert.That(movement.Owner, Is.Not.Empty, $"{nameof(movement.Owner)}");
            });
        }

        public static void AssertIsValid(this IEnumerable<ICharacterAttributeRow> attributeRows)
        {
            attributeRows.ToList().ForEach(row =>
            {
                Assert.That(row.Owner, Is.Not.Empty, $"{nameof(row.Owner)}");
            });
        }
    }
}

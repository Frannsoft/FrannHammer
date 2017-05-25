using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
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

            characterThrowData.ForEach(charThrow =>
            {
                Assert.That(charThrow.MoveType, Is.EqualTo("throw"));
            });
        }
    }
}

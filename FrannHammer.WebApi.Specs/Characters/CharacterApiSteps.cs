using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs.Characters
{
    [Binding]
    public class CharacterApiSteps : BaseSteps
    {
        private static void AssertCharacterIsValid(ICharacter character)
        {
            Assert.That(character, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Null);
            Assert.That(character.DisplayName, Is.Not.Null);
            Assert.That(character.ColorTheme, Is.Not.Null);
            //Assert.That(character.Description, Is.Not.Null);
            Assert.That(character.DisplayName, Is.Not.Null);
            Assert.That(character.FullUrl, Is.Not.Null);
            Assert.That(character.Id, Is.Not.Null);
            Assert.That(character.MainImageUrl, Is.Not.Null);
            Assert.That(character.Name, Is.Not.Null);
            //Assert.That(character.Style, Is.Not.Null);
            Assert.That(character.ThumbnailUrl, Is.Not.Null);
        }

        [When(@"I request all character metadata")]
        public void WhenIRequestAllCharacterMetadata()
        {
            var requestResult = TestServer.HttpClient.GetAsync("/api/characters").Result;
            ScenarioContext.Current[RequestResultKey] = requestResult;
        }

        [When(@"I request one specific character's metadata by id (.*)")]
        public void WhenIRequestOneSpecificCharacterSMetadata(string id)
        {
            var requestResult = TestServer.HttpClient.GetAsync("/api/characters/" + id).Result;
             ScenarioContext.Current.Set(requestResult, RequestResultKey);
        }

        [Then(@"the result should be a list of all character metadata")]
        public void ThenTheResultShouldBeAListOfAllCharacterMetadata()
        {
            var requestResult = ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey);
            var characterMetadata = requestResult.Content.ReadAsAsync<IEnumerable<Character>>().Result.ToList();

            CollectionAssert.AllItemsAreNotNull(characterMetadata);
            CollectionAssert.AllItemsAreUnique(characterMetadata);
            Assert.That(characterMetadata.Count, Is.EqualTo(58));
            characterMetadata.ForEach(AssertCharacterIsValid);
        }

        [Then(@"the result should be just that character metadata")]
        public void ThenTheResultShouldBeJustThatCharacterMetadata()
        {
            var requestResult = ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey);
            var characterMetadata = requestResult.Content.ReadAsAsync<Character>().Result;
            AssertCharacterIsValid(characterMetadata);
        }
    }
}

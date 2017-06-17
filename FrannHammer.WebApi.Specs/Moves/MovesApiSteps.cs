using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.WebApi.Models;
using NUnit.Framework;
using TechTalk.SpecFlow;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;


namespace FrannHammer.WebApi.Specs.Moves
{
    [Binding]
    [Scope(Feature = "MovesApi")]
    public class MovesApiSteps : BaseSteps
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

        [Then(@"The result should be a list of all move data")]
        public void ThenTheResultShouldBeAListOfAllMoveData()
        {
            var moveMetadata = ApiClient
                .DeserializeResponse<IEnumerable<MoveResource>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            CollectionAssert.AllItemsAreNotNull(moveMetadata);
            CollectionAssert.AllItemsAreUnique(moveMetadata);
            moveMetadata.ForEach(AssertMoveIsValid);
        }

        [Then(@"The result should be just that moves data")]
        public void ThenTheResultShouldBeJustThatMovesData()
        {
            var move = ApiClient
                .DeserializeResponse<MoveResource>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey));
            AssertMoveIsValid(move);
        }

        [Then(@"The result should be all moves that match that name")]
        public void ThenTheResultShouldBeAllMovesThatMatchThatName()
        {
            var moves =
                ApiClient.DeserializeResponse<IEnumerable<MoveResource>>(
                        ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                    .ToList();

            CollectionAssert.AllItemsAreNotNull(moves);
            CollectionAssert.AllItemsAreUnique(moves);
            moves.ForEach(AssertMoveIsValid);

            string routeParameter = ScenarioContext.Current.Get<string>(RouteParameter);

            moves.ForEach(move =>
            {
                Assert.That(move.Name, Contains.Substring(routeParameter), $"{move.Name} does not contain {routeParameter}");
            });
        }

        [When(@"I request all of the (.*) property data for a move by (.*)")]
        public void WhenIRequestAllOfTheBaseDamagesPropertyDataForAMoveByName(string property, string name)
        {
            string requestUrl = ScenarioContext.Current.Get<string>(RouteUrlKey).Replace("{name}", name).Replace("{property}", property);
            var requestResult = ApiClient.GetResult(requestUrl);
            ScenarioContext.Current.Set(requestResult, RequestResultKey);
        }

        [Then(@"The result should be a list of (.*) for the specific property in the moves that match that name")]
        public void ThenTheResultShouldBeAListOfBaseDamagesForMovesThatMatchThatName(string expectedResultProperties)
        {
            var results = ApiClient.DeserializeResponse<IEnumerable<Dictionary<string, string>>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            results.ForEach(propertyData =>
            {
                Assert.That(propertyData[MoveNameKey], Is.Not.Empty, $"{propertyData[MoveNameKey]} should not be empty.");

                foreach (string propertyKey in expectedResultProperties.Split(';'))
                {
                    Assert.That(propertyData.Any(kvp => kvp.Key.Equals(propertyKey, StringComparison.CurrentCultureIgnoreCase)), $"{nameof(propertyData)}.{propertyKey} was not present.");
                }
            });
        }
    }
}

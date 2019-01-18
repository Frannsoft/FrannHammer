using FrannHammer.NetCore.WebApi.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechTalk.SpecFlow;
using static FrannHammer.NetCore.WebApi.Specs.ResourceAsserts;

namespace FrannHammer.NetCore.WebApi.Specs.Movements
{
    [Binding]
    //[Scope(Feature = "MovementsApi")]
    public class MovementsApiSteps : BaseSteps
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

        [Then(@"The result should be a list of all character movement data")]
        public void ThenTheResultShouldBeAListOfAllCharacterMovementData()
        {
            var movements = ApiClient
                .DeserializeResponse<IEnumerable<MovementResource>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.AllItemsAreUnique(movements);
            movements.ForEach(AssertMovementIsValid);
        }

        [Then(@"The result should be just that movement data")]
        public void ThenTheResultShouldBeJustThatMovementData()
        {
            var movement = ApiClient
                .DeserializeResponse<MovementResource>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey));
            AssertMovementIsValid(movement);
        }
    }
}

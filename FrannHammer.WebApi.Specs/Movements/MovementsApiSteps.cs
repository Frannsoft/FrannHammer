﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs.Movements
{
    [Binding]
    [Scope(Feature = "MovementsApi")]
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
                .DeserializeResponse<IEnumerable<Movement>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            CollectionAssert.AllItemsAreNotNull(movements);
            CollectionAssert.AllItemsAreUnique(movements);
            movements.ForEach(AssertMovementIsValid);
        }

        [Then(@"The result should be just that movement data")]
        public void ThenTheResultShouldBeJustThatMovementData()
        {
            var movement = ApiClient
                .DeserializeResponse<Movement>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey));
            AssertMovementIsValid(movement);
        }
    }
}
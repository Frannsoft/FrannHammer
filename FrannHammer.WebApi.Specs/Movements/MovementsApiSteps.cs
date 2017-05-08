using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs.Movements
{
    [Binding]
    public class MovementsApiSteps : BaseSteps
    {
        private static void AssertMovementIsValid(IMovement movement)
        {
            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Name, Is.Not.Null);
            Assert.That(movement.OwnerId, Is.Not.Null);
            Assert.That(movement.Value, Is.Not.Null);
            Assert.That(movement.Id, Is.Not.Null);
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

        [Then(@"The result should be a list of all character movement data")]
        public void ThenTheResultShouldBeAListOfAllCharacterMovementData()
        {
            var movements = ApiClient
                .DeserializeResponse<IEnumerable<Movement>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey).Content)
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
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey).Content);
            AssertMovementIsValid(movement);
        }
    }
}

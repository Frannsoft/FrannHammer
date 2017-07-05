using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebApi.Models;
using NUnit.Framework;
using TechTalk.SpecFlow;
using static FrannHammer.WebApi.Specs.ResourceAsserts;


namespace FrannHammer.WebApi.Specs.Unique
{
    [Binding]
    [Scope(Feature = "UniqueApi")]
    public class UniqueApiSteps : BaseSteps
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

        [Then(@"the result should be a list of all unique data")]
        public void ThenTheResultShouldBeAListOfAllUniqueData()
        {
            var uniqueDataCollection = ApiClient
                .DeserializeResponse<IEnumerable<UniqueDataResource>>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            Assert.That(uniqueDataCollection.Count, Is.GreaterThan(0), $"{nameof(uniqueDataCollection.Count)}");

            uniqueDataCollection.ForEach(AssertUniqueDataIsValid);
        }
    }
}

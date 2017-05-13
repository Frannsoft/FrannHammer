using System.Linq;
using Microsoft.Owin.Testing;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs
{
    public abstract class BaseSteps
    {
        protected static TestServer TestServer { get; private set; }
        protected const string RequestResultKey = "requestResult";
        protected const string RouteParameter = "routeParameter";
        protected const string RouteUrlKey = "routeUrl";
        protected static ApiClient ApiClient { get; private set; }

        public static void CreateTestServer()
        {
            TestServer = TestServer.Create<Startup>();
            ApiClient = new ApiClient(TestServer.HttpClient);
        }

        public static void DisposeOfTestServer()
        {
            TestServer.Dispose();
        }

        [When(@"I request all data")]
        public void WhenIRequestAllData()
        {
            string requestUrl = ScenarioContext.Current.Get<string>(RouteUrlKey);
            var requestResult = ApiClient.GetResult(requestUrl);
            ScenarioContext.Current[RequestResultKey] = requestResult;
        }

        protected string InjectRouteParameterIntoRequestUrl(string requestUrl, string routeParameter)
        {
            string urlTail = requestUrl.Split('/').Last();
            return requestUrl.Replace(urlTail, routeParameter);
        }

        [Given(@"The api route of (.*)")]
        public void TheApiRouteOf(string routeUrl)
        {
            ScenarioContext.Current.Set(routeUrl, RouteUrlKey);
        }

        [When(@"I request one specific item by id (.*)")]
        public void WhenIRequestOneSpecificItemById(string id)
        {
            string requestUrl = InjectRouteParameterIntoRequestUrl(ScenarioContext.Current.Get<string>(RouteUrlKey), id);

            var requestResult = ApiClient.GetResult(requestUrl);
            ScenarioContext.Current.Set(requestResult, RequestResultKey);
        }

        [When(@"I request one specific item by name (.*)")]
        public void WhenIRequestOneSpecificItemByName(string name)
        {
            ScenarioContext.Current.Set(name, RouteParameter);
            string requestUrl = InjectRouteParameterIntoRequestUrl(ScenarioContext.Current.Get<string>(RouteUrlKey), name);

            var requestResult = ApiClient.GetResult(requestUrl);
            ScenarioContext.Current.Set(requestResult, RequestResultKey);
        }
    }
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Linq;
using TechTalk.SpecFlow;

namespace FrannHammer.NetCore.WebApi.Specs
{
    public abstract class BaseSteps
    {
        protected static TestServer TestServer { get; private set; }
        protected const string RequestResultKey = "requestResult";
        protected const string RouteTemplateValueToReplaceKey = "routeTemplateValueToReplace";
        protected const string RouteParameter = "routeParameter";
        protected const string RouteUrlKey = "routeUrl";
        protected static ApiClient ApiClient { get; private set; }

        public static void CreateTestServer()
        {
            TestServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            //TestServer = TestServer. .Create<Startup>();
            ApiClient = new ApiClient(TestServer.CreateClient());
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

        protected string InjectRouteParameterIntoRequestUrl(string requestUrl, string routeParameter, string routeTemplateValueToReplace)
        {
            ScenarioContext.Current.Set(routeParameter, RouteTemplateValueToReplaceKey);
            string urlPortionToReplace = requestUrl.Split('/').Single(s => s.Contains("{" + routeTemplateValueToReplace + "}"));
            return requestUrl.Replace(urlPortionToReplace, routeParameter);
        }

        [Given(@"The api route of (.*)")]
        public void TheApiRouteOf(string routeUrl)
        {
            ScenarioContext.Current.Set(routeUrl, RouteUrlKey);
        }

        [When(@"I request one specific item by (.*) (.*)")]
        public void WhenIRequestOneSpecificItemByName(string routeTemplateValueToReplace, string name)
        {
            ScenarioContext.Current.Set(name, RouteParameter);
            string requestUrl = InjectRouteParameterIntoRequestUrl(ScenarioContext.Current.Get<string>(RouteUrlKey), name, routeTemplateValueToReplace);

            var requestResult = ApiClient.GetResult(requestUrl);
            ScenarioContext.Current.Set(requestResult, RequestResultKey);
        }
    }
}

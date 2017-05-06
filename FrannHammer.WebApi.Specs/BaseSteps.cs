using Microsoft.Owin.Testing;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs
{
    [Binding]
    public abstract class BaseSteps
    {
        protected static TestServer TestServer { get; set; }
        protected const string RequestResultKey = "requestResult";

        [BeforeFeature]
        public static void CreateTestServer()
        {
            TestServer = TestServer.Create<Startup>();
        }

        [AfterFeature]
        public static void DisposeOfTestServer()
        {
            TestServer.Dispose();
        }
    }
}

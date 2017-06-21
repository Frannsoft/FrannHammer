using System.Linq;
using FrannHammer.WebApi.Models;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs
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

        //[When(@"I request one specific item by id (.*)")]
        //public void WhenIRequestOneSpecificItemById(string id)
        //{
        //    string requestUrl = InjectRouteParameterIntoRequestUrl(ScenarioContext.Current.Get<string>(RouteUrlKey), id, "id");

        //    var requestResult = ApiClient.GetResult(requestUrl);
        //    ScenarioContext.Current.Set(requestResult, RequestResultKey);
        //}

        [When(@"I request one specific item by (.*) (.*)")]
        public void WhenIRequestOneSpecificItemByName(string routeTemplateValueToReplace, string name)
        {
            ScenarioContext.Current.Set(name, RouteParameter);
            string requestUrl = InjectRouteParameterIntoRequestUrl(ScenarioContext.Current.Get<string>(RouteUrlKey), name, routeTemplateValueToReplace);

            var requestResult = ApiClient.GetResult(requestUrl);
            ScenarioContext.Current.Set(requestResult, RequestResultKey);
        }

        protected static void AssertMoveIsValid(MoveResource move)
        {
            Assert.That(move, Is.Not.Null, $"{nameof(move)}");
            //Assert.That(move.Angle, Is.Not.Null, $"{nameof(move.Angle)}");
            //Assert.That(move.BaseDamage, Is.Not.Null, $"{nameof(move.BaseDamage)}");
            //Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null, $"{nameof(move.BaseKnockBackSetKnockback)}");
            //Assert.That(move.FirstActionableFrame, Is.Not.Null, $"{nameof(move.FirstActionableFrame)}");
            //Assert.That(move.HitboxActive, Is.Not.Null, $"{nameof(move.HitboxActive)}");
            Assert.That(move.InstanceId, Is.Not.Null, $"{nameof(move.InstanceId)}");
            //Assert.That(move.KnockbackGrowth, Is.Not.Null, $"{nameof(move.KnockbackGrowth)}");
            Assert.That(move.Name, Is.Not.Null, $"{nameof(move.Name)}");
            Assert.That(move.Owner, Is.Not.Null, $"{nameof(move.Owner)}");
            //AssertHalLinksArePresent(move);
            AssertResourceContainsCorrectOwnerLink(move.Links.FirstOrDefault(l => l.Rel.Equals("character")), move.Owner);
        }

        private static void AssertResourceContainsCorrectOwnerLink(Link link, string owner)
        {
            Assert.That(link, Is.Not.Null);
            Assert.That(link.Rel, Is.EqualTo("character"));
            StringAssert.Contains(owner, link.Href.Replace("%20", " "));
        }

        protected static void AssertMovementIsValid(MovementResource movement)
        {
            Assert.That(movement, Is.Not.Null);
            Assert.That(movement.Name, Is.Not.Null);
            Assert.That(movement.Owner, Is.Not.Null);
            Assert.That(movement.Value, Is.Not.Null);
            Assert.That(movement.InstanceId, Is.Not.Null);
            Assert.That(movement.Links.Any(l => l.Rel.Equals("character")), $"Unable to find 'character' link.");
        }

        protected static void AssertHalLinksArePresent(Resource resource)
        {
            const string movesName = "moves";
            const string characterAttributesName = "characterattributes";
            const string movementsName = "movements";

            var moveLink = resource.Links.FirstOrDefault(l => l.Rel.Equals(movesName));
            var characterAttributesLink = resource.Links.FirstOrDefault(l => l.Rel.Equals(characterAttributesName));
            var movementsLink = resource.Links.FirstOrDefault(l => l.Rel.Equals("movements"));

            Assert.That(moveLink, Is.Not.Null, $"Unable to find '{movesName}' link.");
            Assert.That(characterAttributesLink, Is.Not.Null, $"Unable to find '{characterAttributesName}' link.");
            Assert.That(movementsLink, Is.Not.Null, $"Unable to find '{movementsName}' link.");
        }
    }
}

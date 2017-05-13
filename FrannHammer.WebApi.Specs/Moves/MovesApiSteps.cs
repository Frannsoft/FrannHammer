using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FrannHammer.WebApi.Specs.Moves
{
    [Binding]
    [Scope(Feature = "MovesApi")]
    public class MovesApiSteps : BaseSteps
    {
        private static void AssertMoveIsValid(IMove move)
        {
            Assert.That(move, Is.Not.Null, $"{nameof(move)}");
            Assert.That(move.Angle, Is.Not.Null, $"{nameof(move.Angle)}");
            Assert.That(move.BaseDamage, Is.Not.Null, $"{nameof(move.BaseDamage)}");
            Assert.That(move.BaseKnockBackSetKnockback, Is.Not.Null, $"{nameof(move.BaseKnockBackSetKnockback)}");
            Assert.That(move.FirstActionableFrame, Is.Not.Null, $"{nameof(move.FirstActionableFrame)}");
            Assert.That(move.HitboxActive, Is.Not.Null, $"{nameof(move.HitboxActive)}");
            Assert.That(move.Id, Is.Not.Null, $"{nameof(move.Id)}");
            Assert.That(move.KnockbackGrowth, Is.Not.Null, $"{nameof(move.KnockbackGrowth)}");
            Assert.That(move.Name, Is.Not.Null, $"{nameof(move.Name)}");
            Assert.That(move.Owner, Is.Not.Null, $"{nameof(move.Owner)}");
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

        [Then(@"The result should be a list of all move data")]
        public void ThenTheResultShouldBeAListOfAllMoveData()
        {
            var moveMetadata = ApiClient
                .DeserializeResponse<IEnumerable<Move>>(ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey))
                .ToList();

            CollectionAssert.AllItemsAreNotNull(moveMetadata);
            CollectionAssert.AllItemsAreUnique(moveMetadata);
            moveMetadata.ForEach(AssertMoveIsValid);
        }

        [Then(@"The result should be just that moves data")]
        public void ThenTheResultShouldBeJustThatMovesData()
        {
            var move = ApiClient
                .DeserializeResponse<Move>(
                    ScenarioContext.Current.Get<HttpResponseMessage>(RequestResultKey));
            AssertMoveIsValid(move);
        }

        [Then(@"The result should be all moves that match that name")]
        public void ThenTheResultShouldBeAllMovesThatMatchThatName()
        {
            var moves =
                ApiClient.DeserializeResponse<IEnumerable<Move>>(
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
    }
}

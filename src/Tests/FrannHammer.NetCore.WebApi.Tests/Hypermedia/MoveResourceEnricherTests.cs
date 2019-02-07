using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrannHammer.NetCore.WebApi.Tests.Hypermedia
{
    [TestFixture]
    public class MoveResourceEnricherTests
    {
        private static IntegrationTestServer _testServer = new IntegrationTestServer();

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _testServer.Dispose();
        }

        private static IEnumerable<string> CollectionEndpoints()
        {
            return new List<string>{
                "/api/characters/name/bowser/moves",
                 "/api/characters/2/moves",
                "/api/moves/name/Close Ftilt",
                 "/api/moves"
            };
        }

        private static IEnumerable<string> SingleEndpoints()
        {
            return new List<string>
            {
                "/api/moves/b50a889439da465fb86fa0d24e9e7d5c"
            };
        }

        private static IEnumerable<string> ExpandIsTrueCollectionEndpoints()
        {
            foreach (var endpoint in CollectionEndpoints())
            {
                yield return endpoint + "?expand=true";
            }
        }

        private static IEnumerable<string> ExpandIsTrueSingleEndpoints()
        {
            foreach (var endpoint in SingleEndpoints())
            {
                yield return endpoint + "?expand=true";
            }
        }

        private static IEnumerable<string> ExpandIsFalseCollectionEndpoints()
        {
            foreach (var endpoint in CollectionEndpoints())
            {
                yield return endpoint + "?expand=false";
            }
        }

        private static IEnumerable<string> ExpandIsFalseSingleEndpoints()
        {
            foreach (var endpoint in SingleEndpoints())
            {
                yield return endpoint + "?expand=false";
            }
        }

        private void AssertNonExpandedSingleIsValid(dynamic move)
        {
            var hitboxActive = move.HitboxActive as JValue;
            Assert.That(hitboxActive.Type == JTokenType.String);

            var baseDamage = move.BaseDamage as JValue;
            Assert.That(baseDamage.Type == JTokenType.String);
        }

        private void AssertExpandedSingleIsValid(dynamic move)
        {
            Assert.DoesNotThrow(() => { var x = move.HitboxActive.Adv; });
            Assert.DoesNotThrow(() => { var x = move.BaseDamage.OneVOne; });
        }

        private IEnumerable<dynamic> GetMovesExceptThrows(IEnumerable<dynamic> moves) => moves.Where(m => !m.Name.ToString().Contains("throw"));

        [Test]
        [TestCaseSource(nameof(ExpandIsTrueCollectionEndpoints))]
        public async Task ExpandSetToTrueReturnsResourceWithExtraMoveProperties_Collections(string endpoint)
        {
            var response = await _testServer.GetAsync(endpoint);

            var moves = JsonConvert.DeserializeObject<List<dynamic>>(await response.Content.ReadAsStringAsync());

            //throws don't have hitboxactive or base damage values
            foreach (var move in GetMovesExceptThrows(moves))
            {
                AssertExpandedSingleIsValid(move);
            }
        }

        [Test]
        [TestCaseSource(nameof(ExpandIsTrueSingleEndpoints))]
        public async Task ExpandSetToTrueReturnsResourceWithExtraMoveProperties_Single(string endpoint)
        {
            var response = await _testServer.GetAsync(endpoint);

            var move = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            AssertExpandedSingleIsValid(move);
        }

        [Test]
        [TestCaseSource(nameof(ExpandIsFalseCollectionEndpoints))]
        public async Task ExpandSetToFalseReturnsResourceWithNoExtraMoveProperties_Collection(string endpoint)
        {
            var response = await _testServer.GetAsync(endpoint);

            var moves = JsonConvert.DeserializeObject<List<dynamic>>(await response.Content.ReadAsStringAsync());

            //throws don't have hitboxactive or base damage values
            foreach (var move in GetMovesExceptThrows(moves))
            {
                AssertNonExpandedSingleIsValid(move);
                Assert.That(move.HitboxActive, Does.Not.Contain(";"));
                Assert.That(move.BaseDamage, Does.Not.Contain(";"));
            }
        }

        [Test]
        [TestCaseSource(nameof(CollectionEndpoints))]
        public async Task ExpandDefaultValueReturnsResourceWithNoExtraMoveProperties_Collection(string endpoint)
        {
            var response = await _testServer.GetAsync(endpoint);

            var moves = JsonConvert.DeserializeObject<List<dynamic>>(await response.Content.ReadAsStringAsync());

            //throws don't have hitboxactive or base damage values
            foreach (var move in GetMovesExceptThrows(moves))
            {
                AssertNonExpandedSingleIsValid(move);
                Assert.That(move.HitboxActive, Does.Not.Contain(";"));
                Assert.That(move.BaseDamage, Does.Not.Contain(";"));
            }
        }

        [Test]
        [TestCaseSource(nameof(ExpandIsFalseSingleEndpoints))]
        public async Task ExpandSetToFalseReturnsResourceWithNoExtraMoveProperties_Single(string endpoint)
        {
            var response = await _testServer.GetAsync(endpoint);

            var move = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            AssertNonExpandedSingleIsValid(move);
            Assert.That(move.HitboxActive, Does.Not.Contain(";"));
            Assert.That(move.BaseDamage, Does.Not.Contain(";"));
        }

        [Test]
        [TestCaseSource(nameof(SingleEndpoints))]
        public async Task ExpandDefaultValueReturnsResourceWithNoExtraMoveProperties_Single(string endpoint)
        {
            var response = await _testServer.GetAsync(endpoint);

            var move = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            AssertNonExpandedSingleIsValid(move);
            Assert.That(move.HitboxActive, Does.Not.Contain(";"));
            Assert.That(move.BaseDamage, Does.Not.Contain(";"));
        }
    }
}

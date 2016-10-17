using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class NameSearchPredicateServiceTest
    {
        [Test]
        public void DoesValidNameFindMove()
        {
            var service = new NameSearchPredicateService();
            string expectedName = "test";

            var createdPredicate = service.GetNameDelegate(expectedName);

            var didFindMove = createdPredicate(new Move
            {
                Name = "testname"
            });

            Assert.That(didFindMove, "Unable to find move using created predicate");
        }

        [Test]
        public void DoesNotFindMoveWhenMoveNotExpectedToBeFoundByName()
        {
            var service = new NameSearchPredicateService();
            string expectedName = "zeta";

            var createdPredicate = service.GetNameDelegate(expectedName);

            var didFindMove = createdPredicate(new Move
            {
                Name = "testname"
            });

            Assert.That(!didFindMove, "Found move using created predicate");
        }

        [Test]
        public void DoesEmptyNameFindAllMoves()
        {
            var service = new NameSearchPredicateService();
            string expectedName = string.Empty;

            var createdPredicate = service.GetNameDelegate(expectedName);

            var didFindMove = createdPredicate(new Move
            {
                Name = "test"
            });

            Assert.That(didFindMove, "Did not find move using created predicate of empty string");
        }
    }
}

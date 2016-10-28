using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class NameSearchPredicateServiceTest
    {

        private NameSearchPredicateService _service;
        private const string UnableToFindValueMessage = "Unable to find value in range";
        private const string FoundValueMessage = "Found value in range";

        [SetUp]
        public void SetUp()
        {
            _service = new NameSearchPredicateService();
        }


        [Test]
        public void DoesValidNameFindMove()
        {
            string expectedName = "test";

            var createdPredicate = _service.GetNamePredicate(expectedName);

            var didFindMove = createdPredicate(new Move
            {
                Name = "testname"
            });

            Assert.That(didFindMove, UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotFindMoveWhenMoveNotExpectedToBeFoundByName()
        {
            string expectedName = "zeta";

            var createdPredicate = _service.GetNamePredicate(expectedName);

            var didFindMove = createdPredicate(new Move
            {
                Name = "testname"
            });

            Assert.That(!didFindMove, FoundValueMessage);
        }

        [Test]
        public void DoesEmptyNameFindAllMoves()
        {
            string expectedName = string.Empty;

            var createdPredicate = _service.GetNamePredicate(expectedName);

            var didFindMove = createdPredicate(new Move
            {
                Name = "test"
            });

            Assert.That(didFindMove, "Did not find move using created predicate of empty string");
        }
    }
}

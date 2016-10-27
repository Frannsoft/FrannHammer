using FrannHammer.Models;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public class CharacterNameSearchPredicateServiceTest
    {
        private CharacterNameSearchPredicateService _service;
        private const string UnableToFindValueMessage = "Unable to find value in range";
        private const string FoundValueMessage = "Found value in range";

        [SetUp]
        public void SetUp()
        {
            _service = new CharacterNameSearchPredicateService();
        }

        [Test]
        public void DoesValidNameFindMove()
        {
            string expectedName = "test";

            var createdPredicate = _service.GetNameDelegate(expectedName);

            var didFindMove = createdPredicate(new Character
            {
                DisplayName = "test"
            });

            Assert.That(didFindMove, UnableToFindValueMessage);
        }

        [Test]
        public void DoesNotFindMoveWhenMoveNotExpectedToBeFoundByName()
        {
            string expectedName = "zeta";

            var createdPredicate = _service.GetNameDelegate(expectedName);

            var didFindMove = createdPredicate(new Character
            {
                DisplayName = "test"
            });

            Assert.That(!didFindMove, FoundValueMessage);
        }

        [Test]
        public void DoesEmptyNameFindAllMoves()
        {
            string expectedName = string.Empty;

            var createdPredicate = _service.GetNameDelegate(expectedName);

            var didFindMove = createdPredicate(new Character
            {
                DisplayName = "test"
            });

            Assert.That(didFindMove, "Did not find move using created predicate of empty string");
        }
    }
}

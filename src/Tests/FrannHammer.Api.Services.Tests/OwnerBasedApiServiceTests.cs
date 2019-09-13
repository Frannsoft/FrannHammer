using System.Collections.Generic;
using System.Linq;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping;
using Moq;
using NUnit.Framework;
using static FrannHammer.Api.Services.Tests.ApiServiceTestSetupUtility;

namespace FrannHammer.Api.Services.Tests
{
    public class OwnerBasedApiServiceTests
    {
        [Test]
        public void GetsAllMovesForSpecifiedOwner()
        {
            const string expectedCharacterName = "testCharacter";

            var items = new List<Move>
            {
                new Move {Name = "test", Owner = expectedCharacterName, MoveType = MoveType.Aerial.GetEnumDescription()},
                new Move {Name = "test", Owner = expectedCharacterName, MoveType = MoveType.Ground.GetEnumDescription()},
                new Move {Name = "test", Owner = expectedCharacterName, MoveType = MoveType.Throw.GetEnumDescription()},
                new Move { Name = "anonymousMove", Owner = "anonymousOwner"}
            };

            var mockRepository = new Mock<IRepository<IMove>>();
            ConfigureGetAllWhereOnMockRepository(mockRepository, items);

            var sut = new OwnerBasedApiService<IMove>(mockRepository.Object, new GameParameterParserService("Smash4"));

            var results = sut.GetAllWhereCharacterNameIs(expectedCharacterName).ToList();

            Assert.That(results.Count, Is.EqualTo(3), $"{nameof(results.Count)}");

            results.ForEach(result =>
            {
                Assert.That(result.Owner, Is.EqualTo(expectedCharacterName), $"{result.Owner}");
            });
        }

        [Test]
        public void VerifyGetAllMovesForCharacterCallsGetAllWhere()
        {
            var items = new List<Move>
            {
                new Move {Name = "test", Owner="testowner"}
            };

            var mockRepository = new Mock<IRepository<IMove>>();
            ConfigureGetAllWhereOnMockRepository(mockRepository, items);

            var sut = new OwnerBasedApiService<IMove>(mockRepository.Object, new GameParameterParserService("Smash4"));

            sut.GetAllWhereCharacterNameIs("dummyValue");

            mockRepository.VerifyAll();
        }
    }
}

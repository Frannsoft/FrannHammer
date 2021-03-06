﻿using AutoFixture;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.Api.Services.Tests
{
    public class ApiServiceTestSetupUtility
    {
        public static void ConfigureGetAllWhereOnMockRepository(Mock<IRepository<IMove>> mockRepository, IEnumerable<IMove> testDataStore)
        {
            mockRepository.Setup(r => r.GetAllWhere(It.IsAny<Func<IMove, bool>>()))
               .Returns((Func<IMove, bool> where) => testDataStore.Where(where));
        }

        public static IRepository<IMove> ConfigureMockRepositoryWithSeedMoves(IEnumerable<Move> matchingMoves, Fixture fixture)
        {
            //add fake moves with all properties filled out.  Some should match the passed in name, others should not
            var matchingItems = matchingMoves;

            var itemsForMockRepository = fixture.CreateMany<Move>().ToList();
            itemsForMockRepository.AddRange(matchingItems);

            //mock repository
            var mockRepository = new Mock<IRepository<IMove>>();

            ConfigureGetAllWhereOnMockRepository(mockRepository, itemsForMockRepository);

            mockRepository.Setup(r => r.GetSingleWhere(It.IsAny<Func<IMove, bool>>()))
                .Returns((Func<IMove, bool> where) => itemsForMockRepository.Single(where));

            mockRepository.Setup(r => r.GetAllWhere(It.IsAny<Func<IMove, bool>>()))
                .Returns((Func<IMove, bool> where) => itemsForMockRepository.Where(where));

            mockRepository.Setup(r => r.GetAll()).Returns(() => itemsForMockRepository);

            return mockRepository.Object;
        }

    }
}

using System;
using System.Data.Entity;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Services;
using Moq;

namespace FrannHammer.Api.Tests
{
    public class BaseTest
    {
        protected Mock<IApplicationDbContext> DbContextMock;

        protected TService Init<TService, TEntity>(IQueryable<TEntity> entities,
                Mock<DbSet<TEntity>> entitiesMock)
            where TService : BaseService
            where TEntity : class, IEntity
        {
            entitiesMock.As<IQueryable<TEntity>>().Setup(t => t.Provider).Returns(entities.Provider);
            entitiesMock.As<IQueryable<TEntity>>().Setup(t => t.Expression).Returns(entities.Expression);
            entitiesMock.As<IQueryable<TEntity>>().Setup(t => t.ElementType).Returns(entities.ElementType);
            entitiesMock.As<IQueryable<TEntity>>().Setup(t => t.GetEnumerator()).Returns(entities.GetEnumerator());
            entitiesMock.Setup(t => t.Find(It.IsAny<int>()))
                .Returns<object[]>(ids => entities.FirstOrDefault(t => t.Id == (int)ids[0]));

            DbContextMock = new Mock<IApplicationDbContext>();

            return (TService)Activator.CreateInstance(typeof(TService), DbContextMock.Object);
        }
    }
}

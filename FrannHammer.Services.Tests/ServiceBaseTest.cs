using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Effort.DataLoaders;
using FrannHammer.Models;
using Moq;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [TestFixture]
    public abstract class ServiceBaseTest
    {
        private DbConnection _connection;

        protected Mock<IApplicationDbContext> DbContextMock;
        protected ApplicationDbContext Context;

        [SetUp]
        public virtual void SetUp()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Throw, ThrowDto>();
                cfg.CreateMap<ThrowDto, Throw>();
                cfg.CreateMap<ThrowType, ThrowTypeDto>();
                cfg.CreateMap<ThrowTypeDto, ThrowType>();
                cfg.CreateMap<Angle, AngleDto>();
                cfg.CreateMap<AngleDto, Angle>();
                cfg.CreateMap<BaseDamage, BaseDamageDto>();
                cfg.CreateMap<BaseDamageDto, BaseDamage>();
                cfg.CreateMap<KnockbackGrowth, KnockbackGrowthDto>();
                cfg.CreateMap<KnockbackGrowthDto, KnockbackGrowth>();
                cfg.CreateMap<Hitbox, HitboxDto>();
                cfg.CreateMap<HitboxDto, Hitbox>();
                cfg.CreateMap<Character, CharacterDto>();
                cfg.CreateMap<CharacterDto, Character>();
                cfg.CreateMap<Movement, MovementDto>();
                cfg.CreateMap<MovementDto, Movement>();
                cfg.CreateMap<Move, MoveDto>();
                cfg.CreateMap<MoveDto, Move>();
                cfg.CreateMap<SmashAttributeType, SmashAttributeTypeDto>();
                cfg.CreateMap<SmashAttributeTypeDto, SmashAttributeType>();
                cfg.CreateMap<CharacterAttributeType, CharacterAttributeTypeDto>();
                cfg.CreateMap<CharacterAttributeTypeDto, CharacterAttributeType>();
                cfg.CreateMap<CharacterAttribute, CharacterAttributeDto>();
                cfg.CreateMap<CharacterAttributeDto, CharacterAttribute>();
            });

            var path = AppDomain.CurrentDomain.BaseDirectory;
            IDataLoader loader = new CsvDataLoader($"{path}\\fakeDb\\");

            _connection = Effort.DbConnectionFactory.CreateTransient(loader);
            Context = new ApplicationDbContext(_connection);
        }

        //protected TService Init<TService, TEntity>(IQueryable<TEntity> entities, 
        //        Mock<DbSet<TEntity>> entitiesMock)
        //    where TService : BaseService
        //    where TEntity : class, IEntity
        //{
        //    entitiesMock.As<IQueryable<TEntity>>().Setup(t => t.Provider).Returns(entities.Provider);
        //    entitiesMock.As<IQueryable<TEntity>>().Setup(t => t.Expression).Returns(entities.Expression);
        //    entitiesMock.As<IQueryable<TEntity>>().Setup(t => t.ElementType).Returns(entities.ElementType);
        //    entitiesMock.As<IQueryable<TEntity>>().Setup(t => t.GetEnumerator()).Returns(entities.GetEnumerator());
        //    entitiesMock.Setup(t => t.Find(It.IsAny<int>()))
        //        .Returns<object[]>(ids => entities.FirstOrDefault(t => t.Id == (int)ids[0]));

        //    DbContextMock = new Mock<IApplicationDbContext>();

        //    return (TService)Activator.CreateInstance(typeof(TService), DbContextMock.Object);
        //}
    }
}

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

        
    }
}

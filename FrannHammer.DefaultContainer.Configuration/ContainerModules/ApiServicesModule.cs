﻿using Autofac;
using FrannHammer.Api.Services;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.DefaultContainer.Configuration.ContainerModules
{
    public class ApiServicesModule : Module
    {
        private const string RepositoryParameterName = "repository";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultCharacterService>()
                .As<ICharacterService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<ICharacter>>());

            builder.RegisterType<DefaultMovementService>()
                .As<IMovementService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<IMovement>>());

            builder.RegisterType<DefaultMoveService>()
                .As<IMoveService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<IMove>>())
                .WithParameter((pi, c) => pi.Name == "queryMappingService",
                 (pi, c) => c.Resolve<IQueryMappingService>());

            builder.RegisterType<DefaultCharacterAttributeService>()
                .As<ICharacterAttributeRowService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<ICharacterAttributeRow>>());

            builder.RegisterType<DefaultAttributeStrategy>().As<IAttributeStrategy>();

            builder.RegisterType<QueryMappingService>()
                .As<IQueryMappingService>()
                .WithParameter((pi, c) => pi.Name == "attributeStrategy",
                (pi, c) => c.Resolve<IAttributeStrategy>());

            builder.RegisterType<DefaultDtoProvider>().As<IDtoProvider>();
        }
    }
}

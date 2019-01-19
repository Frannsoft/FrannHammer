using Autofac;
using FrannHammer.Api.Services;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.DefaultContainer.Configuration.ContainerModules
{
    public class ApiServicesModule : Module
    {
        private const string RepositoryParameterName = "repository";
        private const string QueryMappingParameterName = "queryMappingService";

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<DefaultCharacterService>()
            //    .As<ICharacterService>()
            //    .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
            //        (pi, c) => c.Resolve<IRepository<ICharacter>>())
            //        .WithParameter((pi, c) => pi.Name == "dtoProvider",
            //            (pi, c) => c.Resolve<IDtoProvider>())
            //        .WithParameter((pi, c) => pi.Name == "movementService",
            //            (pi, c) => c.Resolve<IMovementService>())
            //        .WithParameter((pi, c) => pi.Name == "attributeRowService",
            //            (pi, c) => c.Resolve<ICharacterAttributeRowService>())
            //        .WithParameter((pi, c) => pi.Name == "game",
            //            (pi, c) => c.Resolve<IActionContextAccessor>().ActionContext;

            builder.RegisterType<DefaultMovementService>()
                .As<IMovementService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<IMovement>>());

            builder.RegisterType<DefaultMoveService>()
                .As<IMoveService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<IMove>>())
                .WithParameter((pi, c) => pi.Name == QueryMappingParameterName,
                 (pi, c) => c.Resolve<IQueryMappingService>());

            builder.RegisterType<DefaultUniqueDataService>()
                .As<IUniqueDataService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<IUniqueData>>())
                .WithParameter((pi, c) => pi.Name == QueryMappingParameterName,
                 (pi, c) => c.Resolve<IQueryMappingService>());

            builder.RegisterType<DefaultCharacterAttributeNameProvider>().As<ICharacterAttributeNameProvider>();

            builder.RegisterType<DefaultCharacterAttributeService>()
                .As<ICharacterAttributeRowService>()
                .WithParameter((pi, c) => pi.Name == RepositoryParameterName,
                    (pi, c) => c.Resolve<IRepository<ICharacterAttributeRow>>())
                    .WithParameter((pi, c) => pi.Name == "characterAttributeNameProvider",
                    (pi, c) => c.Resolve<ICharacterAttributeNameProvider>());

            builder.RegisterType<DefaultAttributeStrategy>().As<IAttributeStrategy>();

            builder.RegisterType<QueryMappingService>()
                .As<IQueryMappingService>()
                .WithParameter((pi, c) => pi.Name == "attributeStrategy",
                (pi, c) => c.Resolve<IAttributeStrategy>());

            builder.RegisterType<DefaultDtoProvider>().As<IDtoProvider>();
        }
    }
}

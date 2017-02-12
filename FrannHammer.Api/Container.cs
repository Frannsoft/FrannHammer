using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using FrannHammer.Api.Controllers;
using FrannHammer.Services;
using FrannHammer.Services.MoveSearch;

namespace FrannHammer.Api
{
    public class Container
    {
        private readonly IContainer _container;

        public IContainer AutoFacContainer => _container;

        internal static Container Instance = new Container();

        private Container()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationDbContext>()
                    .As<IApplicationDbContext>();

            const string metadataServiceName = "metadataService";

            var context = new ApplicationDbContext();
            var metadataResultValidationService = new ResultValidationService();
            var metadataService = new MetadataService(context, metadataResultValidationService);

            builder.RegisterType<AnglesController>()
                .As<AnglesController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<FirstActionableFramesController>()
                .As<FirstActionableFramesController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<BaseDamagesController>()
                .As<BaseDamagesController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<CharacterAttributesController>()
                .As<CharacterAttributesController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<CharacterAttributeTypesController>()
                .As<CharacterAttributeTypesController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<CharactersController>()
                .As<CharactersController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<HitboxesController>()
                .As<HitboxesController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<KnockbackGrowthsController>()
                .As<KnockbackGrowthsController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<MovementsController>()
                .As<MovementsController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<MovesController>()
                .As<MovesController>()
                .WithParameter(metadataServiceName, metadataService)
                .WithParameter("redisConnectionMultiplexer", WebApiConfig.RedisMultiplexer);

            builder.RegisterType<NotationsController>()
                .As<NotationsController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<SmashAttributeTypesController>()
                .As<SmashAttributeTypesController>()
                .WithParameter("smashAttributeTypesService", new SmashAttributeTypeService(context, metadataResultValidationService));

            builder.RegisterType<ThrowsController>()
                .As<ThrowsController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<ThrowTypesController>()
                .As<ThrowTypesController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<BaseKnockbacksController>()
                .As<BaseKnockbacksController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterType<SetKnockbacksController>()
                .As<SetKnockbacksController>()
                .WithParameter(metadataServiceName, metadataService);

            builder.RegisterApiControllers();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterType<IApplicationDbContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            _container = builder.Build();
        }

        static Container()
        { }
    }
}
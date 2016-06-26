using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using FrannHammer.Api.Models;

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
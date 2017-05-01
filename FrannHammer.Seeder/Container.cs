using Autofac;
using FrannHammer.Seeder.ContainerModules;
using FrannHammer.Seeding;

namespace FrannHammer.Seeder
{
    public class Container
    {
        private readonly IContainer _container;

        internal static Container Instance = new Container();

        private Container()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ScrapingModule>();
            builder.RegisterModule<DatabaseModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ApiServicesModule>();

            //seeder
            builder.RegisterType<DefaultSeeder>()
                .AsSelf();

            _container = builder.Build();
        }

        static Container()
        { }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
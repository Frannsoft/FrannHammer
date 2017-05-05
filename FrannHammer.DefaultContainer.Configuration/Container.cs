using Autofac;
using FrannHammer.DefaultContainer.Configuration.ContainerModules;

namespace FrannHammer.DefaultContainer.Configuration
{
    public class Container
    {
        private IContainer _container;

        public static Container Instance = new Container();

        private Container()
        { }

        static Container()
        { }

        public Container BuildDefault()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ScrapingModule>();
            builder.RegisterModule<DatabaseModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ApiServicesModule>();

            return this;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}

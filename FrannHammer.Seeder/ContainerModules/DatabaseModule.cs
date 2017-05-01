using Autofac;
using MongoDB.Driver;

namespace FrannHammer.Seeder.ContainerModules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //TODO - get thes values from config settings

            builder.RegisterType<MongoClient>()
               .AsSelf()
               .WithParameter((pi, c) => pi.Name == "url",
                   (pi, c) => new MongoUrl("mongodb://testuser:password@ds119151.mlab.com:19151/playgroundfranndotexe"));
        }
    }
}

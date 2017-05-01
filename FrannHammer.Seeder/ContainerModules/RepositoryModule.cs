using Autofac;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.DataAccess.MongoDb;
using FrannHammer.Domain.Contracts;
using MongoDB.Driver;

namespace FrannHammer.Seeder.ContainerModules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //TODO - get these values from config settings

            builder.RegisterType<MongoDbRepository<ICharacter>>()
                 .As<IRepository<ICharacter>>()
                 .WithParameter((pi, c) => pi.Name == "mongoDatabase",
                     (pi, c) =>
                     {
                         var mongoClient = c.Resolve<MongoClient>();
                         return mongoClient.GetDatabase("playgroundfranndotexe");
                     });

            builder.RegisterType<MongoDbRepository<IMovement>>()
                .As<IRepository<IMovement>>()
                .WithParameter((pi, c) => pi.Name == "mongoDatabase",
                    (pi, c) =>
                    {
                        var mongoClient = c.Resolve<MongoClient>();
                        return mongoClient.GetDatabase("playgroundfranndotexe");
                    });


            builder.RegisterType<MongoDbRepository<IMove>>()
                .As<IRepository<IMove>>()
                .WithParameter((pi, c) => pi.Name == "mongoDatabase",
                    (pi, c) =>
                    {
                        var mongoClient = c.Resolve<MongoClient>();
                        return mongoClient.GetDatabase("playgroundfranndotexe");
                    });


            builder.RegisterType<MongoDbRepository<ICharacterAttributeRow>>()
                .As<IRepository<ICharacterAttributeRow>>()
                .WithParameter((pi, c) => pi.Name == "mongoDatabase",
                    (pi, c) =>
                    {
                        var mongoClient = c.Resolve<MongoClient>();
                        return mongoClient.GetDatabase("playgroundfranndotexe");
                    });

        }
    }
}

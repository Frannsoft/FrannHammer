using System.Configuration;
using Autofac;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.DataAccess.MongoDb;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using MongoDB.Driver;

namespace FrannHammer.DefaultContainer.Configuration.ContainerModules
{
    public class RepositoryModule : Module
    {
        private const string MongoDatabaseKey = "mongoDatabase";

        protected override void Load(ContainerBuilder builder)
        {
            string databaseName = ConfigurationManager.AppSettings[ConfigurationKeys.DatabaseName];

            builder.RegisterType<MongoDbRepository<ICharacter>>()
                 .As<IRepository<ICharacter>>()
                 .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
                     (pi, c) =>
                     {
                         var mongoClient = c.Resolve<MongoClient>();
                         return mongoClient.GetDatabase(databaseName);
                     });

            builder.RegisterType<MongoDbRepository<IMovement>>()
                .As<IRepository<IMovement>>()
                .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
                    (pi, c) =>
                    {
                        var mongoClient = c.Resolve<MongoClient>();
                        return mongoClient.GetDatabase(databaseName);
                    });

            builder.RegisterType<MongoDbRepository<IMove>>()
                .As<IRepository<IMove>>()
                .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
                    (pi, c) =>
                    {
                        var mongoClient = c.Resolve<MongoClient>();
                        return mongoClient.GetDatabase(databaseName);
                    });

            builder.RegisterType<MongoDbRepository<ICharacterAttributeRow>>()
                .As<IRepository<ICharacterAttributeRow>>()
                .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
                    (pi, c) =>
                    {
                        var mongoClient = c.Resolve<MongoClient>();
                        return mongoClient.GetDatabase(databaseName);
                    });

            builder.RegisterType<MongoDbRepository<IUniqueData>>()
              .As<IRepository<IUniqueData>>()
              .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
                  (pi, c) =>
                  {
                      var mongoClient = c.Resolve<MongoClient>();
                      return mongoClient.GetDatabase(databaseName);
                  });
        }
    }
}

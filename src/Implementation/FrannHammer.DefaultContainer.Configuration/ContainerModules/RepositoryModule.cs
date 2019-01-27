using Autofac;

namespace FrannHammer.DefaultContainer.Configuration.ContainerModules
{
    public class RepositoryModule : Module
    {
        private const string MongoDatabaseKey = "mongoDatabase";

        private readonly string _databaseName;

        public RepositoryModule(string databaseName)
        {
            _databaseName = databaseName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //string databaseName = _databaseName;//ConfigurationManager.AppSettings[ConfigurationKeys.DatabaseName];

            //builder.RegisterType<MongoDbRepository<ICharacter>>()
            //     .As<IRepository<ICharacter>>()
            //     .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
            //         (pi, c) =>
            //         {
            //             var mongoClient = c.Resolve<MongoClient>();
            //             return mongoClient.GetDatabase(databaseName);
            //         });

            //builder.RegisterType<MongoDbRepository<IMovement>>()
            //    .As<IRepository<IMovement>>()
            //    .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
            //        (pi, c) =>
            //        {
            //            var mongoClient = c.Resolve<MongoClient>();
            //            return mongoClient.GetDatabase(databaseName);
            //        });

            //builder.RegisterType<MongoDbRepository<IMove>>()
            //    .As<IRepository<IMove>>()
            //    .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
            //        (pi, c) =>
            //        {
            //            var mongoClient = c.Resolve<MongoClient>();
            //            return mongoClient.GetDatabase(databaseName);
            //        });

            //builder.RegisterType<MongoDbRepository<ICharacterAttributeRow>>()
            //    .As<IRepository<ICharacterAttributeRow>>()
            //    .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
            //        (pi, c) =>
            //        {
            //            var mongoClient = c.Resolve<MongoClient>();
            //            return mongoClient.GetDatabase(databaseName);
            //        });

            //builder.RegisterType<MongoDbRepository<IUniqueData>>()
            //  .As<IRepository<IUniqueData>>()
            //  .WithParameter((pi, c) => pi.Name == MongoDatabaseKey,
            //      (pi, c) =>
            //      {
            //          var mongoClient = c.Resolve<MongoClient>();
            //          return mongoClient.GetDatabase(databaseName);
            //      });
        }
    }
}

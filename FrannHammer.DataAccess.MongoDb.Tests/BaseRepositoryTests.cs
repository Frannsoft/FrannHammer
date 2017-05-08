using System;
using FrannHammer.Domain;
using FrannHammer.Tests.Utility;
using MongoDB.Driver;
using Ploeh.AutoFixture;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    public abstract class BaseRepositoryTests
    {
        protected IMongoDatabase MongoDatabase { get; private set; }
        protected Fixture Fixture { get; }

        protected BaseRepositoryTests(params Type[] modelTypes)
        {
            Fixture = new Fixture();

            BsonMapper.RegisterTypeWithAutoMap<MongoModel>();
            BsonMapper.RegisterClassMaps(modelTypes);

            MongoDatabase = MongoDbConnectionFactory.GetDatabaseFromAppConfig();
        }
    }
}

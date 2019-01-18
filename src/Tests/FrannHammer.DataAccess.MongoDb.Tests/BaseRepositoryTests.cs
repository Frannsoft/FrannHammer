using AutoFixture;
using FrannHammer.Domain;
using FrannHammer.Tests.Utility;
using FrannHammer.Utility;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

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


            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", true, true)
                .Build();

            string username = configuration[ConfigurationKeys.Username];
            string password = configuration[ConfigurationKeys.Password];
            string databaseName = configuration[ConfigurationKeys.DatabaseName];
            string connectionString = configuration[ConfigurationKeys.DefaultConnection];

            MongoDatabase = MongoDbConnectionFactory.GetDatabaseFromAppConfig(username, password, databaseName, connectionString);
        }
    }
}

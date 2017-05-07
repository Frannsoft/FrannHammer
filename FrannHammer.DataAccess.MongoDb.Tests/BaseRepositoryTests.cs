using System;
using System.Linq;
using System.Reflection;
using FrannHammer.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NUnit.Framework;
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

            if(!BsonClassMap.IsClassMapRegistered(typeof(MongoModel)))
            {
                BsonClassMap.RegisterClassMap<MongoModel>(m =>
                {
                    m.AutoMap();
                });
            }

            foreach (var modelType in modelTypes)
            {
                if(BsonClassMap.IsClassMapRegistered(modelType))
                { continue; }

                var classMap = new BsonClassMap(modelType);
                var properties =
                    modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(p => p.GetCustomAttribute<FriendlyNameAttribute>() != null)
                        .ToList();

                Assert.That(properties.Count > 0);

                foreach (var prop in properties)
                {
                    classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
                }

                BsonClassMap.RegisterClassMap(classMap);
            }

            var mongoClient = new MongoClient(new MongoUrl("mongodb://testuser:password@ds119151.mlab.com:19151/playgroundfranndotexe"));
            MongoDatabase = mongoClient.GetDatabase("playgroundfranndotexe");
        }
    }
}

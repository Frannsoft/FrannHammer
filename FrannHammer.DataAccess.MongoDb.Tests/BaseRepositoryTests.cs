using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrannHammer.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NUnit.Framework;

namespace FrannHammer.DataAccess.MongoDb.Tests
{
    public abstract class BaseRepositoryTests
    {
        protected abstract Type ModelType { get; }

        protected IMongoDatabase MongoDatabase { get; private set; }

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            var classMap = new BsonClassMap(ModelType);
            var properties =
                ModelType.GetProperties()
                    .Where(p => p.GetCustomAttribute<FriendlyNameAttribute>() != null)
                    .ToList();

            Assert.That(properties.Count > 0);

            foreach (var prop in properties)
            {
                classMap.MapMember(prop).SetElementName(prop.GetCustomAttribute<FriendlyNameAttribute>().Name);
            }

            BsonClassMap.RegisterClassMap(classMap);

            var mongoClient = new MongoClient(new MongoUrl("mongodb://testuser:password@ds058739.mlab.com:58739/testfranndotexe"));
            MongoDatabase = mongoClient.GetDatabase("testfranndotexe");
        }
    }
}

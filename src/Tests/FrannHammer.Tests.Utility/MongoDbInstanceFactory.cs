using MongoDB.Driver;
using System;
using System.Linq;

namespace FrannHammer.Tests.Utility
{
    public static class MongoDbConnectionFactory
    {
        public static IMongoDatabase GetDatabase(string uri)
        {
            string databaseName = uri.Split('/').Last(); //database name should be on end of uri by default

            if (string.IsNullOrEmpty(databaseName))
            { throw new InvalidOperationException($"Error getting datasource name in uri '{uri};"); }

            var mongoClient = new MongoClient(new MongoUrl(uri));
            return mongoClient.GetDatabase(databaseName);
        }

        public static IMongoDatabase GetDatabaseFromAppConfig(string username, string password, string databaseName, string connectionString)
        {
            //string username = //ConfigurationManager.AppSettings[ConfigurationKeys.Username];
            //string password = //ConfigurationManager.AppSettings[ConfigurationKeys.Password];
            //string databaseName = //ConfigurationManager.AppSettings[ConfigurationKeys.DatabaseName];
            //string connectionString = //ConfigurationManager.ConnectionStrings[ConfigurationKeys.DefaultConnection].ConnectionString;

            string mongoUrlRaw = $"mongodb://{username}:{password}@{connectionString}{databaseName}";
            return GetDatabase(mongoUrlRaw);
        }
    }
}

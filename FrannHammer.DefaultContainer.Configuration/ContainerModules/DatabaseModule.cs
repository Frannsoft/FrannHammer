using System.Configuration;
using Autofac;
using FrannHammer.Utility;
using MongoDB.Driver;

namespace FrannHammer.DefaultContainer.Configuration.ContainerModules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string username = ConfigurationManager.AppSettings[ConfigurationKeys.Username];
            string password = ConfigurationManager.AppSettings[ConfigurationKeys.Password];
            string databaseName = ConfigurationManager.AppSettings[ConfigurationKeys.DatabaseName];
            string connectionString = ConfigurationManager.ConnectionStrings[ConfigurationKeys.DefaultConnection].ConnectionString;

            string mongoUrlRaw = $"mongodb://{username}:{password}@{connectionString}{databaseName}";

            builder.RegisterType<MongoClient>()
               .AsSelf()
               .WithParameter((pi, c) => pi.Name == "url",
                   (pi, c) => new MongoUrl(mongoUrlRaw));
        }
    }
}

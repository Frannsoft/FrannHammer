using Autofac;
using MongoDB.Driver;

namespace FrannHammer.DefaultContainer.Configuration.ContainerModules
{
    public class DatabaseModule : Module
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _databaseName;
        private readonly string _connectionString;

        public DatabaseModule(string username, string password, string databaseName, string connectionString)
        {
            _username = username;
            _password = password;
            _databaseName = databaseName;
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            string username = _username;//Configuration[ConfigurationKeys.Username];
            string password = _password;//ConfigurationManager.AppSettings[ConfigurationKeys.Password];
            string databaseName = _databaseName;//ConfigurationManager.AppSettings[ConfigurationKeys.DatabaseName];
            string connectionString = _connectionString;//ConfigurationManager.ConnectionStrings[ConfigurationKeys.DefaultConnection].ConnectionString;

            string mongoUrlRaw = $"mongodb://{username}:{password}@{connectionString}{databaseName}";

            builder.RegisterType<MongoClient>()
               .AsSelf()
               .WithParameter((pi, c) => pi.Name == "url",
                   (pi, c) => new MongoUrl(mongoUrlRaw));
        }
    }
}

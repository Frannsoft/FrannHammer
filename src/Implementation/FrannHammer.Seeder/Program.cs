using Autofac;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.DefaultContainer.Configuration.ContainerModules;
using FrannHammer.Seeding;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Domain;
using System;

namespace FrannHammer.Seeder
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Startup.InitializeMapping();

            Console.WriteLine("Start? Press a key.");
            Console.ReadLine();

            //configure container
            var builder = new ContainerBuilder();

            builder.RegisterModule<ScrapingModule>();
            //builder.RegisterModule(new DatabaseModule(ConfigurationManager.AppSettings[ConfigurationKeys.Username], ConfigurationManager.AppSettings[ConfigurationKeys.Password], ConfigurationManager.AppSettings[ConfigurationKeys.DatabaseName]
            //    , ConfigurationManager.ConnectionStrings[ConfigurationKeys.DefaultConnection].ConnectionString));

            //builder.RegisterModule(new RepositoryModule(ConfigurationManager.AppSettings[ConfigurationKeys.DatabaseName]));
            //builder.RegisterModule<ApiServicesModule>();
            builder.RegisterType<DefaultSeeder>()
                .AsSelf();

            var container = builder.Build();
            var characterDataScraper = container.Resolve<ICharacterDataScraper>();
            var seeder = container.Resolve<DefaultSeeder>();

            foreach (var character in Characters.All)
            {
                Console.WriteLine($"Scraping data for '{character.Name}'...");
                characterDataScraper.PopulateCharacterFromWeb(character);

                seeder.SeedCharacterData(character,
                    container.Resolve<ICharacterService>(),
                    container.Resolve<IMovementService>(),
                    container.Resolve<IMoveService>(),
                    container.Resolve<ICharacterAttributeRowService>(),
                    container.Resolve<IUniqueDataService>());
            }

            Console.WriteLine("Seeding completed.  Press any key to close.");
            Console.Read();
        }
    }
}

using System;
using Autofac;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Domain;
using FrannHammer.Seeding;
using FrannHammer.DefaultContainer.Configuration.ContainerModules;

namespace FrannHammer.Seeder
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Startup.InitializeMapping();

            //configure container
            var builder = new ContainerBuilder();

            builder.RegisterModule<ScrapingModule>();
            builder.RegisterModule<DatabaseModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ApiServicesModule>();
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
                    container.Resolve<ICharacterAttributeRowService>());
            }

            Console.WriteLine("Seeding completed.  Press any key to close.");
            Console.Read();
        }
    }
}

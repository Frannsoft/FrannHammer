using System;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Domain;
using FrannHammer.Seeding;

namespace FrannHammer.Seeder
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Startup.InitializeMapping();

            var characterDataScraper = Container.Instance.Resolve<ICharacterDataScraper>();

            foreach (var character in Characters.All)
            {
                Console.WriteLine($"Scraping data for '{character.Name}'...");
                characterDataScraper.PopulateCharacterFromWeb(character);

                var seeder = Container.Instance.Resolve<DefaultSeeder>();
                seeder.SeedCharacterData(character,
                    Container.Instance.Resolve<ICharacterService>(),
                    Container.Instance.Resolve<IMovementService>(),
                    Container.Instance.Resolve<IMoveService>(),
                    Container.Instance.Resolve<ICharacterAttributeRowService>());
            }

            Console.WriteLine("Seeding completed.  Press any key to close.");
            Console.Read();
        }
    }
}

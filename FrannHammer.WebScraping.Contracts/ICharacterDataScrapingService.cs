using FrannHammer.WebScraping.Domain;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface ICharacterDataScrapingService
    {
        void PopulateCharacterFromWeb<T>(T character) where T : WebCharacter;
    }
}

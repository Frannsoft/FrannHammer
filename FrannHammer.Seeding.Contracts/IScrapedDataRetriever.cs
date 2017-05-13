using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.Seeding.Contracts
{
    public interface IScrapedDataRetriever
    {
        T GetCharacterData<T>(T character) where T : WebCharacter;
    }
}

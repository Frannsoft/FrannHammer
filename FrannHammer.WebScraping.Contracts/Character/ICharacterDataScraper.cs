using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Character
{
    public interface ICharacterDataScraper
    {
        void PopulateCharacterFromWeb(WebCharacter character);
    }
}

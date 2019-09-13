using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Character
{
    public interface ICharacterDataScraper
    {
        WebCharacter PopulateCharacterFromWeb(WebCharacter character, string sourceBaseUrl);
    }
}

using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Character
{
    public interface ICharacterDataScrapingServices
    {
        WebCharacter PopulateCharacter(WebCharacter character, string sourceBaseUrl);
    }
}

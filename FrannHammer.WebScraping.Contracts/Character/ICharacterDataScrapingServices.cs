using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Character
{
    public interface ICharacterDataScrapingServices
    {
        void PopulateCharacter(WebCharacter character);
    }
}

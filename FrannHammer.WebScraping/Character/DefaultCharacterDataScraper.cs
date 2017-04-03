using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Character
{
    public class DefaultCharacterDataScraper : ICharacterDataScraper
    {
        private readonly ICharacterDataScrapingServices _characterDataScrapingServices;

        public DefaultCharacterDataScraper(ICharacterDataScrapingServices characterDataScrapingServices)
        {
            Guard.VerifyObjectNotNull(characterDataScrapingServices, nameof(characterDataScrapingServices));
            _characterDataScrapingServices = characterDataScrapingServices;
        }

        public void PopulateCharacterFromWeb(WebCharacter character)
        {
            _characterDataScrapingServices.PopulateCharacter(character);
        }
    }
}

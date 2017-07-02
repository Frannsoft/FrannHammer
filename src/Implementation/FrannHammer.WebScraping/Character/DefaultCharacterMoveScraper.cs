using System.Collections.Generic;
using System.Linq;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Contracts.Moves;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Character
{
    public class DefaultCharacterMoveScraper : ICharacterMoveScraper
    {
        private readonly IEnumerable<IMoveScraper> _scrapers;

        public DefaultCharacterMoveScraper(IEnumerable<IMoveScraper> scrapers)
        {
            Guard.VerifyObjectNotNull(scrapers, nameof(scrapers));
            _scrapers = scrapers;
        }

        public IEnumerable<IMove> ScrapeMoves(WebCharacter character)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));
            return _scrapers.SelectMany(scraper => scraper.Scrape(character));
        }
    }
}

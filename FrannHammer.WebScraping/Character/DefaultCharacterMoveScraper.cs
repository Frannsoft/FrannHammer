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
        private readonly IMoveScraper _groundMoveScraper;
        private readonly IMoveScraper _aerialMoveScraper;
        private readonly IMoveScraper _specialMoveScraper;

        public DefaultCharacterMoveScraper(IMoveScraper groundMoveScraper, IMoveScraper aerialMoveScraper,
            IMoveScraper specialMoveScraper)
        {
            Guard.VerifyObjectNotNull(groundMoveScraper, nameof(groundMoveScraper));
            Guard.VerifyObjectNotNull(aerialMoveScraper, nameof(aerialMoveScraper));
            Guard.VerifyObjectNotNull(specialMoveScraper, nameof(specialMoveScraper));

            _groundMoveScraper = groundMoveScraper;
            _aerialMoveScraper = aerialMoveScraper;
            _specialMoveScraper = specialMoveScraper;
        }

        public IEnumerable<IMove> ScrapeMoves(WebCharacter character)
        {
            return _groundMoveScraper.Scrape(character.SourceUrl)
                .Concat(_aerialMoveScraper.Scrape(character.SourceUrl))
                    .Concat(_specialMoveScraper.Scrape(character.SourceUrl));
        }
    }
}

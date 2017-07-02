using FrannHammer.Domain.Contracts;
using System.Collections.Generic;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Character
{
    public interface ICharacterMoveScraper
    {
        IEnumerable<IMove> ScrapeMoves(WebCharacter character);
    }
}

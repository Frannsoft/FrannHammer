using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Moves
{
    public interface IMoveScraper
    {
        Func<WebCharacter, IEnumerable<IMove>> Scrape { get; }
    }
}

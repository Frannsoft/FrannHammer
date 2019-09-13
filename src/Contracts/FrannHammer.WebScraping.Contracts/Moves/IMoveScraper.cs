using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Contracts.Moves
{
    public interface IMoveScraper
    {
        Func<WebCharacter, IEnumerable<IMove>> Scrape { get; }
    }
}

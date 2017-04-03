using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Moves
{
    public interface IMoveScraper
    {
        Func<string, IEnumerable<IMove>> Scrape { get; }
    }
}

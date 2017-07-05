using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.UniqueData
{
    public interface IUniqueDataScraper
    {
        Func<WebCharacter, IEnumerable<IUniqueData>> Scrape { get; }
    }
}
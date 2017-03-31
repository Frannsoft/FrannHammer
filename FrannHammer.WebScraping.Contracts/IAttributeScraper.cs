using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IAttributeScraper
    {
        string AttributeName { get; }
        Func<string, IScrapingServices, IEnumerable<IAttribute>> Scrape { get; }
    }
}

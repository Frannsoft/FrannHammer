using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IAttributeScraper
    {
        string AttributeName { get; }
        Func<IEnumerable<IAttribute>> Scrape { get; }
    }
}

using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Attributes
{
    public interface IAttributeScraper
    {
        string AttributeName { get; }
        Func<string, IEnumerable<ICharacterAttributeRow>> Scrape { get; }
    }
}

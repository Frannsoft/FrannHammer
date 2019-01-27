using System;
using System.Collections.Generic;
using FrannHammer.Domain.Contracts;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Contracts.Attributes
{
    public interface IAttributeScraper
    {
        string AttributeName { get; }
        Func<WebCharacter, IEnumerable<ICharacterAttributeRow>> Scrape { get; }
        string SourceUrl { get; set; }
    }
}

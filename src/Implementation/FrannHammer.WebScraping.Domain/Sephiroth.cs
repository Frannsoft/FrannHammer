using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.Unique;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain
{
    public class Sephiroth : WebCharacter
    {
        public Sephiroth() : base("Sephiroth", uniqueScraperTypes: new List<Type>
            {
                typeof(OneWingedAngelScraper)
            })
        {
            DisplayName = "Sephiroth";
            CssKey = "sephiroth";
        }
    }
}

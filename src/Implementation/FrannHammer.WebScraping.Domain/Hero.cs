using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.Unique;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain
{
    public class Hero : WebCharacter
    {
        public Hero()
            : base("Hero",
                   uniqueScraperTypes: new List<Type>
                   {
                       typeof(CommandSelectionScraper)
                   })
        {
            CssKey = "hero";
        }
    }
}

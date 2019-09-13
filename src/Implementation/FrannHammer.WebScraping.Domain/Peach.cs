using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.Unique;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain
{
    public class Peach : WebCharacter
    {
        public Peach()
            : base("Peach",
                   uniqueScraperTypes: new List<Type>
                    {
                        typeof(FloatScraper),
                        typeof(VegetableScraper)
                    })
        {
            CssKey = "peachu";
        }
    }
}
using FrannHammer.WebScraping.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain
{
    public class Daisy : WebCharacter
    {
        public Daisy()
            : base("Daisy",
                   uniqueScraperTypes: new List<Type>
                    {
                        typeof(FloatScraper),
                        typeof(VegetableScraper)
                    })
        {
            CssKey = "daisy";
        }
    }
}

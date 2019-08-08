using FrannHammer.WebScraping.Domain.Contracts;
using FrannHammer.WebScraping.Unique;
using System;
using System.Collections.Generic;

namespace FrannHammer.WebScraping.Domain
{
    public class Lucario : WebCharacter
    {
        public Lucario()
            : base("Lucario",
                  uniqueScraperTypes: new List<Type>
                  {
                      typeof(AuraScraper)
                  })
        {
            CssKey = "lucario";
        }
    }
}